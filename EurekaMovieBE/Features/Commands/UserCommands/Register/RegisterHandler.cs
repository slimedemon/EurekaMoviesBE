namespace EurekaMovieBE.Features.Commands.UserCommands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<RegisterHandler> _logger;
    private readonly UserManager<User> _userManager;
    public RegisterHandler
    (
        IApplicationUnitOfWork unitOfRepository,
        ILogger<RegisterHandler> logger,
        UserManager<User> userManager
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _userManager = userManager;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(RegisterHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new RegisterResponse{ Status = (int)ResponseStatusCode.BadRequest };

        try
        {
            var user = await _unitOfRepository.User
                .Where(u => u.UserName!.ToLower().Equals(payload.Email.ToLower()))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (user != null)
            {
                _logger.LogError($"{functionName} => Email is already in used");
                response.ErrorMessage = "Email is already in used";
                return response;
            }
            var accountId = Guid.NewGuid().ToString();
            var newUser = new User
            {
                Id = accountId,
                ClientId = 1,
                UserName = payload.Email,
                Email = payload.Email,
                IsActive = request.IsSocial,
                EmailConfirmed = request.IsSocial
            };
            
            var hashedPassword = new PasswordHasher<User>().HashPassword(newUser, payload.Password); 
            newUser.PasswordHash = hashedPassword;
            var createResult = await _userManager.CreateAsync(newUser);
            
            var createUserRole = await _userManager.AddToRoleAsync(newUser, SystemRole.Viewer);
            if (!createResult.Succeeded || !createUserRole.Succeeded)
            {
                throw new Exception($"Failed to create user: {createResult.Errors.ToString() + createUserRole.Errors}");
            }

            var userInfo = new UserInfo
            {
                Id = Guid.Parse(newUser.Id),
                Email = payload.Email,
                IsActive = true,
                Avatar = payload.Avatar,
                DisplayName = payload.DisplayName
            };
            
            await _unitOfRepository.UserInfo.AddAsync(userInfo);
            await _unitOfRepository.CompleteAsync();
            
            response.Status = (int)ResponseStatusCode.Created;
            response.User = newUser;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = "An error occurred";
            response.Status = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}