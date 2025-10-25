using Data.Domain;
using Data.Domain.Account;
using Data.Persistence;

namespace Data.Persistence.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (context.Set<Clinic>().Any())
        {
            return; // Database already seeded
        }

        // Seed Addresses
        var address1 = new Address(
            street: "Str. Memorandumului 28",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400114",
            country: "Romania"
        );

        var address2 = new Address(
            street: "Str. Avram Iancu 15",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400098",
            country: "Romania"
        );

        var address3 = new Address(
            street: "Calea Turzii 123",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400495",
            country: "Romania"
        );

        await context.Set<Address>().AddRangeAsync(new[] { address1, address2, address3 });
        await context.SaveChangesAsync();

        // Seed Clinic Admin Accounts first (because Clinic needs their IDs)
        var admin1 = new ClinicAdminAccount(
            Username: "adrian.moldovan",
            Email: "admin@ortopluscentral.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv" // Example bcrypt hash
        );

        var admin2 = new ClinicAdminAccount(
            Username: "elena.georgescu",
            Email: "admin@dentalsmile.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin3 = new ClinicAdminAccount(
            Username: "mihai.stan",
            Email: "admin@ortodentpro.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        await context.Set<ClinicAdminAccount>().AddRangeAsync(new[] { admin1, admin2, admin3 });
        await context.SaveChangesAsync();

        // Seed Clinics with Admin IDs
        var clinic1 = new Clinic(
            name: "OrtoPlus Cluj Central",
            addressId: address1.AddressId,
            clinicAdminAccountId: admin1.AccountId
        );

        var clinic2 = new Clinic(
            name: "Dental Smile Clinic",
            addressId: address2.AddressId,
            clinicAdminAccountId: admin2.AccountId
        );

        var clinic3 = new Clinic(
            name: "OrtoDent Pro",
            addressId: address3.AddressId,
            clinicAdminAccountId: admin3.AccountId
        );

        await context.Set<Clinic>().AddRangeAsync(new[] { clinic1, clinic2, clinic3 });
        await context.SaveChangesAsync();

        // Seed User Accounts
        var user1 = new UserAccount(
            Username: "ion.popescu",
            Email: "ion.popescu@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user2 = new UserAccount(
            Username: "maria.ionescu",
            Email: "maria.ionescu@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user3 = new UserAccount(
            Username: "andrei.dumitru",
            Email: "andrei.dumitru@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        await context.Set<UserAccount>().AddRangeAsync(new[] { user1, user2, user3 });
        await context.SaveChangesAsync();

        // Seed Reviews
        var review1 = new Review(
            comment: "Experiență excelentă! Personalul foarte profesionist și amabil.",
            rating: 5,
            userAccountId: user1.AccountId,
            clinicId: clinic1.ClinicId
        );

        var review2 = new Review(
            comment: "Servicii de calitate, puțin mai scump decât alte clinici.",
            rating: 4,
            userAccountId: user2.AccountId,
            clinicId: clinic1.ClinicId
        );

        var review3 = new Review(
            comment: "Recomand cu încredere! Doctorul a fost foarte atent.",
            rating: 5,
            userAccountId: user3.AccountId,
            clinicId: clinic2.ClinicId
        );

        var review4 = new Review(
            comment: "Echipamente moderne și ambianță plăcută.",
            rating: 4,
            userAccountId: user1.AccountId,
            clinicId: clinic3.ClinicId
        );

        await context.Set<Review>().AddRangeAsync([review1, review2, review3, review4]);
        await context.SaveChangesAsync();
    }
}