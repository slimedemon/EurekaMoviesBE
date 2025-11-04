using EurekaMoviesBE.Features.Commands.WatchListCommands.AddMovieToWatchList;
using EurekaMoviesBE.Features.Commands.WatchListCommands.RemoveMovieFromWatchList;
using EurekaMoviesBE.Features.Queries.WatchListQueries.GetWatchList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(SystemRole.Viewer)]
public class WatchListController : ControllerBase
{
    private readonly IMediator _mediator;
    public WatchListController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetWatchList")]
    public async Task<IActionResult> GetFavoriteList([FromQuery] GetWatchListRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetWatchListQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddFilmToWatchList([FromBody] AddMovieToWatchListRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddMovieToWatchListCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpDelete("Remove/{watchListId}")]
    public async Task<IActionResult> RemoveWatchList([FromRoute] long watchListId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveMovieFromWatchListCommand(watchListId), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
}