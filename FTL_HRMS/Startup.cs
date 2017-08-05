using System;
using Microsoft.Owin;
using Owin;
using FTL_HRMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(FTL_HRMS.Startup))]
namespace FTL_HRMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            HRMSDbContext db_ctx = new HRMSDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db_ctx));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db_ctx));


            // In Startup creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("System Admin"))
            {

                // first we create System Admin role   
                var role = new IdentityRole();
                role.Name = "System Admin";
                roleManager.Create(role);

                //Here we create a System Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "futuristic";
                user.Email = "systemadmin@futuristictech.xyz";
                string userPWD = "futureRistic@2017Sys";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "System Admin");

                }

                //first we create Admin role
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Super Admin";
                roleManager.Create(roleAdmin);

                //Here we create a Admin super user who will maintain the website                  
                var userAdmin = new ApplicationUser();
                userAdmin.UserName = "admin";
                userAdmin.Email = "admin@futuristictech.xyz";
                userPWD = "futureRistic@2017Ad";

                chkUser = UserManager.Create(userAdmin, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result2 = UserManager.AddToRole(user.Id, "Super Admin");
                }

                MenuItem MenuItem1 = new MenuItem();
                MenuItem1.Name = "Manage Menu";
                MenuItem1.ActionName = "Index";
                MenuItem1.ControllerName = "MenuItems";
                MenuItem1.IcnClass = "fa fa-file-archive-o";
                db_ctx.MenuItem.Add(MenuItem1);
                db_ctx.SaveChanges();

                MenuItem MenuItem2 = new MenuItem();
                MenuItem2.Name = "Add Menu Item";
                MenuItem2.ParentItemId = MenuItem1.Id;
                MenuItem2.ActionName = "Create";
                MenuItem2.ControllerName = "MenuItems";
                db_ctx.MenuItem.Add(MenuItem2);
                db_ctx.SaveChanges();

                MenuItem MenuItem3 = new MenuItem();
                MenuItem3.Name = "Menu List";
                MenuItem3.ParentItemId = MenuItem1.Id;
                MenuItem3.ActionName = "Index";
                MenuItem3.ControllerName = "MenuItems";
                db_ctx.MenuItem.Add(MenuItem3);
                db_ctx.SaveChanges();


                MenuItem MenuItem4 = new MenuItem();
                MenuItem4.Name = "Role Management";
                MenuItem4.ActionName = "#";
                MenuItem4.IcnClass = "fa fa-th";
                MenuItem4.ControllerName = "MenuItems";
                db_ctx.MenuItem.Add(MenuItem4);
                db_ctx.SaveChanges();

                MenuItem MenuItem5 = new MenuItem();
                MenuItem5.Name = "Add Role";
                MenuItem5.ParentItemId = MenuItem4.Id;
                MenuItem5.ActionName = "AddRole";
                MenuItem5.ControllerName = "Roles";
                db_ctx.MenuItem.Add(MenuItem5);
                db_ctx.SaveChanges();

                MenuItem MenuItem6 = new MenuItem();
                MenuItem6.Name = "Role List";
                MenuItem6.ParentItemId = MenuItem4.Id;
                MenuItem6.ActionName = "RoleList";
                MenuItem6.ControllerName = "Roles";
                db_ctx.MenuItem.Add(MenuItem6);
                db_ctx.SaveChanges();

                MenuItem MenuItem7 = new MenuItem();
                MenuItem7.Name = "Add User";
                MenuItem7.ParentItemId = MenuItem4.Id;
                MenuItem7.ActionName = "UserManagement";
                MenuItem7.ControllerName = "Roles";
                db_ctx.MenuItem.Add(MenuItem7);
                db_ctx.SaveChanges();

                List<int> MenuItemList = new List<int>();

                MenuItemList = db_ctx.MenuItem.Select(t => t.Id).ToList();

                RolePermission RolePermission = new RolePermission();

                RolePermission.RoleId = roleManager.FindByName("System Admin").Id;
                RolePermission.MenuItemIdList = string.Join(",", MenuItemList);
                db_ctx.RolePermission.Add(RolePermission);
                db_ctx.SaveChanges();
            }
        }
    }
}
