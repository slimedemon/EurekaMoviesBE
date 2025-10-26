namespace EurekaMovieBE.Services;

public interface IGoogleService
{
    Task<SocialAuthDto?> ExchangeGoogleIdToken(string authCode);
}