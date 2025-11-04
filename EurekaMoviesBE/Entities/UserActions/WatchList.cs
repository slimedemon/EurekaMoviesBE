namespace EurekaMoviesBE.Entities.UserActions
{
    public class WatchList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Index { get; set; }
        public Guid UserId { get; set; }
        public long TmdbId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual UserInfo UserInfo { get; set; } = null!;
    }
}
