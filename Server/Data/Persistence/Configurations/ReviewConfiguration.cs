using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Set primary key
        builder.HasKey(r => r.ReviewId);

        builder.Property(r => r.Comment)
            .HasMaxLength(1000);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Date)
            .IsRequired();

        // Set Review - UserAccount relationship
        builder.HasOne(r => r.UserAccount)
            .WithMany(ua => ua.Reviews)
            .HasForeignKey(r => r.UserAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        // Set Review - Clinic relationship
        builder.HasOne(r => r.Clinic)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.ClinicId)
            .OnDelete(DeleteBehavior.Cascade);    
    }
}
