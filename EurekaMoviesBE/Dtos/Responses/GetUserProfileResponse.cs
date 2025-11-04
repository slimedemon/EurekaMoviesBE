namespace EurekaMoviesBE.Dtos.Responses;

public class GetUserProfileResponse : BaseResponse
{
    public GetUserProfileData Data { get; set; }
}

public class GetUserProfileData
{
    public string UserId { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
}