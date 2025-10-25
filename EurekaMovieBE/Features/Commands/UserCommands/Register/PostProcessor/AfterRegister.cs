using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using EurekaMovieBE.Data.AuthData;
using EurekaMovieBE.Enums;
using EurekaMovieBE.Dtos.Responses;
using EurekaMovieBE.Persistence.UnitOfWork.Postgres;
using EurekaMovieBE.Services.MailSender;

namespace EurekaMovieBE.Features.Commands.UserCommands.Register.PostProcessor;

public class AfterRegister : IRequestPostProcessor<RegisterCommand, RegisterResponse>
{
    private readonly IMailSenderService _mailSenderService;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AfterRegister> _logger;
    private readonly IUnitOfRepository _unitOfRepository;
    public AfterRegister
    (
        IMailSenderService mailSenderService, 
        UserManager<User> userManager,
        ILogger<AfterRegister> logger,
        IUnitOfRepository unitOfRepository
    )
    {
        _mailSenderService = mailSenderService;
        _userManager = userManager;
        _logger = logger;
        _unitOfRepository = unitOfRepository;
    }
    public async Task Process(RegisterCommand request, RegisterResponse response, CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(AfterRegister)} =>";

        try
        {
            if (response.Status== (int)ResponseStatusCode.Created && !request.IsSocial)
            {
                var payload = request.Payload;
                var newUser = response.User;
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _mailSenderService.SendRegistrationEmail(newUser.UserName, confirmationToken, payload.DisplayName);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}