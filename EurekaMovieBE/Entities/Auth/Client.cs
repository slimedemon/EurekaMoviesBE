namespace EurekaMovieBE.Entities.Auth
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientId { get; set; } = default!;
        public string ClientName { get; set; } = default!;

        public virtual ICollection<ClientGrantType> ClientGrantTypes { get; set; } = [];
        public virtual ICollection<ClientScope> ClientScopes { get; set; } = [];
        public virtual ICollection<ClientSecret> ClientSecrets { get; set; } = [];
    }
}
