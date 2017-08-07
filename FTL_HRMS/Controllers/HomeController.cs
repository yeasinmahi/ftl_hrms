using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class HomeController : Controller
    {
        public HRMSDbContext db_ctx = null;
        public UserManager<ApplicationUser> UserManager = null;

        public ActionResult Index()
        {
            return View();
        }

        public HomeController()
        {
            this.db_ctx = new HRMSDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HRMSDbContext()));
        }

        public ActionResult AdminDashboard()
        {
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            string RollId = db_ctx.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();
            string ViewName = string.Empty;
            RolePermission RolePermission = db_ctx.RolePermission.Where(rr => rr.RoleId == RollId).FirstOrDefault();


            Session.Add("UserName", User.Identity.GetUserName());
            Session.Add("RoleName", rolll);
            if (rolll == "System Admin")
            {
                ViewName = "~/Views/Shared/_DashboardAdmin.cshtml";
            }
            else if (rolll == "Super Admin")
            {
                ViewName = "~/Views/Home/AdminDashboard.cshtml";
            }
            
            Session.Add("CanEdit", RolePermission.CanEdit);
            Session.Add("CanDelete", RolePermission.CanDelete);
            return View(ViewName);
        }

    }
}