using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMoviesBE.Data.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable(nameof(Favorite));
            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone");
            builder.Property(x => x.Index).ValueGeneratedOnAdd();
            builder.HasOne(x => x.UserInfo)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
