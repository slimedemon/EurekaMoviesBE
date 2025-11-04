using MediatR;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Queries.UserQueries.GetUserProfile;

public class GetUserProfileQuery : IRequest<GetUserProfileResponse>
{
}