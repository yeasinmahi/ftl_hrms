using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models;
using System;
using FTL_HRMS.DAL;

namespace FTL_HRMS.Controllers
{
    public class HomeController : Controller
    {
        public HRMSDbContext _db = null;
        public UserManager<ApplicationUser> UserManager = null;

        public ActionResult Index()
        {
            return View();
        }

        public HomeController()
        {
            this._db = new HRMSDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public ActionResult AdminDashboard()
        {
            
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(_db));

            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            string rollId = _db.Roles.Where(t => t.Name == rolll).Select(g => g.Id).FirstOrDefault();
            string viewName = string.Empty;
            RolePermission rolePermission = _db.RolePermission.Where(rr => rr.RoleId == rollId).FirstOrDefault();


            Session.Add("UserName", User.Identity.GetUserName());
            Session.Add("RoleName", rolll);
            if (rolll == "System Admin")
            {
                viewName = "~/Views/Shared/_DashboardAdmin.cshtml";
            }
            else if (rolll == "Super Admin" || rolll == "Admin" || rolll == "Department Head" || rolll == "Employee")
            {
                ViewBag.TotalEmployee = _db.Employee.Where(x=> x.Status== true).Count();
                ViewBag.TotalAdmin = _db.Designation.Where(x=> x.RoleName == "Admin").Where(x => x.Status == true).Count();
                ViewBag.TotalBranch = _db.Branches.Where(x => x.Status == true).Count();
                DateTime d1 = DateTime.Now;
                DateTime d2 = DateTime.Now.AddDays(30);
                DateTime d3 = DateTime.Now.AddDays(-30);
                if (_db.Holiday.Where(i => i.Date > d1 && i.Date < d2).Count() >0)
                {
                    ViewBag.Holiday = _db.Holiday.Where(i => i.Date > d1 && i.Date < d2).Count();
                }
                else
                {
                    ViewBag.Holiday = 0;
                }
                ViewBag.DepartmentGroup = _db.DepartmentGroup.Where(x => x.Status == true).Count();
                ViewBag.Department = _db.Department.Where(x => x.Status == true).Count();
                ViewBag.Designation = _db.Designation.Where(x => x.Status == true).Count();
                ViewBag.Probation = _db.Employee.Where(i => i.ProbationStatus == true).Where(x => x.Status == true).Count();

                string UserName = User.Identity.Name;
                var CustomUserId = _db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                int EmployeeId = _db.Employee.Where(x => x.Sl == CustomUserId).Select(x => x.Sl).FirstOrDefault();

                ViewBag.EarnLeave = _db.LeaveCounts.Where(x => x.EmployeeId == EmployeeId).Where(x => x.LeaveTypeId == 1).Select(x=> x.AvailableDay).FirstOrDefault();
                var designationId = _db.Employee.Where(x => x.Sl == EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                ViewBag.DesignationName = _db.Employee.Where(x => x.Sl == EmployeeId).Where(x => x.Status == true).Select(x => x.Designation.Name).FirstOrDefault();
                var departmentId = _db.Designation.Where(x => x.Sl == designationId).Select(x => x.DepartmentId).FirstOrDefault();
                ViewBag.DepartmentName = _db.Designation.Where(x => x.Sl == designationId).Where(x => x.Status == true).Select(x=> x.Department.Name).FirstOrDefault();
                ViewBag.DepartmentGroupName = _db.Department.Where(x => x.Sl == departmentId).Where(x => x.Status == true).Select(x => x.DepartmentGroup.Name).FirstOrDefault();

                if (_db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x => x.Status == "P").Count() > 0)
                {
                    ViewBag.LastPresent = _db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x=> x.Status== "P").Count();
                }
                else
                {
                    ViewBag.LastPresent = 0;
                }
                if (_db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x => x.Status == "L").Count() > 0)
                {
                    ViewBag.LateDay = _db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x => x.Status == "L").Count();
                }
                else
                {
                    ViewBag.LateDay = 0;
                }

                if (_db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x => x.Status == "A").Count() > 0)
                {
                    ViewBag.AbsentDay = _db.MonthlyAttendance.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Where(x => x.Status == "A").Count();
                }
                else
                {
                    ViewBag.AbsentDay = 0;
                }

                if (_db.PerformanceRating.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Select(x => x.Rating).Count() > 0)
                {
                    ViewBag.PerformanceRating = _db.PerformanceRating.Where(x => x.EmployeeId == EmployeeId).Where(i => i.Date < d1 && i.Date > d3).Select(x => x.Rating).Count();
                }
                else
                {
                    ViewBag.PerformanceRating = 0;
                }
                Session["NotifyList"] = NotificationController.GetInstant().GetNotificationListByRoll(rolll, User.Identity.Name);

                //UpdateNotification(rolll);
                viewName = "~/Views/Home/AdminDashboard.cshtml";
            }
            
            Session.Add("CanEdit", rolePermission.CanEdit);
            Session.Add("CanDelete", rolePermission.CanDelete);
            return View(viewName);
            
        }
        

    }
}