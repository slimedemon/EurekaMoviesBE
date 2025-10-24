using System.Net;
using System.Net.Mail;
using System.Web;

namespace EurekaMovieBE.Services
{
    public class MailSenderService : IMailSenderService
    {
        private readonly ILogger<MailSenderService> _logger;
        private readonly AuthenticationOptions _authenticationOptions;

        public MailSenderService
        (
            ILogger<MailSenderService> logger, IOptions<AuthenticationOptions> authenticationOptions)
        {
            _logger = logger;
            _authenticationOptions = authenticationOptions.Value;
        }
        public Task SendRegistrationEmail(string toEmail, string verificationToken, string userName)
        {
            var functionName = $"{nameof(MailSenderService)} - {nameof(SendRegistrationEmail)} => ";
            try
            {
                _logger.LogInformation(functionName);
                var body = ProcessVerificationEmailBody(verificationToken, userName, toEmail);
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = EmailConstant.SMTPPort,
                    Credentials = new NetworkCredential(EmailConstant.SMTPUser, EmailConstant.SMTPPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(EmailConstant.SMTPUser, EmailConstant.CompanyName),
                    Subject = string.Concat(EmailConstant.CompanyName, EmailSubjectConstant.RegistrationEmail),
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        public Task SendResetPasswordEmail(string toEmail, string resetToken)
        {
            var functionName = $"{nameof(MailSenderService)} - {nameof(SendResetPasswordEmail)} => ";
            try
            {
                _logger.LogInformation(functionName);
                var body = ProcessResetPasswordEmailBody(resetToken);
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = EmailConstant.SMTPPort,
                    Credentials = new NetworkCredential(EmailConstant.SMTPUser, EmailConstant.SMTPPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(EmailConstant.SMTPUser, EmailConstant.CompanyName),
                    Subject = string.Concat(EmailConstant.CompanyName, EmailSubjectConstant.ResetPasswordEmail),
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        #region Private methods

        private string ProcessVerificationEmailBody(string verificationToken, string userName, string email)
        {
            var template = EmailTemplates.RegistrationEmailTemplate;
            string tokenSafeString = HttpUtility.UrlEncode(verificationToken);
            string emailSafeString = HttpUtility.UrlEncode(email);
            var verificationLink = $"{_authenticationOptions.ConfirmEmailUrl}?Email={emailSafeString}&VerificationToken={tokenSafeString}";
            template = template.Replace("{{VerificationLink}}", verificationLink);
            template = template.Replace("{{UserName}}", userName);
            return template;
        }

        private string ProcessResetPasswordEmailBody(string resetToken)
        {
            var template = EmailTemplates.ResetPasswordEmailTemplate;
            string tokenSafeString = HttpUtility.UrlEncode(resetToken);
            template = template.Replace("{{ResetToken}}", tokenSafeString);
            return template;
        }

        #endregion
    }
}
