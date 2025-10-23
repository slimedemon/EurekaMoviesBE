using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaMovieBE.Data.Configurations
{
    public class ClientScopeConfiguration: IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> builder)
        {
            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientScopes)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
