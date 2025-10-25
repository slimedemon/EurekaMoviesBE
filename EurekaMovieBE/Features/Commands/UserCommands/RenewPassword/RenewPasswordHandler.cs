using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EurekaMovieBE.Data.AuthData;
using EurekaMovieBE.Enums;
using EurekaMovieBE.Dtos.Dtos;
using EurekaMovieBE.Dtos.Responses;
using EurekaMovieBE.Persistence.UnitOfWork.Postgres;
using EurekaMovieBE.Services.MemoryCache;

namespace EurekaMovieBE.Features.Commands.UserCommands.RenewPassword;

public class RenewPasswordHandler : IRequestHandler<RenewPasswordCommand, RenewPasswordResponse>
{
    private readonly ILogger<RenewPasswordHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly IMemoryCacheService _memoryCacheService;
    public RenewPasswordHandler
    (
        ILogger<RenewPasswordHandler> logger,
        UserManager<User> userManager,
        IUnitOfRepository unitOfRepository,
        IMemoryCacheService memoryCacheService
    )
    {
        _logger = logger;
        _userManager = userManager;
        _unitOfRepository = unitOfRepository;
        _memoryCacheService = memoryCacheService;
    }
    
    public async Task<RenewPasswordResponse> Handle(RenewPasswordCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(RenewPasswordHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new RenewPasswordResponse{ Status = (int)ResponseStatusCode.BadRequest };

        try
        {
            var user = await _unitOfRepository.User
                .Where(u => u.UserName.ToLower().Equals(payload.Email.ToLower()))
                .FirstOrDefaultAsync(cancellationToken);
            if (user is null)
            {
                _logger.LogWarning($"{functionName} User does not exist");
                response.ErrorMessage = "User does not exist";
                return response;
            }
            
            var resetDto = _memoryCacheService.Get<ResetPasswordDto>(payload.ResetCode);

            if (resetDto is null)
            {
                _logger.LogWarning($"{functionName} Invalid reset code");
                response.ErrorMessage = "Invalid reset code";
                return response;
            }

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", resetDto.ResetToken);
            
            if (!isValidToken)
            {
                _logger.LogWarning($"{functionName} Invalid reset token");
                response.ErrorMessage = "Invalid reset token";
                return response;
            }
            
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, payload.NewPassword);
            user.PasswordHash = hashedPassword;

            await _unitOfRepository.CompleteAsync();
            
            response.Status = (int)ResponseStatusCode.Ok;
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