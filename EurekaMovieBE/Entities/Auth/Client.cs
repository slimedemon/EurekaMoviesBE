namespace EurekaMovieBE.Entities.Auth
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientId { get; set; } = default!;
        public string ClientName { get; set; } = default!;
    }
}
