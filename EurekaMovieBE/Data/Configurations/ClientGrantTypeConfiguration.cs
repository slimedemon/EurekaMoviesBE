using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMovieBE.Data.Configurations
{
    public class ClientGrantTypeConfiguration: IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> builder)
        {
            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientGrantTypes)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
