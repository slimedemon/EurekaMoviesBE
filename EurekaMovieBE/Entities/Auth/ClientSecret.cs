namespace EurekaMovieBE.Entities.Auth
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public string Secrets { get; set; } = default!;
        public int ClientId { get; set; }
    }
}
