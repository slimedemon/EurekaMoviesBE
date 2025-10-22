using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaMovieBE.Entities.User
{
    public class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Index { get; set; }
        public Guid UserId { get; set; }
        public long TmdbId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual UserInfo UserInfo { get; set; } = default!;
    }
}
