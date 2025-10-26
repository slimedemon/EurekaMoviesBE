using Google.Apis.Auth;

namespace EurekaMovieBE.Services;

public class GoogleService : IGoogleService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<GoogleService> _logger;

    public GoogleService(
        IConfiguration configuration, 
        ILogger<GoogleService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<SocialAuthDto?> ExchangeGoogleIdToken(string idToken)
    {
        try
        {
            // Verify the JWT ID token sent from the frontend using the public keys from Google
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { _configuration["Google:ClientID"] }  // Ensure you use your correct ClientID here
            });

            // Extract necessary information from the Google payload (e.g., email, name, etc.)
            var userEmail = payload.Email;
            var emailVerified = payload.EmailVerified;
            var firstName = payload.GivenName;
            var lastName = payload.FamilyName;

            // Ensure the email is verified and valid
            if (string.IsNullOrEmpty(userEmail) || !emailVerified)
            {
                _logger.LogError($"{nameof(GoogleService)} Invalid or unverified email");
                return null;
            }

            // Fill in the first name and last name if not provided
            firstName = string.IsNullOrEmpty(firstName) ? userEmail : firstName;
            lastName = string.IsNullOrEmpty(lastName) ? "" : lastName;

            // Return the user's data in a DTO
            return new SocialAuthDto
            {
                Email = userEmail.ToLower(),
                FullName = firstName + " " + lastName
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(GoogleService)} Error during token verification: Message = {ex.Message}");
            return null;
        }
    }
}
