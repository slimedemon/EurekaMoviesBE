using MediatR;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Queries.MovieQueries.GetMovieDetailById;

public class GetMovieDetailByIdQuery : IRequest<GetMovieDetailByIdResponse>
{
    public string Id { get; set; }
    public GetMovieDetailByIdQuery(string id)
    {
        Id = id;
    }
}