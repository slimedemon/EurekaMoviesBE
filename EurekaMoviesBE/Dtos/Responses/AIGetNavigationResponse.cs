namespace EurekaMoviesBE.Dtos.Responses;

public class AIGetNavigationResponse : AIBaseResponse<AIGetNavigationData>
{
    
}

public class AIGetNavigationData
{
    public string Route { get; set; }
    public NavigationParamData? Params { get; set; }
    public object Metadata { get; set; }
    public bool IsSuccess { get; set; }
}

public class NavigationParamData 
{
    public string? Keyword { get; set; }
    public List<string>? MovieIds { get; set; }
    public List<string>? GenresId { get; set; }
}
