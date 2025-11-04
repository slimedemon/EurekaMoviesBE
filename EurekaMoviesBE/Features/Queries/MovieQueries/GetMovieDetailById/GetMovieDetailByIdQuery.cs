using MediatR;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetMovieDetailById;

public class GetMovieDetailByIdQuery : IRequest<GetMovieDetailByIdResponse>
{
    public string Id { get; set; }
    public GetMovieDetailByIdQuery(string id)
    {
        Id = id;
    }
}