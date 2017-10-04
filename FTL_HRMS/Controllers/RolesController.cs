using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models;
using System.Data.Entity;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.ViewModels;

namespace FTL_HRMS.Controllers
{
    public class RolesController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        HRMSDbContext _dbCtx = new HRMSDbContext();

        UserManager<FTL_HRMS.Models.ApplicationUser> _userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new HRMSDbContext()));

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
            var roles = _dbCtx.Roles.ToList();
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
                _dbCtx.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                _dbCtx.SaveChanges();
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
            var thisRole = _dbCtx.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

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
                _dbCtx.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _dbCtx.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string roleName)
        {
            var thisRole = _dbCtx.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _dbCtx.Roles.Remove(thisRole);
            _dbCtx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserRoles()
        {
            var list = _dbCtx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            ApplicationUser user = _dbCtx.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            account.UserManager.AddToRole(user.Id, roleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = _dbCtx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                ApplicationUser user = _dbCtx.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();

                ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = _dbCtx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            var account = new AccountController();
            ApplicationUser user = _dbCtx.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, roleName))
            {
                account.UserManager.RemoveFromRole(user.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = _dbCtx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }


        #region FTL_HRMS

        public ViewResult RoleList()
        {
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new HRMSDbContext()));

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            List<IdentityRole> roleList = new List<IdentityRole>();
            if (rolll == "System Admin")
            {
                roleList = RoleManager.Roles.ToList();
            }
            else
            {
                roleList = RoleManager.Roles.Where(t => t.Name != "System Admin").ToList();
            }




            return View("~/Views/RoleManagement/RoleList.cshtml", roleList);
        }


        [HttpGet]
        public ActionResult AddRole()
        {
            HRMSDbContext dbCtx = new HRMSDbContext();
            List<VMMenuByRole> menuItemsForRole = new List<VMMenuByRole>();

            string rolll = _userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            string roleId = dbCtx.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();

            FTL_HRMS.Models.RolePermission rolePermissions = dbCtx.RolePermission.Where(t => t.RoleId == roleId).FirstOrDefault();
            List<FTL_HRMS.Models.MenuItem> menuItems = new List<FTL_HRMS.Models.MenuItem>();

            FTL_HRMS.Models.MenuItem menuItemsForCurrentUser = new FTL_HRMS.Models.MenuItem();

            Dictionary<int, string> parentMenuName = new Dictionary<int, string>();
            //string[] MenuIds = null;


            if (rolePermissions != null)
            {
                //MenuIds = RolePermissions.MenuItemIdList.Split(',');
                //MenuIds = comfortEMSContext.MenuItems.Select(t => t.Id).FirstOrDefault().ToString();

                //List<int> IntMenuIds = new List<int>();
                //foreach (string s in MenuIds)
                //{
                //    IntMenuIds.Add(Convert.ToInt32(s));
                //}

                List<MenuItem> mnuItemList = dbCtx.MenuItem.ToList();



                foreach (MenuItem mId in mnuItemList)
                {
                    menuItemsForCurrentUser = dbCtx.MenuItem.Where(t => t.Id == mId.Id).FirstOrDefault();
                    if (menuItemsForCurrentUser.ParentItemId != null)
                    {
                        parentMenuName.Add(mId.Id, dbCtx.MenuItem.Where(w => w.Id == menuItemsForCurrentUser.ParentItemId).FirstOrDefault().Name);

                    }

                    menuItems.Add(menuItemsForCurrentUser);
                }
            }
            foreach (var mi in menuItems)
            {
                VMMenuByRole menuByRole = new VMMenuByRole();
                menuByRole.ActionName = mi.ActionName;
                menuByRole.ControllerName = mi.ControllerName;
                menuByRole.MenuItemName = mi.Name;
                menuByRole.Id = mi.Id;
                menuByRole.ParentId = mi.ParentItemId;
                menuByRole.FunctionNames = mi.AllFunctions;
                menuByRole.ViewNames = mi.ViewNames;
                // MenuByRole.MenuOrder = mi.MenuOrder;
                menuByRole.ParentMenuName = mi.ParentItemId != null ? parentMenuName[mi.Id] : "";
                menuItemsForRole.Add(menuByRole);
            }


            return View("~/Views/RoleManagement/AddRole.cshtml", menuItemsForRole);

        }




        [HttpPost]
        public ActionResult AddNewRole()
        {

            //var res = Request.Form["MenuItems[2].Id"];
            string statusMsg = string.Empty;

            string roleName = Convert.ToString(Request["RoleName"]);
            string totalSelectedItem = Convert.ToString(Request["TotalMnuItem"]);
            bool canEdit = Convert.ToBoolean(Request["CanEdit"]);
            bool canDelete = Convert.ToBoolean(Request["CanDelete"]);

            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext()));

            if (!RoleManager.RoleExists(roleName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = roleName;
                RoleManager.Create(role);

                RolePermission rolePermission = new FTL_HRMS.Models.RolePermission();
                rolePermission.RoleId = role.Id;
                rolePermission.MenuItemIdList = totalSelectedItem;
                rolePermission.CanEdit = canEdit;
                rolePermission.CanDelete = canDelete;

                _dbCtx.RolePermission.Add(rolePermission);
                int count = _dbCtx.SaveChanges();

                if (count == 1)
                {
                    statusMsg = "Role is successfully added.";
                }
                else
                {
                    statusMsg = "Role add failed.";
                }

            }
            else
            {
                statusMsg = "Role already exist.";
            }
            return Json(statusMsg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditRole(string roleId)
        {

            List<VMMenuByRole> menuItemsForRole = new List<VMMenuByRole>();


            string[] menuIds = null;
            Dictionary<int, string> parentMenuName = new Dictionary<int, string>();
            List<int> intMenuIds = new List<int>();
            List<VMMenuByRole> menuByRoleVmList = new List<VMMenuByRole>();
            List<MenuItem> allMenues = null;
            List<int> allSelectedMenues = null;

            string roleName = _dbCtx.Roles.Where(t => t.Id == roleId).Select(t => t.Name).ToString();
            //string RoleName = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            // get all menu ids for this role
            string permittedMenuIds = _dbCtx.RolePermission.Where(t => t.RoleId == roleId).Select(p => p.MenuItemIdList).FirstOrDefault();



            if (permittedMenuIds != null && permittedMenuIds.Length > 0)
            {
                menuIds = permittedMenuIds.Split(',');


                if (menuIds != null)
                {
                    foreach (string s in menuIds)
                    {
                        intMenuIds.Add(Convert.ToInt32(s));
                    }
                }

            }

            if (intMenuIds.Count > 0)
            {

                allSelectedMenues = _dbCtx.MenuItem.Where(t => intMenuIds.Contains(t.Id)).Select(rr => rr.Id).ToList();
            }

            if (roleName == "System Admin")
            {
                allMenues = _dbCtx.MenuItem.ToList();
            }
            else
            {
                allMenues = _dbCtx.MenuItem.Where(t => t.Id > 3).ToList();
            }




            foreach (MenuItem menu in allMenues)
            {
                VMMenuByRole menuByRoleVm = new VMMenuByRole();

                if(allSelectedMenues != null)
                {
                    if (allSelectedMenues.Contains(menu.Id))
                    {
                        menuByRoleVm.IsSelected = true;
                    }
                    else
                    {
                        menuByRoleVm.IsSelected = false;
                    }
                }
                else
                {
                    menuByRoleVm.IsSelected = false;
                }
                

                menuByRoleVm.ActionName = menu.ActionName;
                menuByRoleVm.ControllerName = menu.ControllerName;
                menuByRoleVm.ParentId = menu.ParentItemId;
                menuByRoleVm.Id = menu.Id;
                //MenuByRoleVM.MenuOrder = menu.MenuOrder;
                menuByRoleVm.MenuItemName = menu.Name;
                // MenuByRoleVM.ParentMenuName = menu.ParentItemId != null ? ParentMenuName[menu.Id] : "";

                if (menu.ParentItemId != null)
                {
                    parentMenuName.Add(menu.Id, _dbCtx.MenuItem.Where(w => w.Id == menu.ParentItemId).FirstOrDefault().Name);

                }



                menuByRoleVmList.Add(menuByRoleVm);

            }

            //ViewBag.RoleName = RoleName;
            ViewBag.total_selected = permittedMenuIds;
            ViewBag.RoleName = RoleManager.FindById(roleId).Name;
            ViewBag.RoleId = roleId;
            return View("~/Views/RoleManagement/EditRole.cshtml", menuByRoleVmList);
        }


        [HttpPost]
        public ActionResult EditRole()
        {
            string statusMsg = string.Empty;
            try
            {

                string roleId = Convert.ToString(Request["RoleId"]);
                string totalSelectedItem = Convert.ToString(Request["TotalMnuItem"]);
                bool canEdit = Convert.ToBoolean(Request["CanEdit"]);
                bool canDelete = Convert.ToBoolean(Request["CanDelete"]);

                RolePermission rolePermission = _dbCtx.RolePermission.Where(rr => rr.RoleId == roleId).FirstOrDefault();

                
                if(rolePermission == null)
                {
                    RolePermission r = new RolePermission();
                    r.CanDelete = canDelete;
                    r.CanEdit = canEdit;
                    r.MenuItemIdList = totalSelectedItem;
                    r.RoleId = roleId;
                    r.CanView = true;
                    _dbCtx.RolePermission.Add(r);
                }
                else
                {
                    rolePermission.MenuItemIdList = totalSelectedItem;
                    rolePermission.CanEdit = canEdit;
                    rolePermission.CanDelete = canDelete;

                    _dbCtx.RolePermission.Attach(rolePermission);
                    var entry = _dbCtx.Entry(rolePermission);
                    entry.State = EntityState.Modified;
                }
                

                int resultCount = _dbCtx.SaveChanges();

                if (resultCount > 0)
                {
                    statusMsg = "Data is successfully updated";
                }

                else
                {
                    statusMsg = "Data update failed. There is something wrong!!!";
                }
                return Json(statusMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                statusMsg = "Data update failed. There is something wrong!!!";
                return Json(statusMsg, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Add User
        [HttpGet]
        public ActionResult UserManagement()
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext()));

            List<IdentityRole> r = RoleManager.Roles.ToList();
            List<VMRoleList> vmRoleList = new List<VMRoleList>();
            int count = 0;
            foreach (IdentityRole p in r)
            {
                if (p.Name == "System Admin")
                    continue;
                count++;
                var vmRole = new VMRoleList()
                {
                    Id = count,
                    RoleName = p.Name
                };

                vmRoleList.Add(vmRole);
            }

            ViewBag.RoleName = new SelectList(vmRoleList, "RoleName", "RoleName");

            return View("~/Views/RoleManagement/AddUser.cshtml");

        }

        [HttpPost]
        public ActionResult UserManagement_Add()
        {
            string userName = Request["UserName"].ToString();
            string roleName = Request["RoleName"].ToString();
            string userPass = Request["UserPassword"].ToString();
            string userPhone = Request["UserMobileNo"].ToString();

            ApplicationUser appUser = new ApplicationUser();

            appUser.UserName = userName;
            appUser.PhoneNumber = userPhone;

            var result = UserManager.Create(appUser, userPass);
            if (result.Succeeded)
            {
                var result2 = this.UserManager.AddToRole(appUser.Id, roleName);
            }

            TempData["SucMessage"] = "User added successfully.";

            return Redirect("UserManagement");

        }
        #endregion
    }
}
