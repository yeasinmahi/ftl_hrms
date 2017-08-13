using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class HomeController : Controller
    {
        public HRMSDbContext DbCtx = null;
        public UserManager<ApplicationUser> UserManager = null;

        public ActionResult Index()
        {
            return View();
        }

        public HomeController()
        {
            this.DbCtx = new HRMSDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HRMSDbContext()));
        }

        public ActionResult AdminDashboard()
        {
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            string rollId = DbCtx.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();
            string viewName = string.Empty;
            RolePermission rolePermission = DbCtx.RolePermission.Where(rr => rr.RoleId == rollId).FirstOrDefault();


            Session.Add("UserName", User.Identity.GetUserName());
            Session.Add("RoleName", rolll);
            if (rolll == "System Admin")
            {
                viewName = "~/Views/Shared/_DashboardAdmin.cshtml";
            }
            else if (rolll == "Super Admin")
            {
                viewName = "~/Views/Home/AdminDashboard.cshtml";
            }
            
            Session.Add("CanEdit", rolePermission.CanEdit);
            Session.Add("CanDelete", rolePermission.CanDelete);
            return View(viewName);
        }

    }
}