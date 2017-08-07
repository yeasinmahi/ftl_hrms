using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models;
using FTL_HRMS.ViewModels;
using System.Data.Entity;

namespace FTL_HRMS.Controllers
{
    public class RolesController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        HRMSDbContext db_ctx = new HRMSDbContext();

        UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

        public RolesController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HRMSDbContext())), new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext())))
        {
        }

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            var roles = db_ctx.Roles.ToList();
            return View(roles);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                db_ctx.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                db_ctx.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = db_ctx.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db_ctx.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db_ctx.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string RoleName)
        {
            var thisRole = db_ctx.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db_ctx.Roles.Remove(thisRole);
            db_ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserRoles()
        {
            var list = db_ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = db_ctx.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            account.UserManager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = db_ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = db_ctx.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();

                ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = db_ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            ApplicationUser user = db_ctx.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, RoleName))
            {
                account.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = db_ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }


        #region FTL_HRMS

        public ViewResult RoleList()
        {
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            List<IdentityRole> RoleList = new List<IdentityRole>();
            if (rolll == "System Admin")
            {
                RoleList = RoleManager.Roles.ToList();
            }
            else
            {
                RoleList = RoleManager.Roles.Where(t => t.Name != "System Admin").ToList();
            }




            return View("~/Views/RoleManagement/RoleList.cshtml", RoleList);
        }


        [HttpGet]
        public ActionResult AddRole()
        {
            FTL_HRMS.Models.HRMSDbContext db_ctx = new FTL_HRMS.Models.HRMSDbContext();
            List<FTL_HRMS.ViewModels.VMMenuByRole> MenuItemsForRole = new List<FTL_HRMS.ViewModels.VMMenuByRole>();

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            string role_id = db_ctx.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();

            FTL_HRMS.Models.RolePermission RolePermissions = db_ctx.RolePermission.Where(t => t.RoleId == role_id).FirstOrDefault();
            List<FTL_HRMS.Models.MenuItem> MenuItems = new List<FTL_HRMS.Models.MenuItem>();

            FTL_HRMS.Models.MenuItem MenuItemsForCurrentUser = new FTL_HRMS.Models.MenuItem();

            Dictionary<int, string> ParentMenuName = new Dictionary<int, string>();
            //string[] MenuIds = null;


            if (RolePermissions != null)
            {
                //MenuIds = RolePermissions.MenuItemIdList.Split(',');
                //MenuIds = comfortEMSContext.MenuItems.Select(t => t.Id).FirstOrDefault().ToString();

                //List<int> IntMenuIds = new List<int>();
                //foreach (string s in MenuIds)
                //{
                //    IntMenuIds.Add(Convert.ToInt32(s));
                //}

                List<MenuItem> MnuItemList = db_ctx.MenuItem.ToList();



                foreach (MenuItem mId in MnuItemList)
                {
                    MenuItemsForCurrentUser = db_ctx.MenuItem.Where(t => t.Id == mId.Id).FirstOrDefault();
                    if (MenuItemsForCurrentUser.ParentItemId != null)
                    {
                        ParentMenuName.Add(mId.Id, db_ctx.MenuItem.Where(w => w.Id == MenuItemsForCurrentUser.ParentItemId).FirstOrDefault().Name);

                    }

                    MenuItems.Add(MenuItemsForCurrentUser);
                }
            }
            foreach (var mi in MenuItems)
            {
                FTL_HRMS.ViewModels.VMMenuByRole MenuByRole = new FTL_HRMS.ViewModels.VMMenuByRole();
                MenuByRole.ActionName = mi.ActionName;
                MenuByRole.ControllerName = mi.ControllerName;
                MenuByRole.MenuItemName = mi.Name;
                MenuByRole.Id = mi.Id;
                MenuByRole.ParentId = mi.ParentItemId;
                MenuByRole.FunctionNames = mi.AllFunctions;
                MenuByRole.ViewNames = mi.ViewNames;
                // MenuByRole.MenuOrder = mi.MenuOrder;
                MenuByRole.ParentMenuName = mi.ParentItemId != null ? ParentMenuName[mi.Id] : "";
                MenuItemsForRole.Add(MenuByRole);
            }


            return View("~/Views/RoleManagement/AddRole.cshtml", MenuItemsForRole);

        }




        [HttpPost]
        public ActionResult AddNewRole()
        {

            //var res = Request.Form["MenuItems[2].Id"];
            string StatusMsg = string.Empty;

            string RoleName = Convert.ToString(Request["RoleName"]);
            string TotalSelectedItem = Convert.ToString(Request["TotalMnuItem"]);
            bool CanEdit = Convert.ToBoolean(Request["CanEdit"]);
            bool CanDelete = Convert.ToBoolean(Request["CanDelete"]);

            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext()));

            if (!RoleManager.RoleExists(RoleName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleName;
                RoleManager.Create(role);

                RolePermission RolePermission = new FTL_HRMS.Models.RolePermission();
                RolePermission.RoleId = role.Id;
                RolePermission.MenuItemIdList = TotalSelectedItem;
                RolePermission.CanEdit = CanEdit;
                RolePermission.CanDelete = CanDelete;

                db_ctx.RolePermission.Add(RolePermission);
                int count = db_ctx.SaveChanges();

                if (count == 1)
                {
                    StatusMsg = "Role is successfully added.";
                }
                else
                {
                    StatusMsg = "Role add failed.";
                }

            }
            else
            {
                StatusMsg = "Role already exist.";
            }
            return Json(StatusMsg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditRole(string RoleId)
        {

            List<FTL_HRMS.ViewModels.VMMenuByRole> MenuItemsForRole = new List<FTL_HRMS.ViewModels.VMMenuByRole>();


            string[] MenuIds = null;
            Dictionary<int, string> ParentMenuName = new Dictionary<int, string>();
            List<int> IntMenuIds = new List<int>();
            List<VMMenuByRole> MenuByRoleVMList = new List<VMMenuByRole>();
            List<MenuItem> AllMenues = null;
            List<int> AllSelectedMenues = null;

            string RoleName = db_ctx.Roles.Where(t => t.Id == RoleId).Select(t => t.Name).ToString();
            //string RoleName = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            // get all menu ids for this role
            string PermittedMenuIds = db_ctx.RolePermission.Where(t => t.RoleId == RoleId).Select(p => p.MenuItemIdList).FirstOrDefault();



            if (PermittedMenuIds != null && PermittedMenuIds.Length > 0)
            {
                MenuIds = PermittedMenuIds.Split(',');


                if (MenuIds != null)
                {
                    foreach (string s in MenuIds)
                    {
                        IntMenuIds.Add(Convert.ToInt32(s));
                    }
                }

            }

            if (IntMenuIds.Count > 0)
            {

                AllSelectedMenues = db_ctx.MenuItem.Where(t => IntMenuIds.Contains(t.Id)).Select(rr => rr.Id).ToList();
            }

            if (RoleName == "System Admin")
            {
                AllMenues = db_ctx.MenuItem.ToList();
            }
            else
            {
                AllMenues = db_ctx.MenuItem.Where(t => t.Id > 3).ToList();
            }




            foreach (MenuItem menu in AllMenues)
            {
                VMMenuByRole MenuByRoleVM = new VMMenuByRole();

                if(AllSelectedMenues != null)
                {
                    if (AllSelectedMenues.Contains(menu.Id))
                    {
                        MenuByRoleVM.IsSelected = true;
                    }
                    else
                    {
                        MenuByRoleVM.IsSelected = false;
                    }
                }
                else
                {
                    MenuByRoleVM.IsSelected = false;
                }
                

                MenuByRoleVM.ActionName = menu.ActionName;
                MenuByRoleVM.ControllerName = menu.ControllerName;
                MenuByRoleVM.ParentId = menu.ParentItemId;
                MenuByRoleVM.Id = menu.Id;
                //MenuByRoleVM.MenuOrder = menu.MenuOrder;
                MenuByRoleVM.MenuItemName = menu.Name;
                // MenuByRoleVM.ParentMenuName = menu.ParentItemId != null ? ParentMenuName[menu.Id] : "";

                if (menu.ParentItemId != null)
                {
                    ParentMenuName.Add(menu.Id, db_ctx.MenuItem.Where(w => w.Id == menu.ParentItemId).FirstOrDefault().Name);

                }



                MenuByRoleVMList.Add(MenuByRoleVM);

            }

            //ViewBag.RoleName = RoleName;
            ViewBag.total_selected = PermittedMenuIds;
            ViewBag.RoleName = RoleManager.FindById(RoleId).Name;
            ViewBag.RoleId = RoleId;
            return View("~/Views/RoleManagement/EditRole.cshtml", MenuByRoleVMList);
        }


        [HttpPost]
        public ActionResult EditRole()
        {
            string StatusMsg = string.Empty;
            try
            {

                string RoleId = Convert.ToString(Request["RoleId"]);
                string TotalSelectedItem = Convert.ToString(Request["TotalMnuItem"]);
                bool CanEdit = Convert.ToBoolean(Request["CanEdit"]);
                bool CanDelete = Convert.ToBoolean(Request["CanDelete"]);

                RolePermission RolePermission = db_ctx.RolePermission.Where(rr => rr.RoleId == RoleId).FirstOrDefault();

                
                if(RolePermission == null)
                {
                    RolePermission r = new RolePermission();
                    r.CanDelete = CanDelete;
                    r.CanEdit = CanEdit;
                    r.MenuItemIdList = TotalSelectedItem;
                    r.RoleId = RoleId;
                    r.CanView = true;
                    db_ctx.RolePermission.Add(r);
                }
                else
                {
                    RolePermission.MenuItemIdList = TotalSelectedItem;
                    RolePermission.CanEdit = CanEdit;
                    RolePermission.CanDelete = CanDelete;

                    db_ctx.RolePermission.Attach(RolePermission);
                    var entry = db_ctx.Entry(RolePermission);
                    entry.State = EntityState.Modified;
                }
                

                int resultCount = db_ctx.SaveChanges();

                if (resultCount > 0)
                {
                    StatusMsg = "Data is successfully updated";
                }

                else
                {
                    StatusMsg = "Data update failed. There is something wrong!!!";
                }
                return Json(StatusMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                StatusMsg = "Data update failed. There is something wrong!!!";
                return Json(StatusMsg, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Add User
        [HttpGet]
        public ActionResult UserManagement()
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext()));

            List<IdentityRole> r = RoleManager.Roles.ToList();
            List<VMRoleList> VMRoleList = new List<VMRoleList>();
            int count = 0;
            foreach (IdentityRole p in r)
            {
                if (p.Name == "System Admin")
                    continue;
                count++;
                var VMRole = new VMRoleList()
                {
                    Id = count,
                    RoleName = p.Name
                };

                VMRoleList.Add(VMRole);
            }

            ViewBag.RoleName = new SelectList(VMRoleList, "RoleName", "RoleName");

            return View("~/Views/RoleManagement/AddUser.cshtml");

        }

        [HttpPost]
        public ActionResult UserManagement_Add()
        {
            string UserName = Request["UserName"].ToString();
            string RoleName = Request["RoleName"].ToString();
            string UserPass = Request["UserPassword"].ToString();
            string UserPhone = Request["UserMobileNo"].ToString();

            ApplicationUser AppUser = new ApplicationUser();

            AppUser.UserName = UserName;
            AppUser.PhoneNumber = UserPhone;

            var result = UserManager.Create(AppUser, UserPass);
            if (result.Succeeded)
            {
                var result2 = this.UserManager.AddToRole(AppUser.Id, RoleName);
            }

            TempData["SucMessage"] = "User added successfully.";

            return Redirect("UserManagement");

        }
        #endregion
    }
}
