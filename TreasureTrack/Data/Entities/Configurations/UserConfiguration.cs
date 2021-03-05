using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreasureTrack.Data.Entities.Configurations
{
    public class UserConfiguration  : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Enabled).HasDefaultValue(true);
        }
    }
}