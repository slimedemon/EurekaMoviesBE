namespace EurekaMoviesBE.Options;

public class HttpClientOption
{
    public const string OptionName = "HttpClientConfig";
    public string Url { get; set; }
    public int HttpClientTimeout { get; set; }
}