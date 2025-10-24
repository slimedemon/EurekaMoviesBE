namespace EurekaMovieBE.Services
{
    public interface IMailSenderService
    {
        Task SendRegistrationEmail(string toEmail, string verificationToken, string userName);
        Task SendResetPasswordEmail(string toEmail, string resetToken);
    }
}
