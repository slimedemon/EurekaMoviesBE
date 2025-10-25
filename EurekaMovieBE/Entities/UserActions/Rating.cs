namespace EurekaMovieBE.Entities.UserActions
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Index { get; set; }
        public double Star { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public long TmdbId { get; set; }
        public Guid UserId { get; set; }
        public virtual UserInfo UserInfo { get; set; } = default!;
    }
}
