namespace EurekaMovieBE.Dtos.Requests;

public class RegisterRequest
{
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Avatar { get; set; }
    public string Password { get; set; }
}