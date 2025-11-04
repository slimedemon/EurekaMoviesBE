using EurekaMoviesBE.Features.Commands.UserCommands.Login;
using EurekaMoviesBE.Features.Commands.UserCommands.Register;

namespace EurekaMoviesBE.Features.Commands.UserCommands.LoginSocial;

public class LoginSocialHandler : IRequestHandler<LoginSocialCommand, LoginSocialResponse>
{
    private readonly ILogger<LoginSocialHandler> _logger;
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly IGoogleService _googleService;
    private readonly IMediator _mediator;

    public LoginSocialHandler
    (
        ILogger<LoginSocialHandler> logger,
        IApplicationUnitOfWork unitOfRepository,
        IGoogleService googleService,
        IMediator mediator
    )
    {
        _logger = logger;
        _unitOfRepository = unitOfRepository;
        _googleService = googleService;
        _mediator = mediator;
    }

    public async Task<LoginSocialResponse> Handle(LoginSocialCommand request, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(LoginSocialHandler)} => ";
        _logger.LogInformation($"{functionName} ");
        var response = new LoginSocialResponse { Status = (int)ResponseStatusCode.BadRequest };
        
        try
        {
            var jwtString = request.Payload.JwtString;

            var socialOutput = await _googleService.ExchangeGoogleIdToken(jwtString);
            if (socialOutput is null)
            {
                _logger.LogWarning($"{functionName} AuthorizeCode is invalid");
                response.Status = (int)ResponseStatusCode.Unauthorized;
                response.ErrorMessage = "AuthorizeCode is invalid";
                return response;
            }

            var emailUpper = socialOutput.Email.ToUpper().Trim();

            var user = await _unitOfRepository.User
                .Where(u => u.NormalizedEmail.Equals(emailUpper)
                    && u.IsActive
                    && !u.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                #region REGISTER NEW ACCOUNT

                var email = socialOutput.Email;

                var registerRequest = new RegisterRequest
                {
                    Avatar = "Default",
                    DisplayName = socialOutput.FullName,
                    Email = email,
                    Password = IdentityResourceConstants.PasswordDefault,
                };

                var registerCommand = new RegisterCommand(registerRequest);
                registerCommand.IsSocial = true;

                var registerResponse = await _mediator.Send(registerCommand, cancellationToken);

                if (registerResponse.Status != (int)ResponseStatusCode.Created)
                {
                    _logger.LogError($"{functionName} User register failed: {registerResponse.ErrorMessage}");
                    response.ErrorMessage = registerResponse.ErrorMessage;
                    response.Status = registerResponse.Status;
                    return response;
                }

                #endregion
            }

            var loginRequest = new LoginRequest
            {
                Email = socialOutput.Email,
                Password = IdentityResourceConstants.PasswordDefault
            };
            var loginResponse = await _mediator.Send(new LoginCommand(loginRequest), cancellationToken);
            if (loginResponse.Status != (int)ResponseStatusCode.Ok)
            {
                _logger.LogError($"{functionName} Get token error {loginResponse.ErrorMessage}");
                response.ErrorMessage = "Can get token";
                response.Status = (int)ResponseStatusCode.Unauthorized;
                return response;
            }
            
            response.Status = (int)ResponseStatusCode.Ok;
            response.Data = loginResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.Status = (int)ResponseStatusCode.InternalServerError;
            response.ErrorMessage = "An error occurred";
        }

        return response;
    }
}