using Data.Domain.Account;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Account> builder)
    {
        // Set the primary key
        builder.HasKey(a => a.AccountId);

        builder.Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.PasswordHash)
            .IsRequired()
            .HasMaxLength(512);

        // Unique constraint on Email
        builder.HasIndex(a => a.Email)
            .IsUnique();
    }
}
