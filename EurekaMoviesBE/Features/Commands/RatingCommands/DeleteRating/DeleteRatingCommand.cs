namespace EurekaMoviesBE.Features.Commands.RatingCommands.DeleteRating;

public class DeleteRatingCommand : IRequest<DeleteRatingResponse>
{
    public long Id { get; set; }
    public DeleteRatingCommand(long id)
    {
        Id = id;
    }
}