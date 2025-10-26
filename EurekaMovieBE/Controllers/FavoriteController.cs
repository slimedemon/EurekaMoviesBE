using EurekaMovieBE.Features.Commands.FavoriteCommands.MarkAsFavorite;
using EurekaMovieBE.Features.Commands.FavoriteCommands.UnmarkFavorite;
using EurekaMovieBE.Features.Queries.FavoriteQueries.GetListFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStreaming.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(SystemRole.Viewer)]
public class FavoriteController : ControllerBase
{ 
    private readonly IMediator _mediator;
    public FavoriteController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetFavoriteList")]
    public async Task<IActionResult> GetFavoriteList([FromQuery] GetListFavoriteRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetListFavoriteQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddToFavorite([FromBody] MarkAsFavoriteRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new MarkAsFavoriteCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpDelete("Remove/{favoriteId}")]
    public async Task<IActionResult> RemoveFavorite([FromRoute] long favoriteId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveFavoriteCommand(favoriteId), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
}