using MediatR;
using EurekaMoviesBE.Dtos.Requests;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetTrendingDay;

public class GetTrendingMovieTodayQuery : IRequest<GetTrendingMovieTodayResponse>
{
    public GetTrendingMovieRequest Payload { get; set; }
    public GetTrendingMovieTodayQuery(GetTrendingMovieRequest payload)
    {
        Payload = payload;
    }
}