namespace EurekaMovieBE.Dtos;

public class ResetPasswordDto
{
    public string Email { get; set; }
    public string ResetToken { get; set; }
}