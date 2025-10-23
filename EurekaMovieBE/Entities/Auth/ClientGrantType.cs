namespace EurekaMovieBE.Entities.Auth
{
    public class ClientGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; } = default!;
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = default!;
    }
}
