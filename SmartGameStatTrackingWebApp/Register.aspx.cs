using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartGameStatTrackingWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGameStatTrackingWebApp
{
    public partial class Register : System.Web.UI.Page
    {
        private SportsTrackDBContext db = new SportsTrackDBContext();

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = UserName.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            //manager.AddToRole(user.Id, "Admin");

            //Create Profile
            Profiles Profile = new Profiles
            {
                UserName = UserName.Text,
                Email = Email.Text
            };
            db.Profiles.Add(Profile);
            db.SaveChanges();

            if (result.Succeeded)
            {
                StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void CreateRole(object sender, EventArgs e)
        {
            var roleStore = new RoleStore<IdentityRole>();
            var roleman = new RoleManager<IdentityRole>(roleStore);
            var roleresult = roleman.Create(new IdentityRole("Admin"));


            if (roleresult.Succeeded)
            {
                StatusMessage.Text = string.Format("Role created successfully!");
            }
            else
            {
                StatusMessage.Text = roleresult.Errors.FirstOrDefault();
            }
        }
    }
}