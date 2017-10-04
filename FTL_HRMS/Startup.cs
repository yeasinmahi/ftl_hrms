using Microsoft.Owin;
using Owin;
using FTL_HRMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using MenuItem = FTL_HRMS.Models.MenuItem;

[assembly: OwinStartupAttribute(typeof(FTL_HRMS.Startup))]
namespace FTL_HRMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CheckDatabaseConnection();
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            HRMSDbContext dbCtx = new HRMSDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbCtx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbCtx));


            // In Startup creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("System Admin"))
            {

                // first we create System Admin role   
                var role = new IdentityRole {Name = "System Admin"};
                roleManager.Create(role);

                //Here we create a System Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    UserName = "futuristic",
                    Email = "systemadmin@futuristictech.xyz"
                };
                string userPwd = "futureRistic@2017Sys";

                var chkUser = userManager.Create(user, userPwd);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "System Admin");
                }

                //first we create Admin role
                var roleAdmin = new IdentityRole {Name = "Super Admin"};
                roleManager.Create(roleAdmin);

                //Here we create a Admin super user who will maintain the website                  
                var userAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@futuristictech.xyz"
                };
                userPwd = "futureRistic@2017Ad";

                chkUser = userManager.Create(userAdmin, userPwd);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Super Admin");
                }

                MenuItem menuItem1 = new MenuItem
                {
                    Name = "Manage Menu",
                    ActionName = "Index",
                    ControllerName = "MenuItems",
                    IcnClass = "fa fa-file-archive-o"
                };
                dbCtx.MenuItem.Add(menuItem1);
                dbCtx.SaveChanges();

                MenuItem menuItem2 = new MenuItem
                {
                    Name = "Add Menu Item",
                    ParentItemId = menuItem1.Id,
                    ActionName = "Create",
                    ControllerName = "MenuItems"
                };
                dbCtx.MenuItem.Add(menuItem2);
                dbCtx.SaveChanges();

                MenuItem menuItem3 = new MenuItem
                {
                    Name = "Menu List",
                    ParentItemId = menuItem1.Id,
                    ActionName = "Index",
                    ControllerName = "MenuItems"
                };
                dbCtx.MenuItem.Add(menuItem3);
                dbCtx.SaveChanges();


                MenuItem menuItem4 = new MenuItem
                {
                    Name = "Role Management",
                    ActionName = "#",
                    IcnClass = "fa fa-th",
                    ControllerName = "MenuItems"
                };
                dbCtx.MenuItem.Add(menuItem4);
                dbCtx.SaveChanges();

                MenuItem menuItem5 = new MenuItem
                {
                    Name = "Add Role",
                    ParentItemId = menuItem4.Id,
                    ActionName = "AddRole",
                    ControllerName = "Roles"
                };
                dbCtx.MenuItem.Add(menuItem5);
                dbCtx.SaveChanges();

                MenuItem menuItem6 = new MenuItem
                {
                    Name = "Role List",
                    ParentItemId = menuItem4.Id,
                    ActionName = "RoleList",
                    ControllerName = "Roles"
                };
                dbCtx.MenuItem.Add(menuItem6);
                dbCtx.SaveChanges();

                MenuItem menuItem7 = new MenuItem
                {
                    Name = "Add User",
                    ParentItemId = menuItem4.Id,
                    ActionName = "UserManagement",
                    ControllerName = "Roles"
                };
                dbCtx.MenuItem.Add(menuItem7);
                dbCtx.SaveChanges();

                List<int> menuItemList = dbCtx.MenuItem.Select(t => t.Id).ToList();

                RolePermission rolePermission = new RolePermission
                {
                    RoleId = roleManager.FindByName("System Admin").Id,
                    MenuItemIdList = string.Join(",", menuItemList)
                };

                dbCtx.RolePermission.Add(rolePermission);
                dbCtx.SaveChanges();
            }
        }
    }
}
