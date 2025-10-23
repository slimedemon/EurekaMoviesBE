namespace EurekaMovieBE.Entities.Auth
{
    public class ClientGrantTypes
    {
        public int Id { get; set; }
        public string GrantType { get; set; } = default!;
        public int ClientId { get; set; }
    }
}
