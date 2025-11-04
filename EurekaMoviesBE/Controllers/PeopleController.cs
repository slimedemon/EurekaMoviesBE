using EurekaMoviesBE.Features.Queries.CastQueries.GetActingList;
using EurekaMoviesBE.Features.Queries.CastQueries.GetCastDetail;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    public PeopleController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("ActingList")]
    public async Task<IActionResult> GetActingList([FromQuery] GetActingListRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetActingListQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpGet("Cast/{tmdbId}")]
    public async Task<IActionResult> GetCastDetail([FromRoute] long tmdbId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCastDetailQuery(tmdbId), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
}