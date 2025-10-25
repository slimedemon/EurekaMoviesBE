using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Queries.RecommendationQueries.LLMSearch;

public class LLMSearchQuery : IRequest<LLMSearchResponse>
{
    public LLMSearchRequest Payload { get; set; }
    public LLMSearchQuery(LLMSearchRequest payload)
    {
        Payload = payload;
    }
}