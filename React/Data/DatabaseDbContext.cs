using React.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    public class DatabaseDbContext : IdentityDbContext<ApplicationUser>
    {
	public DbSet<DBPerson> People { get; set; }
	public DbSet<DBCity> Cities { get; set; }
	public DbSet<Country> Countries { get; set; }
	public DbSet<PersonLanguage> PersonLanguages { get; set; }
	public DbSet<DBLanguage> Languages { get; set; } 

	public DbSet<ApplicationUser> Users { get; set; }

	public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    base.OnModelCreating(modelBuilder);

	    // Setup PersonLanguage association table:
	    modelBuilder.Entity<PersonLanguage>().HasKey(pl => new { pl.PersonId, pl.LanguageId });
	    modelBuilder.Entity<PersonLanguage>()
		.HasOne(pl => pl.Person)
		.WithMany(person => person.Languages)
		.HasForeignKey(pl => pl.PersonId);
	    modelBuilder.Entity<PersonLanguage>()
		.HasOne(pl => pl.Language)
		.WithMany(language => language.People)
		.HasForeignKey(pl => pl.LanguageId);
	    modelBuilder.Entity<DBPerson>()
		.HasOne(person => person.City)
		.WithMany(city => city.People)
		.HasForeignKey(person => person.CityId);
	    modelBuilder.Entity<DBCity>()
		.HasOne(city => city.Country)
		.WithMany(country => country.Cities)
		.HasForeignKey(city => city.CountryId);

	    // Add countries:
	    modelBuilder.Entity<Country>().HasData(new Country { Id = 1, Name = "Sverige", CountryCode="SE" });
	    modelBuilder.Entity<Country>().HasData(new Country { Id = 2, Name = "Norge", CountryCode = "NO" });
	    modelBuilder.Entity<Country>().HasData(new Country { Id = 3, Name = "USA", CountryCode = "US" });

	    // Add cities:
	    modelBuilder.Entity<DBCity>().HasData(new DBCity { Id = 1, Name = "Lidköping", CountryId = 1 });
	    modelBuilder.Entity<DBCity>().HasData(new DBCity { Id = 2, Name = "Skövde", CountryId = 1 });
	    modelBuilder.Entity<DBCity>().HasData(new DBCity { Id = 3, Name = "Skara", CountryId = 1 });

	    // Add people:
	    modelBuilder.Entity<DBPerson>().HasData(new DBPerson { ID = 1, Name = "Niklas", CityId = 1, PhoneNumber="0510-28826" });
	    modelBuilder.Entity<DBPerson>().HasData(new DBPerson { ID = 2, Name = "Per", CityId = 2, PhoneNumber = "0500-85855" });
	    modelBuilder.Entity<DBPerson>().HasData(new DBPerson { ID = 3, Name = "Otto", CityId = 3, PhoneNumber = "0511-32448" });

	    // Add languages:
	    modelBuilder.Entity<DBLanguage>().HasData(new DBLanguage { Id = 1, Name = "Svenska" });
	    modelBuilder.Entity<DBLanguage>().HasData(new DBLanguage { Id = 2, Name = "Norska" });
	    modelBuilder.Entity<DBLanguage>().HasData(new DBLanguage { Id = 3, Name = "Danska" });
	    modelBuilder.Entity<DBLanguage>().HasData(new DBLanguage { Id = 4, Name = "Engelska (UK)" });
	    modelBuilder.Entity<DBLanguage>().HasData(new DBLanguage { Id = 5, Name = "Engelska (US)" });

	    // Associate people with a language:
	    modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage { PersonId = 1, LanguageId = 1 });
	    modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage { PersonId = 2, LanguageId = 1 });
	    modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage { PersonId = 3, LanguageId = 1 });

	    string adminRoleId = Guid.NewGuid().ToString();
	    string adminUserId = Guid.NewGuid().ToString();
	    string userRoleId = Guid.NewGuid().ToString();

	    // Add user roles:
	    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" });
	    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" });

	    var hasher = new PasswordHasher<ApplicationUser>();

	    // Add an admin user:
	    modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
	    {
		Id = adminUserId,
		Email = "admin@admin.com",
		NormalizedEmail = "ADMIN@ADMIN.COM",
		UserName = "Admin",
		NormalizedUserName = "ADMIN",
		PasswordHash = hasher.HashPassword(null, "virge3d"),
		FirstName = "Admin",
		LastName = "Admin",
		BirthDate="1998-01-01"
	    });

	    // Set user admin role to admin
	    modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
	    {
		RoleId = adminRoleId,
		UserId = adminUserId
	    });

	}
    }
}
