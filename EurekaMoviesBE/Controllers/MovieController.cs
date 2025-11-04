using EurekaMoviesBE.Features.Queries.MovieQueries.GetLatestTrailers;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetListMovies;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetMovieDetail;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetMovieDetailById;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetPopularMovies;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetTrendingDay;
using EurekaMoviesBE.Features.Queries.MovieQueries.GetTrendingThisWeek;
using Microsoft.AspNetCore.Mvc;

namespace EurekaMoviesBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMediator _mediator;
    public MovieController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("TrendingDay")]
    public async Task<IActionResult> GetTrendingDay([FromQuery] GetTrendingMovieRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTrendingMovieTodayQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpGet("TrendingWeek")]
    public async Task<IActionResult> GetTrendingWeek([FromQuery] GetTrendingMovieRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTrendingMovieThisWeekQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpGet("Popular")]
    public async Task<IActionResult> GetPopularList([FromQuery] GetPopularMovieRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPopularMoviesQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpGet("Movies")]
    public async Task<IActionResult> GetMovies([FromQuery] GetListMoviesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetListMoviesQuery(request), cancellationToken);
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
    
    [HttpGet("Detail/{tmdbId}")]
    public async Task<IActionResult> GetDetail([FromRoute] long tmdbId,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetMovieDetailQuery(tmdbId), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpGet("DetailById/{id}")]
    public async Task<IActionResult> GetDetailById([FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetMovieDetailByIdQuery(id), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpGet("LatestTrailers")]
    public async Task<IActionResult> GetLatestTrailer([FromQuery] GetLatestTrailersRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetLatestTrailersQuery(request), cancellationToken); 
        return ResponseHelper.ToPaginationResponse(response.Status, response.ErrorMessage, response.Data, response.Paging);
    }
}