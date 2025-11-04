namespace EurekaMoviesBE.Dtos.Responses;

public class AIBaseResponse<T>
{
    public int Status { get; set; }
    public T Data { get; set; }
}