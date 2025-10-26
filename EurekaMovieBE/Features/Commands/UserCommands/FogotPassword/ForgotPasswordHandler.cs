namespace EurekaMovieBE.Features.Commands.UserCommands.FogotPassword;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ForgotPasswordHandler> _logger;
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly IMailSenderService _mailSenderService;
    private readonly IMemoryCacheService _memoryCacheService;
    public ForgotPasswordHandler
    (
        UserManager<User> userManager,
        ILogger<ForgotPasswordHandler> logger,
        IApplicationUnitOfWork unitOfRepository,
        IMailSenderService mailSenderService,
        IMemoryCacheService memoryCacheService
    )
    {
        _userManager = userManager;
        _logger = logger;
        _unitOfRepository = unitOfRepository;
        _mailSenderService = mailSenderService;
        _memoryCacheService = memoryCacheService;
    }
    
    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var functionName =
            $"{nameof(ForgotPasswordResponse)} Email = {email}=> ";
        _logger.LogInformation(functionName);
        var response = new ForgotPasswordResponse { Status = (int)ResponseStatusCode.BadRequest };

        try
        {
            var user = await _unitOfRepository.User
                .Where(u => u.IsActive
                            && u.NormalizedUserName!.Equals(email.ToUpper()))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken)!;

            if (user is null)
            {
                _logger.LogError($"{functionName} User not found");
                response.Status = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "User not found";
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            while (true)
            {
                var resetCode = GenerateCode().ToString();
                var resetDto = _memoryCacheService.Get<ResetPasswordDto>(resetCode);
                
                if (resetDto is not null) continue;
                
                resetDto = new ResetPasswordDto
                {
                    Email = user.UserName!,
                    ResetToken = token
                };
            
                _memoryCacheService.Set(resetCode, resetDto);
                await _mailSenderService.SendResetPasswordEmail(user.UserName, resetCode);
                break;
            }
            
            response.Status = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Error: {ex.Message}");
            response.ErrorMessage = "An error occurred";
        }

        return response;
    }

    #region Private methods

    public int GenerateCode()
    {
        Random random = new Random();
        return random.Next(100000, 1000000); 
    }

    #endregion
}