using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        // Set primary key
        builder.HasKey(c => c.ClinicId);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Rating)
            .IsRequired();

        // Set Clinic - Address relationship
        builder.HasOne(c => c.Address)
            .WithMany(a => a.Clinics)
            .HasForeignKey(c => c.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        // Set Clinic - Reviews relationship
        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Clinic)
            .HasForeignKey(r => r.ClinicId)
            .OnDelete(DeleteBehavior.Cascade);

        // Set Clinic - ClinicAdminAccount relationship
        builder.HasOne(c => c.ClinicAdminAccount)
            .WithMany(cac => cac.Clinics)
            .HasForeignKey(c => c.ClinicAdminAccountId);
    }
}
