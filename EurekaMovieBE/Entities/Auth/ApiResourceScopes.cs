namespace EurekaMovieBE.Entities.Auth
{
    public class ApiResourceScopes
    {
        public int Id { get; set; }
        public string Scope { get; set; } = default!;
        public int ApiResourceId { get; set; }
    }
}
