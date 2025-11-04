namespace EurekaMoviesBE.Services;

public interface IGoogleService
{
    Task<SocialAuthDto?> ExchangeGoogleIdToken(string authCode);
}