using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMovieBE.Data.Configurations
{
    public class ApiResourceScopeConfiguration : IEntityTypeConfiguration<ApiResourceScope>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScope> builder)
        {
            builder.HasOne(x => x.ApiResource)
                .WithMany(x => x.ApiResourceScopes)
                .HasForeignKey(x => x.ApiResourceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
