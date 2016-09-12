using BuySell.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuySell.WebUI.Startup))]
namespace BuySell.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role2 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role2.Name = "Manager";
                roleManager.Create(role2);
            }

            // creating Creating Seller role    
            if (!roleManager.RoleExists("Seller"))
            {
                var role3 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role3.Name = "Seller";
                roleManager.Create(role3);
            }
        }
    }
}
