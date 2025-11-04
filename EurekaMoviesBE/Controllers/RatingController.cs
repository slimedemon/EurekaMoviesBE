using EurekaMoviesBE.Features.Commands.RatingCommands.AddRating;
using EurekaMoviesBE.Features.Commands.RatingCommands.DeleteRating;
using EurekaMoviesBE.Features.Commands.RatingCommands.EditRating;
using EurekaMoviesBE.Features.Queries.RatingQueries.GetRatingList;
using EurekaMoviesBE.Features.Queries.RatingQueries.GetReviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(SystemRole.Viewer)]
public class RatingController : ControllerBase
{
    private readonly IMediator _mediator;
    public RatingController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetRatingList")]
    public async Task<IActionResult> GetRatingList([FromQuery] GetRatingListRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRatingListQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [AllowAnonymous]
    [HttpGet("GetReviews")]
    public async Task<IActionResult> GetReviews([FromQuery] GetReviewsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetReviewsQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddRating([FromBody] AddRatingRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddRatingCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpPut("Edit")]
    public async Task<IActionResult> EditRating([FromBody] EditRatingRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new EditRatingCommand(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
    
    [HttpDelete("Delete/{ratingId}")]
    public async Task<IActionResult> DeleteRating([FromRoute] long ratingId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteRatingCommand(ratingId), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage);
    }
}
