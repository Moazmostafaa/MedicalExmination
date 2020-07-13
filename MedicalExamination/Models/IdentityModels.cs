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

        public System.Data.Entity.DbSet<MedicalExamination.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Governorate> Governorates { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Doctor.Doctor> Doctors { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Patient> Patients { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Doctor.Hospital> Hospitals { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Doctor.Clinic> Clinics { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Doctor.DoctorHospital> DoctorHospitals { get; set; }

        public System.Data.Entity.DbSet<MedicalExamination.Models.Doctor.Rating> Ratings { get; set; }
    }
}