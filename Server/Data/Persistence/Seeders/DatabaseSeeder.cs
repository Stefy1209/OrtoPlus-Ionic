using Data.Domain;
using Data.Domain.Account;

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

        // Seed Addresses (25 addresses)
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

        var address4 = new Address(
            street: "Str. Republicii 45",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400015",
            country: "Romania"
        );

        var address5 = new Address(
            street: "Calea Dorobanților 89",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400609",
            country: "Romania"
        );

        var address6 = new Address(
            street: "Str. Horea 12",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400174",
            country: "Romania"
        );

        var address7 = new Address(
            street: "Bulevardul 21 Decembrie 1989 nr. 67",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400124",
            country: "Romania"
        );

        var address8 = new Address(
            street: "Str. Motilor 33",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400001",
            country: "Romania"
        );

        var address9 = new Address(
            street: "Calea Florești 156",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400524",
            country: "Romania"
        );

        var address10 = new Address(
            street: "Str. Iuliu Maniu 2-4",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400347",
            country: "Romania"
        );

        var address11 = new Address(
            street: "Str. Observatorului 72",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400363",
            country: "Romania"
        );

        var address12 = new Address(
            street: "Str. Pasteur 6",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400349",
            country: "Romania"
        );

        var address13 = new Address(
            street: "Calea Manastur 81",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400658",
            country: "Romania"
        );

        var address14 = new Address(
            street: "Str. Fabricii 3-5",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400632",
            country: "Romania"
        );

        var address15 = new Address(
            street: "Str. Clinicilor 38",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400006",
            country: "Romania"
        );

        var address16 = new Address(
            street: "Str. Alverna 14",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400125",
            country: "Romania"
        );

        var address17 = new Address(
            street: "Bulevardul Eroilor 29",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400129",
            country: "Romania"
        );

        var address18 = new Address(
            street: "Str. Plopilor 17",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400157",
            country: "Romania"
        );

        var address19 = new Address(
            street: "Str. Fagului 22",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400471",
            country: "Romania"
        );

        var address20 = new Address(
            street: "Calea Baciului 95",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400223",
            country: "Romania"
        );

        var address21 = new Address(
            street: "Str. Mehedinți 31",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400664",
            country: "Romania"
        );

        var address22 = new Address(
            street: "Str. Primaverii 8",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400508",
            country: "Romania"
        );

        var address23 = new Address(
            street: "Str. Brancusi 42",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400462",
            country: "Romania"
        );

        var address24 = new Address(
            street: "Calea Someseni 67",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400412",
            country: "Romania"
        );

        var address25 = new Address(
            street: "Str. Aurel Vlaicu 50",
            city: "Cluj-Napoca",
            state: "Cluj",
            zipCode: "400340",
            country: "Romania"
        );

        await context.Set<Address>().AddRangeAsync(new[] { 
            address1, address2, address3, address4, address5, 
            address6, address7, address8, address9, address10,
            address11, address12, address13, address14, address15,
            address16, address17, address18, address19, address20,
            address21, address22, address23, address24, address25
        });
        await context.SaveChangesAsync();

        // Seed Clinic Admin Accounts (25 admins)
        var admin1 = new ClinicAdminAccount(
            Username: "adrian.moldovan",
            Email: "admin@ortopluscentral.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin2 = new ClinicAdminAccount(
            Username: "elena.georgescu",
            Email: "admin@ortomed.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin3 = new ClinicAdminAccount(
            Username: "mihai.stan",
            Email: "admin@orthopedicpro.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin4 = new ClinicAdminAccount(
            Username: "ana.radu",
            Email: "admin@ortopediecluj.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin5 = new ClinicAdminAccount(
            Username: "vasile.popa",
            Email: "admin@ortosport.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin6 = new ClinicAdminAccount(
            Username: "laura.cristea",
            Email: "admin@orthopedicmed.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin7 = new ClinicAdminAccount(
            Username: "cristian.munteanu",
            Email: "admin@ortolife.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin8 = new ClinicAdminAccount(
            Username: "ioana.matei",
            Email: "admin@traumaorto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin9 = new ClinicAdminAccount(
            Username: "george.lungu",
            Email: "admin@ortocare.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin10 = new ClinicAdminAccount(
            Username: "diana.stoica",
            Email: "admin@recuperareorto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin11 = new ClinicAdminAccount(
            Username: "florin.dobre",
            Email: "admin@ortoexpert.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin12 = new ClinicAdminAccount(
            Username: "simona.barbu",
            Email: "admin@kineto-orto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin13 = new ClinicAdminAccount(
            Username: "robert.constantinescu",
            Email: "admin@ortopedic-center.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin14 = new ClinicAdminAccount(
            Username: "andreea.florea",
            Email: "admin@ortosanatos.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin15 = new ClinicAdminAccount(
            Username: "dan.marinescu",
            Email: "admin@clinica-ortopedica.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin16 = new ClinicAdminAccount(
            Username: "raluca.nica",
            Email: "admin@ortomedica.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin17 = new ClinicAdminAccount(
            Username: "paul.vasilescu",
            Email: "admin@ortopremium.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin18 = new ClinicAdminAccount(
            Username: "monica.escu",
            Email: "admin@centrulorto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin19 = new ClinicAdminAccount(
            Username: "alexandru.grigore",
            Email: "admin@ortokineto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin20 = new ClinicAdminAccount(
            Username: "gabriela.serban",
            Email: "admin@traumatorto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin21 = new ClinicAdminAccount(
            Username: "stefan.radulescu",
            Email: "admin@ortomed-plus.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin22 = new ClinicAdminAccount(
            Username: "corina.nicolescu",
            Email: "admin@recuperare-medical.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin23 = new ClinicAdminAccount(
            Username: "victor.andrei",
            Email: "admin@sportorto.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin24 = new ClinicAdminAccount(
            Username: "claudia.manea",
            Email: "admin@orto-clinic.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var admin25 = new ClinicAdminAccount(
            Username: "razvan.popovici",
            Email: "admin@mediortho.ro",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        await context.Set<ClinicAdminAccount>().AddRangeAsync(new[] { 
            admin1, admin2, admin3, admin4, admin5, 
            admin6, admin7, admin8, admin9, admin10,
            admin11, admin12, admin13, admin14, admin15,
            admin16, admin17, admin18, admin19, admin20,
            admin21, admin22, admin23, admin24, admin25
        });
        await context.SaveChangesAsync();

        // Seed Clinics (25 clinics)
        var clinic1 = new Clinic(
            name: "OrtoPlus Cluj Central",
            addressId: address1.AddressId,
            clinicAdminAccountId: admin1.AccountId
        );

        var clinic2 = new Clinic(
            name: "OrtoMed Clinic",
            addressId: address2.AddressId,
            clinicAdminAccountId: admin2.AccountId
        );

        var clinic3 = new Clinic(
            name: "Centrul Orthopedic Pro",
            addressId: address3.AddressId,
            clinicAdminAccountId: admin3.AccountId
        );

        var clinic4 = new Clinic(
            name: "Clinica de Ortopedie Cluj",
            addressId: address4.AddressId,
            clinicAdminAccountId: admin4.AccountId
        );

        var clinic5 = new Clinic(
            name: "OrtoSport Recovery Center",
            addressId: address5.AddressId,
            clinicAdminAccountId: admin5.AccountId
        );

        var clinic6 = new Clinic(
            name: "Centrul Medical Orthopedic",
            addressId: address6.AddressId,
            clinicAdminAccountId: admin6.AccountId
        );

        var clinic7 = new Clinic(
            name: "OrtoLife Clinic",
            addressId: address7.AddressId,
            clinicAdminAccountId: admin7.AccountId
        );

        var clinic8 = new Clinic(
            name: "Clinica de Traumatologie și Ortopedie",
            addressId: address8.AddressId,
            clinicAdminAccountId: admin8.AccountId
        );

        var clinic9 = new Clinic(
            name: "OrtoCare Medical Center",
            addressId: address9.AddressId,
            clinicAdminAccountId: admin9.AccountId
        );

        var clinic10 = new Clinic(
            name: "Centrul de Recuperare Orthopedică",
            addressId: address10.AddressId,
            clinicAdminAccountId: admin10.AccountId
        );

        var clinic11 = new Clinic(
            name: "OrtoExpert Cluj",
            addressId: address11.AddressId,
            clinicAdminAccountId: admin11.AccountId
        );

        var clinic12 = new Clinic(
            name: "Kineto-Orto Medical",
            addressId: address12.AddressId,
            clinicAdminAccountId: admin12.AccountId
        );

        var clinic13 = new Clinic(
            name: "Centrul Ortopedic Manastur",
            addressId: address13.AddressId,
            clinicAdminAccountId: admin13.AccountId
        );

        var clinic14 = new Clinic(
            name: "OrtoSănătos",
            addressId: address14.AddressId,
            clinicAdminAccountId: admin14.AccountId
        );

        var clinic15 = new Clinic(
            name: "Clinica Ortopedică Cluj",
            addressId: address15.AddressId,
            clinicAdminAccountId: admin15.AccountId
        );

        var clinic16 = new Clinic(
            name: "OrtoMedica Plus",
            addressId: address16.AddressId,
            clinicAdminAccountId: admin16.AccountId
        );

        var clinic17 = new Clinic(
            name: "OrtoPremium Clinic",
            addressId: address17.AddressId,
            clinicAdminAccountId: admin17.AccountId
        );

        var clinic18 = new Clinic(
            name: "Centrul OrtoTrauma",
            addressId: address18.AddressId,
            clinicAdminAccountId: admin18.AccountId
        );

        var clinic19 = new Clinic(
            name: "OrtoKineto Center",
            addressId: address19.AddressId,
            clinicAdminAccountId: admin19.AccountId
        );

        var clinic20 = new Clinic(
            name: "Clinica de Traumatologie Baciului",
            addressId: address20.AddressId,
            clinicAdminAccountId: admin20.AccountId
        );

        var clinic21 = new Clinic(
            name: "OrtoMed Plus",
            addressId: address21.AddressId,
            clinicAdminAccountId: admin21.AccountId
        );

        var clinic22 = new Clinic(
            name: "Centrul de Recuperare Medicală",
            addressId: address22.AddressId,
            clinicAdminAccountId: admin22.AccountId
        );

        var clinic23 = new Clinic(
            name: "SportOrto Performance Clinic",
            addressId: address23.AddressId,
            clinicAdminAccountId: admin23.AccountId
        );

        var clinic24 = new Clinic(
            name: "Orto-Clinic Someseni",
            addressId: address24.AddressId,
            clinicAdminAccountId: admin24.AccountId
        );

        var clinic25 = new Clinic(
            name: "MediOrtho Center",
            addressId: address25.AddressId,
            clinicAdminAccountId: admin25.AccountId
        );

        await context.Set<Clinic>().AddRangeAsync(new[] { 
            clinic1, clinic2, clinic3, clinic4, clinic5, 
            clinic6, clinic7, clinic8, clinic9, clinic10,
            clinic11, clinic12, clinic13, clinic14, clinic15,
            clinic16, clinic17, clinic18, clinic19, clinic20,
            clinic21, clinic22, clinic23, clinic24, clinic25
        });
        await context.SaveChangesAsync();

        // Seed User Accounts (12 users)
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

        var user4 = new UserAccount(
            Username: "carmen.vasile",
            Email: "carmen.vasile@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user5 = new UserAccount(
            Username: "bogdan.mihai",
            Email: "bogdan.mihai@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user6 = new UserAccount(
            Username: "alexandra.ene",
            Email: "alexandra.ene@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user7 = new UserAccount(
            Username: "daniel.petre",
            Email: "daniel.petre@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user8 = new UserAccount(
            Username: "cristina.barbu",
            Email: "cristina.barbu@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user9 = new UserAccount(
            Username: "gabriel.stanciu",
            Email: "gabriel.stanciu@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user10 = new UserAccount(
            Username: "teodora.lazar",
            Email: "teodora.lazar@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user11 = new UserAccount(
            Username: "cosmin.Tudor",
            Email: "cosmin.tudor@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        var user12 = new UserAccount(
            Username: "bianca.paun",
            Email: "bianca.paun@email.com",
            PasswordHash: "$2a$11$abcdefghijklmnopqrstuv"
        );

        await context.Set<UserAccount>().AddRangeAsync(new[] { 
            user1, user2, user3, user4, user5, user6,
            user7, user8, user9, user10, user11, user12
        });
        await context.SaveChangesAsync();

        // Seed Reviews (40 reviews distributed across clinics)
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
            comment: "Recomand cu încredere! Doctorul a fost foarte atent la problemele mele de genunchi.",
            rating: 5,
            userAccountId: user3.AccountId,
            clinicId: clinic2.ClinicId
        );

        var review4 = new Review(
            comment: "Echipamente moderne și fizioterapie de calitate.",
            rating: 4,
            userAccountId: user1.AccountId,
            clinicId: clinic3.ClinicId
        );

        var review5 = new Review(
            comment: "Foarte mulțumită de recuperarea post-operatorie. Kinetoterapeuții sunt profesioniști.",
            rating: 5,
            userAccountId: user4.AccountId,
            clinicId: clinic4.ClinicId
        );

        var review6 = new Review(
            comment: "Bună clinică pentru traumatologie, dar programările sunt dificil de obținut.",
            rating: 3,
            userAccountId: user5.AccountId,
            clinicId: clinic4.ClinicId
        );

        var review7 = new Review(
            comment: "Specializat pentru sportivi! M-au ajutat să revin rapid după accidentare.",
            rating: 5,
            userAccountId: user6.AccountId,
            clinicId: clinic5.ClinicId
        );

        var review8 = new Review(
            comment: "Raport calitate-preț foarte bun pentru consultații ortopedice.",
            rating: 4,
            userAccountId: user2.AccountId,
            clinicId: clinic6.ClinicId
        );

        var review9 = new Review(
            comment: "Clinică modernă cu specialiști în chirurgie ortopedică.",
            rating: 5,
            userAccountId: user3.AccountId,
            clinicId: clinic7.ClinicId
        );

        var review10 = new Review(
            comment: "Am avut o experiență plăcută, dar așteptarea pentru RMN a fost lungă.",
            rating: 3,
            userAccountId: user4.AccountId,
            clinicId: clinic7.ClinicId
        );

        var review11 = new Review(
            comment: "Tratament pentru hernie de disc impecabil! Recomand din suflet.",
            rating: 5,
            userAccountId: user1.AccountId,
            clinicId: clinic8.ClinicId
        );

        var review12 = new Review(
            comment: "Echipă tânără și specializată în artroscopie, rezultate bune.",
            rating: 4,
            userAccountId: user5.AccountId,
            clinicId: clinic9.ClinicId
        );

        var review13 = new Review(
            comment: "Locație convenabilă și program de recuperare personalizat excelent.",
            rating: 5,
            userAccountId: user6.AccountId,
            clinicId: clinic10.ClinicId
        );

        var review14 = new Review(
            comment: "Prețuri competitive și rezultate vizibile după câteva ședințe de kinetoterapie.",
            rating: 4,
            userAccountId: user2.AccountId,
            clinicId: clinic10.ClinicId
        );

        var review15 = new Review(
            comment: "Medicul ortoped a fost foarte atent și mi-a explicat totul în detaliu.",
            rating: 5,
            userAccountId: user7.AccountId,
            clinicId: clinic11.ClinicId
        );

        var review16 = new Review(
            comment: "Excelentă clinică pentru recuperare după fracturi. Recomand!",
            rating: 5,
            userAccountId: user8.AccountId,
            clinicId: clinic12.ClinicId
        );

        var review17 = new Review(
            comment: "Personal prietenos și cabinet bine dotat.",
            rating: 4,
            userAccountId: user9.AccountId,
            clinicId: clinic13.ClinicId
        );

        var review18 = new Review(
            comment: "M-au tratat pentru problemă de coloană și sunt foarte mulțumit.",
            rating: 5,
            userAccountId: user10.AccountId,
            clinicId: clinic14.ClinicId
        );

        var review19 = new Review(
            comment: "Clinica este curată, dar timpul de așteptare a fost lung.",
            rating: 3,
            userAccountId: user11.AccountId,
            clinicId: clinic15.ClinicId
        );

        var review20 = new Review(
            comment: "Tratament profesionist pentru dureri de umăr. Foarte mulțumit!",
            rating: 5,
            userAccountId: user12.AccountId,
            clinicId: clinic16.ClinicId
        );

        var review21 = new Review(
            comment: "Preț accesibil și echipă de specialiști dedicați.",
            rating: 4,
            userAccountId: user1.AccountId,
            clinicId: clinic17.ClinicId
        );

        var review22 = new Review(
            comment: "Cea mai bună clinică pentru sportivi din Cluj!",
            rating: 5,
            userAccountId: user7.AccountId,
            clinicId: clinic18.ClinicId
        );

        var review23 = new Review(
            comment: "Kinetoterapia aici m-a ajutat enorm după operație.",
            rating: 5,
            userAccountId: user8.AccountId,
            clinicId: clinic19.ClinicId
        );

        var review24 = new Review(
            comment: "Bună clinică, dar puțin aglomerată în anumite zile.",
            rating: 3,
            userAccountId: user9.AccountId,
            clinicId: clinic20.ClinicId
        );

        var review25 = new Review(
            comment: "Tratament complet pentru probleme de șold. Recomand!",
            rating: 5,
            userAccountId: user10.AccountId,
            clinicId: clinic21.ClinicId
        );

        var review26 = new Review(
            comment: "Personal calificat și abordare individualizată.",
            rating: 4,
            userAccountId: user11.AccountId,
            clinicId: clinic22.ClinicId
        );

        var review27 = new Review(
            comment: "Recuperare rapidă după accidentare sportivă. Mulțumesc!",
            rating: 5,
            userAccountId: user12.AccountId,
            clinicId: clinic23.ClinicId
        );

        var review28 = new Review(
            comment: "Clinică nouă cu echipamente de ultimă generație.",
            rating: 5,
            userAccountId: user3.AccountId,
            clinicId: clinic24.ClinicId
        );

        var review29 = new Review(
            comment: "Am fost tratat pentru entorsă și recuperarea a fost foarte bună.",
            rating: 4,
            userAccountId: user4.AccountId,
            clinicId: clinic25.ClinicId
        );

        var review30 = new Review(
            comment: "Doctor experimentat și echipă prietenoasă.",
            rating: 5,
            userAccountId: user5.AccountId,
            clinicId: clinic11.ClinicId
        );

        var review31 = new Review(
            comment: "Recomand pentru tratamente de coloană vertebrală.",
            rating: 4,
            userAccountId: user6.AccountId,
            clinicId: clinic12.ClinicId
        );

        var review32 = new Review(
            comment: "Expertiză în artroplastie. Operația a decurs perfect.",
            rating: 5,
            userAccountId: user7.AccountId,
            clinicId: clinic13.ClinicId
        );

        var review33 = new Review(
            comment: "Programare rapidă și diagnostic corect.",
            rating: 4,
            userAccountId: user8.AccountId,
            clinicId: clinic14.ClinicId
        );

        var review34 = new Review(
            comment: "Fizioterapie excelentă pentru recuperarea mobilității.",
            rating: 5,
            userAccountId: user9.AccountId,
            clinicId: clinic15.ClinicId
        );

        var review35 = new Review(
            comment: "Clinică profesionistă, dar prețurile sunt ridicate.",
            rating: 3,
            userAccountId: user10.AccountId,
            clinicId: clinic16.ClinicId
        );

        var review36 = new Review(
            comment: "Tratament pentru artroză foarte eficient!",
            rating: 5,
            userAccountId: user11.AccountId,
            clinicId: clinic17.ClinicId
        );

        var review37 = new Review(
            comment: "Echipă de traumatologi dedicați și competenți.",
            rating: 4,
            userAccountId: user12.AccountId,
            clinicId: clinic18.ClinicId
        );

        var review38 = new Review(
            comment: "Recuperare post-traumatică bine coordonată.",
            rating: 5,
            userAccountId: user1.AccountId,
            clinicId: clinic19.ClinicId
        );

        var review39 = new Review(
            comment: "Servicii complete de diagnostic și tratament ortopedic.",
            rating: 4,
            userAccountId: user2.AccountId,
            clinicId: clinic20.ClinicId
        );

        var review40 = new Review(
            comment: "Cea mai bună alegere pentru probleme de genunchi!",
            rating: 5,
            userAccountId: user3.AccountId,
            clinicId: clinic21.ClinicId
        );

        await context.Set<Review>().AddRangeAsync([
            review1, review2, review3, review4, review5, review6, review7,
            review8, review9, review10, review11, review12, review13, review14,
            review15, review16, review17, review18, review19, review20, review21,
            review22, review23, review24, review25, review26, review27, review28,
            review29, review30, review31, review32, review33, review34, review35,
            review36, review37, review38, review39, review40
        ]);
        await context.SaveChangesAsync();
    }
}