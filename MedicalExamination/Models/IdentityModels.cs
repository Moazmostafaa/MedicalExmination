using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExamination.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public int CityId { get; set; }
        public string Gender { get; set; }
        public String BirthDate { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public String UserType { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Governorate> Governorates { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Doctor.Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor.Hospital> Hospitals { get; set; }

        public DbSet<Doctor.Clinic> Clinics { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Doctor.DoctorHospital> DoctorHospitals { get; set; }

        public DbSet<Doctor.Rating> Ratings { get; set; }

        public DbSet<TestAndDisease.Disease> Diseases{ get; set; }
        public DbSet<TestAndDisease.DiseaseSymptoms> DiseaseSymptoms{ get; set; }
        public DbSet<TestAndDisease.Symptoms> Symptoms{ get; set; }
        public DbSet<TestAndDisease.Test> Tests{ get; set; }
        public DbSet<TestAndDisease.TestSymptoms> TestSymptoms{ get; set; }
    }
}