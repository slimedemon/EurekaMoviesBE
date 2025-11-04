namespace EurekaMoviesBE.Entities.UserActions
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string Avatar { get; set; } = default!;
        public DateTime? Birth { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; } = default!;
        public virtual ICollection<Rating> Ratings { get; set; } = default!;
        public virtual ICollection<WatchList> WatchLists { get; set; } = default!;
    }
}
