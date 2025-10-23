namespace EurekaMovieBE.Entities.Auth
{
    public class ApiResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;

        public virtual ICollection<ApiResourceScope> ApiResourceScopes { get; set; } = [];
    }
}
