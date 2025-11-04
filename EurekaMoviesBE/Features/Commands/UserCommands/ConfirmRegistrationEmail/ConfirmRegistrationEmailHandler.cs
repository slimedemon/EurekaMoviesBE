using System.Text.Json;

namespace EurekaMoviesBE.Features.Commands.UserCommands.ConfirmRegistrationEmail;

public class ConfirmRegistrationEmailHandler : IRequestHandler<ConfirmRegistrationEmailCommand, ConfirmRegistrationEmailResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ConfirmRegistrationEmailHandler> _logger;
    private readonly IApplicationUnitOfWork _unitOfRepository;

    public ConfirmRegistrationEmailHandler
    (
        UserManager<User> userManager,
        ILogger<ConfirmRegistrationEmailHandler> logger,
        IApplicationUnitOfWork unitOfRepository
    )
    {
        _userManager = userManager;
        _logger = logger;
        _unitOfRepository = unitOfRepository;
    }

    public async Task<ConfirmRegistrationEmailResponse> Handle(ConfirmRegistrationEmailCommand request,
        CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName =
            $"{nameof(ConfirmRegistrationEmailHandler)} Payload = {JsonSerializer.Serialize(payload)}=> ";
        _logger.LogInformation(functionName);
        var response = new ConfirmRegistrationEmailResponse { Status = (int)ResponseStatusCode.BadRequest };

        try
        {
            var user = await _unitOfRepository.User
                .Where(u => !u.IsActive
                            && u.NormalizedUserName!.Equals(payload.Email.ToUpper()))
                .FirstOrDefaultAsync(cancellationToken)!;

            if (user is null)
            {
                _logger.LogError($"{functionName} account not found");
                response.ErrorMessage = "User not found";
                return response;
            }

            if (user.EmailConfirmed)
            {
                _logger.LogWarning($"{functionName} email already verified");
                response.ErrorMessage = "Email already verified";
                return response;
            }

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, payload.VerificationToken);
            if (confirmEmailResult.Errors.Any())
            {
                _logger.LogError(
                    $"{functionName} has error: Errors = {JsonSerializer.Serialize(confirmEmailResult.Errors)}");
                response.ErrorMessage = "Something went wrong";
                return response;
            }

            user.IsActive = true;
            _unitOfRepository.User.Update(user);
            await _unitOfRepository.CompleteAsync();

            response.Status = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Error: {ex.Message}");
            response.Status = (int)ResponseStatusCode.InternalServerError;
            response.ErrorMessage = "An error occurred";
        }

        return response;
    }
}