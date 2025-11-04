namespace EurekaMoviesBE.Entities.Auth
{
    public class User : IdentityUser
    {
        public override string Id { get; set; } = default!;
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
