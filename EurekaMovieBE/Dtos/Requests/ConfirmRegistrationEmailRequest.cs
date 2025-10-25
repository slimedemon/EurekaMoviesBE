namespace EurekaMovieBE.Dtos.Requests;

public class ConfirmRegistrationEmailRequest
{
    public string Email { get; set; }
    public string VerificationToken { get; set; }
}