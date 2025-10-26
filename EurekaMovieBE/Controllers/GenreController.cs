using EurekaMovieBE.Features.Queries.GenreQueries.GetGenres;
using Microsoft.AspNetCore.Mvc;

namespace MovieStreaming.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;
    public GenreController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("Genres")]
    public async Task<IActionResult> GetGenres(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetGenresQuery(), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
}