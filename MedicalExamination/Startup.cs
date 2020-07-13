using MedicalExamination.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(MedicalExamination.Startup))]
namespace MedicalExamination
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateDefaultRolesAndUSers();
        }

        public void CreateDefaultRolesAndUSers()
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser();

            user.UserName = "admin@gmail.com";
            user.Email = "admin@gmail.com";
            user.UserType = "Admin";
            user.CityId = 1;
            user.BirthDate = "28/3/1997";
            user.Gender = "ذكر";
            user.PhoneNumber = "011244258468";
            var check = usermanager.Create(user, "Ad!123");
        }

        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;

            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Guest"))
            {
                role = new IdentityRole();
                role.Name = "Guest";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("مريض"))
            {
                role = new IdentityRole();
                role.Name = "مريض";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("دكتور"))
            {
                role = new IdentityRole();
                role.Name = "دكتور";
                roleManager.Create(role);
            }
        }
    }
}
