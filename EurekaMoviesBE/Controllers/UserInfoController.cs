using EurekaMoviesBE.Features.Queries.UserQueries.GetUserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserInfoController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserInfoController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetProfile")]
    public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserProfileQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
}