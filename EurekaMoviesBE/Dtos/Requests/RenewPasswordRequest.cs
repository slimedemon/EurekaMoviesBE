namespace EurekaMoviesBE.Dtos.Requests;

public class RenewPasswordRequest
{
    public string Email { get; set; }
    public string ResetCode { get; set; }
    public string NewPassword { get; set; }
}