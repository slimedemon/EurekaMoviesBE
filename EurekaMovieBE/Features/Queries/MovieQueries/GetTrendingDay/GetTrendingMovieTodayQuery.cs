using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Queries.MovieQueries.GetTrendingDay;

public class GetTrendingMovieTodayQuery : IRequest<GetTrendingMovieTodayResponse>
{
    public GetTrendingMovieRequest Payload { get; set; }
    public GetTrendingMovieTodayQuery(GetTrendingMovieRequest payload)
    {
        Payload = payload;
    }
}