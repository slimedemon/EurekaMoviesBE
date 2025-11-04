using EurekaMoviesBE.Features.Commands.UserCommands.ConfirmRegistrationEmail;
using EurekaMoviesBE.Features.Commands.UserCommands.FogotPassword;
using EurekaMoviesBE.Features.Commands.UserCommands.Login;
using EurekaMoviesBE.Features.Commands.UserCommands.LoginSocial;
using EurekaMoviesBE.Features.Commands.UserCommands.Register;
using EurekaMoviesBE.Features.Commands.UserCommands.RenewPassword;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRecommendationService _recommendationService;
    public AuthenticationController
    (
        IMediator mediator,
        IRecommendationService recommendationService
    )
    {
        _mediator = mediator;
        _recommendationService = recommendationService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RegisterCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LoginCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpPost("LoginSocial")]
    public async Task<IActionResult> LoginSocial([FromBody] LoginSocialRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LoginSocialCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpGet("ConfirmRegister")]
    public async Task<IActionResult> ConfirmRegister([FromQuery] ConfirmRegistrationEmailRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ConfirmRegistrationEmailCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
    
    [HttpGet("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ForgotPasswordCommand(email), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
    
    [HttpPost("RenewPassword")]
    public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RenewPasswordCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
}