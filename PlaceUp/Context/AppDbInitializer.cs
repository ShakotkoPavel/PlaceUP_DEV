using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlaceUp.Models;
using System.Data.Entity;

namespace PlaceUp.Context
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "pavelshakotko@gmail.com", UserName = "pavelshakotko@gmail.com" };
            string password = "@Qwerty12";

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }

            var admin2 = new ApplicationUser { Email = "kotsabiukmv98@ukr.net", UserName = "kotsabiukmv98@ukr.net" };
            string password2 = "@Qwerty13";

            var result2 = userManager.Create(admin2, password2);

            if (result2.Succeeded)
            {
                userManager.AddToRole(admin2.Id, role1.Name);
            }

            var admin3 = new ApplicationUser { Email = "zalisckyivan@gmail.com", UserName = "zalisckyivan@gmail.com" };
            string password3 = "@Qwerty14";

            var result3 = userManager.Create(admin3, password3);

            if (result3.Succeeded)
            {
                userManager.AddToRole(admin3.Id, role1.Name);
            }

            base.Seed(context);
        }
    }
}