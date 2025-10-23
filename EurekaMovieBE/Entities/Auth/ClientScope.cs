namespace EurekaMovieBE.Entities.Auth
{
    public class ClientScope
    {
        public int Id { get; set; }
        public string Scope { get; set; } = default!;
        public int ClientId { get; set; }
    }
}
