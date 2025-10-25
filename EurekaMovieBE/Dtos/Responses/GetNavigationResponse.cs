using System.Text.Json.Serialization;
using EurekaMovieBE.Dtos.Responses.AIService;

namespace EurekaMovieBE.Dtos.Responses;

public class GetNavigationResponse : BaseResponse
{
    public GetNavigationData Data { get; set; }
}

public class GetNavigationData
{
    public string Route { get; set; }
    public NavigationParamData? Params { get; set; }
    public string? Metadata { get; set; }
    public bool IsSuccess { get; set; }
}

public class NavigationMetadata
{
    public NavigationTitle Title { get; set; }
}

public class NavigationTitle
{
    [JsonPropertyName("$regex")]
    public string Regex { get; set; }

    [JsonPropertyName("$options")]
    public string Options { get; set; }
}