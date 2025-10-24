namespace EurekaMovieBE.Options
{
    public class AuthenticationOptions
    {
        public const string OptionName = "Authentication";
        public string Authority { get; set; } = "http://localhost:5084";
        public string ConfirmEmailUrl { get; set; } = "http://localhost:5084/api/Authentication/ConfirmRegister";
    }
}
