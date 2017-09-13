using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FTL_HRMS.Models.ViewModels;

namespace FTL_HRMS.App_Start
{
    public class CustomFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool flag = false;

            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "AccessDenied")
            {
                flag = true;
            }

            else if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Account")
            {
                flag = true;
            }


            else if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Home")
            {
                flag = true;
            }

            if (!flag)
            {
                if (HttpContext.Current.User.Identity.GetUserId() != null)
                {
                    FTL_HRMS.Models.HRMSDbContext db_ctx = new FTL_HRMS.Models.HRMSDbContext();
                    List<VMMenuByRole> MenuItemsForRole = new List<VMMenuByRole>();



                    UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

                    string rolll = userManager.GetRoles(HttpContext.Current.User.Identity.GetUserId()).FirstOrDefault();


                    //string current_user_role=Roles.GetRolesForUser(User.Identity.Name).FirstOrDefault();
                    //List<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> current_role = roleManager.Roles.ToList();  


                    string role_id = db_ctx.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();

                    FTL_HRMS.Models.RolePermission RolePermissions = db_ctx.RolePermission.Where(t => t.RoleId == role_id).FirstOrDefault();
                    List<FTL_HRMS.Models.MenuItem> MenuItems = new List<FTL_HRMS.Models.MenuItem>();

                    FTL_HRMS.Models.MenuItem MenuItemsForCurrentUser = new FTL_HRMS.Models.MenuItem();

                    Dictionary<int, string> ParentMenuName = new Dictionary<int, string>();
                    string[] MenuIds = null;
                    if (RolePermissions != null)
                    {
                        MenuIds = RolePermissions.MenuItemIdList.Split(',');

                        List<int> IntMenuIds = new List<int>();
                        foreach (string s in MenuIds)
                        {
                            IntMenuIds.Add(Convert.ToInt32(s));
                        }

                        foreach (int mId in IntMenuIds)
                        {
                            MenuItemsForCurrentUser = db_ctx.MenuItem.Where(t => t.Id == mId).FirstOrDefault();
                            if (MenuItemsForCurrentUser.ParentItemId != null)
                            {
                                ParentMenuName.Add(mId, db_ctx.MenuItem.Where(w => w.Id == MenuItemsForCurrentUser.ParentItemId).FirstOrDefault().Name);

                            }


                            MenuItems.Add(MenuItemsForCurrentUser);
                        }


                        string[] ActionParts = null;
                        ActionParts = filterContext.ActionDescriptor.ActionName.Split('_');
                        foreach (var mi in MenuItems)
                        {
                            if (filterContext.ActionDescriptor.ActionName == mi.ActionName)
                            {
                                flag = true;
                            }
                            else if (ActionParts[0] == mi.ActionName)
                            {
                                flag = true;
                            }

                            else if (filterContext.ActionDescriptor.ActionName == "Login")
                            {
                                flag = true;
                            }
                            //comfortemsDAL.ViewModels.MenuByRoleVM MenuByRole = new comfortemsDAL.ViewModels.MenuByRoleVM();
                            //MenuByRole.ActionName = mi.ActionName;
                            //MenuByRole.ControllerName = mi.ControllerName;
                            //MenuByRole.MenuItemName = mi.Name;
                            //MenuByRole.ParentMenuName = mi.ParentItemId != null ? ParentMenuName[mi.Id] : "";
                            //MenuItemsForRole.Add(MenuByRole);
                        }
                    }
                }
            }


            if (!flag)
            {
                filterContext.Result = new RedirectToRouteResult(
                                          new RouteValueDictionary {
                                                { "action", "Index" },
                                                { "controller", "AccessDenied" } });
            }

            else
                base.OnActionExecuting(filterContext);



        }
    }
}