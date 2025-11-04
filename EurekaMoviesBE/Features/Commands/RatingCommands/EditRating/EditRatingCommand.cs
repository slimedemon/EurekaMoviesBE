namespace EurekaMoviesBE.Features.Commands.RatingCommands.EditRating;

public class EditRatingCommand : IRequest<EditRatingResponse>
{
    public EditRatingRequest Payload { get; set; }
    public EditRatingCommand(EditRatingRequest payload)
    {
        Payload = payload;
    }
}