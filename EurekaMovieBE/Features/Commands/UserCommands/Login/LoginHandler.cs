using System.Net;
using Duende.IdentityModel.Client;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EurekaMovieBE.Data.AuthData;
using EurekaMovieBE.Enums;
using EurekaMovieBE.Dtos.Responses;
using EurekaMovieBE.Options;
using EurekaMovieBE.Persistence.UnitOfWork.Postgres;

namespace EurekaMovieBE.Features.Commands.UserCommands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<LoginHandler> _logger;
    private readonly AuthenticationOptions _authenticationOptions;

    public LoginHandler
    (
        IUnitOfRepository unitOfRepository, 
        UserManager<User> userManager, 
        ILogger<LoginHandler> logger, 
        IOptions<AuthenticationOptions> authenticationOptions
    )
    {
        _unitOfRepository = unitOfRepository;
        _userManager = userManager;
        _logger = logger;
        _authenticationOptions = authenticationOptions.Value;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var functionName = nameof(LoginHandler);
        _logger.LogInformation($"{functionName} => ");
        var loginResponse = new LoginResponse(){Status = (int)ResponseStatusCode.BadRequest};
        try
        {
            var payload = request.Payload;
            var user = await _unitOfRepository.User
                .Where(u => 
                    u.NormalizedUserName.Equals(payload.Email.ToUpper())
                    && !u.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                _logger.LogWarning($"{functionName} => User not found or deleted");
                loginResponse.Status = (int)ResponseStatusCode.NotFound;
                loginResponse.ErrorMessage = "User not found or deleted";
                return loginResponse;
            }
            
            if (!user.IsActive || !user.EmailConfirmed)
            {
                _logger.LogWarning($"{functionName} => User is not active or email not confirmed");
                loginResponse.Status = (int)ResponseStatusCode.Forbidden;
                loginResponse.ErrorMessage = "User is not active or email not confirmed";
                return loginResponse;
            }
            
            var client = await _unitOfRepository.Client
                .Where(cl => cl.Id == user.ClientId)
                .Select(client => new Client
                {
                    Id = client.Id,
                    ClientId = client.ClientId
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            var clientSecret = await _unitOfRepository.ClientSecret
                .Where(cs => cs.ClientId == client.Id)
                .Select(cs => cs.Secrets)
                .FirstOrDefaultAsync(cancellationToken);
            
            var scopes = await _unitOfRepository.ClientScope
                .Where(p => p.ClientId == client.Id)
                .Select(p => p.Scope)
                .ToListAsync(cancellationToken);
            var requestScopes = "";
            
            foreach (var scope in scopes)
            {
                requestScopes += " " + scope;
            }
            
            var httpClient = new HttpClient();
            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{_authenticationOptions.Authority}/connect/token",
                ClientId = client.ClientId,
                ClientSecret = clientSecret,
                Scope = requestScopes.Trim(),
                UserName = payload.Email,
                Password = payload.Password
            }, cancellationToken);
            
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                loginResponse.Status= (int)ResponseStatusCode.Ok;
                loginResponse.Data = new LoginResponseData()
                {
                    AccessToken = response.AccessToken,
                    Scope = response.Scope,
                    Expired = response.ExpiresIn
                };
            }
            else
            {
                _logger.LogError($"{functionName} Can't get access token: {response.ErrorDescription}");
                loginResponse.Status = (int)response.HttpStatusCode;
                loginResponse.ErrorMessage = response.ErrorDescription;
            }
            return loginResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Error: {ex.Message}");
            loginResponse.Status = (int)ResponseStatusCode.InternalServerError;
            loginResponse.ErrorMessage = "An error occurred";
            return loginResponse;
        }
    }
}