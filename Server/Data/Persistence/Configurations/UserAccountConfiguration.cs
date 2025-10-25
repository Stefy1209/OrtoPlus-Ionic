using Data.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.HasMany(ua => ua.Reviews)
            .WithOne(r => r.UserAccount)
            .HasForeignKey(r => r.UserAccountId);
    }
}
