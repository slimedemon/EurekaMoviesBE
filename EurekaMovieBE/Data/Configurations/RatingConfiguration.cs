using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMovieBE.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable(nameof(Rating));
            builder.Property(x => x.Index).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone");
            builder.HasOne(x => x.UserInfo)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId);
        }
    }
}
