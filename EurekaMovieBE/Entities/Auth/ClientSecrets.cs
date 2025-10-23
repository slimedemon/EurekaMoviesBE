namespace EurekaMovieBE.Entities.Auth
{
    public class ClientSecrets
    {
        public int Id { get; set; }
        public string Secrets { get; set; } = default!;
        public int ClientId { get; set; }
    }
}
