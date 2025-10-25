using Data.Domain;
using Data.Domain.Account;
using Data.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new AccountConfiguration().Configure(modelBuilder.Entity<Account>());
        new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
        new ClinicAdminAccountConfiguration().Configure(modelBuilder.Entity<ClinicAdminAccount>());
        new ClinicConfiguration().Configure(modelBuilder.Entity<Clinic>());
        new ReviewConfiguration().Configure(modelBuilder.Entity<Review>());
        new UserAccountConfiguration().Configure(modelBuilder.Entity<UserAccount>());
    }
}
