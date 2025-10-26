using EurekaMovieBE.Features.Queries.RecommendationQueries.GetNavigation;
using EurekaMovieBE.Features.Queries.RecommendationQueries.LLMSearch;
using EurekaMovieBE.Features.Queries.RecommendationQueries.RAGSearch;
using Microsoft.AspNetCore.Mvc;

namespace MovieStreaming.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly IMediator _mediator;
    public RecommendationController
    (
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet("Navigation")]
    public async Task<IActionResult> Navigation([FromQuery] string request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetNavigationQuery(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpGet("LLMSearch")]
    public async Task<IActionResult> LLMSeach([FromQuery] LLMSearchRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LLMSearchQuery(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
    
    [HttpGet("RAGSearch")]
    public async Task<IActionResult> RAGSearch([FromQuery] RAGSearchRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RAGSearchQuery(request), cancellationToken);
        return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
    }
}