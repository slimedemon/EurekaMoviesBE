using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EurekaMovieBE.HttpClientCustom;

public interface IHttpClientCustom
{
    Task<TResult?> GetAsync<TResult>(string path, CancellationToken cancellationToken);
    Task<TResult?> GetAsync<TResult>(string host, string path, CancellationToken cancellationToken);
    Task<TResult?> PostAsync<TResult>(string path, string jsonContent, CancellationToken cancellationToken);
    Task<TResult?> PostAsync<TResult>(string host, string path, string jsonContent, CancellationToken cancellationToken);
}

public class HttpClientCustom : IHttpClientCustom
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpClientCustom> _logger;
    
    public HttpClientCustom(
        HttpClient httpClient, 
        IHttpClientFactory httpClientFactory, 
        ILogger<HttpClientCustom> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<TResult?> GetAsync<TResult>(string path, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(IHttpClientCustom)}-{path}-{nameof(GetAsync)} => ";
        try
        {
            _logger.LogDebug($"{functionName} is called ...");
            
            var response = await _httpClient.GetAsync(path, cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<TResult>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            WriteLogCritical(functionName, ex);
            return default;
        }
    }
    
    public async Task<TResult?> GetAsync<TResult>(string host, string path, CancellationToken cancellationToken)
    {
        var requestUri = $"{host}/{path}";
        var functionName = $"{nameof(HttpClientCustom)} - {nameof(GetAsync)} =>";
        try
        {
            _logger.LogDebug($"{functionName} is called {requestUri}");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var httpClient = _httpClientFactory.CreateClient("EurekaMovieBE");
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<TResult>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            WriteLogCritical(functionName, ex);
            return default;
        }
    }
    
    public async Task<TResult?> PostAsync<TResult>(string path, string jsonContent, CancellationToken cancellationToken)
    {
        var functionName = $"{nameof(IHttpClientCustom)}_{path}-{nameof(PostAsync)} => ";
        
        try
        {
            _logger.LogDebug($"{functionName}");

            StringContent? content = null;
            if (!string.IsNullOrEmpty(jsonContent))
            {
                content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            using var response = await _httpClient.PostAsync(path, content, cancellationToken);

            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<TResult>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            WriteLogCritical(functionName, ex);
            return default;
        }
    }
    
    public async Task<TResult?> PostAsync<TResult>(string host, string path, string jsonContent, CancellationToken cancellationToken)
    {
        var requestUri = $"{host}/{path}";
        var functionName = $"{nameof(IHttpClientCustom)}_{requestUri}-{nameof(PostAsync)} => ";
        
        try
        {
            _logger.LogDebug($"{functionName}");
            
            StringContent? content = null;
            if (!string.IsNullOrEmpty(jsonContent))
            {
                content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            httpRequestMessage.Content = content;

            var httpClient = _httpClientFactory.CreateClient("EurekaMovieBE");
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            return JsonConvert.DeserializeObject<TResult>(result, settings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            WriteLogCritical(functionName, ex);
            return default;
        }
    }

    private void WriteLogCritical(string functionName, Exception ex)
    {
        if (ex.Message.Contains("Connection timed out"))
        {
            _logger.LogCritical(ex, $"{functionName} Has error: {ex.Message}");
        }
    }
}