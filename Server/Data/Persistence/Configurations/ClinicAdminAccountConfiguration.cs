using Data.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ClinicAdminAccountConfiguration : IEntityTypeConfiguration<ClinicAdminAccount>
{
    public void Configure(EntityTypeBuilder<ClinicAdminAccount> builder)
    {
        // Set up one-to-many relationship between ClinicAdminAccount and Clinic
        builder.HasMany(ca => ca.Clinics)
            .WithOne(c => c.ClinicAdminAccount)
            .HasForeignKey(c => c.ClinicAdminAccountId);
    }
}
