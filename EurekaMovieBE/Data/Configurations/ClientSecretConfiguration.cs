using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMovieBE.Data.Configurations
{
    public class ClientSecretConfiguration: IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> builder)
        {
            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientSecrets)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
