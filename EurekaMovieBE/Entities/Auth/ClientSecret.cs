namespace EurekaMovieBE.Entities.Auth
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public string Secret { get; set; } = default!;
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = default!;
    }
}
