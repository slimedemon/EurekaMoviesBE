namespace EurekaMovieBE.Features.Commands.RatingCommands.AddRating;

public class AddRatingCommand : IRequest<AddRatingResponse>
{
    public AddRatingRequest Payload { get; set; }
    public AddRatingCommand(AddRatingRequest payload)
    {
        Payload = payload;
    }
}