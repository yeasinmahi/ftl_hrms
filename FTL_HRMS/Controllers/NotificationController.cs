using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FTL_HRMS.Controllers
{
    class NotificationController : Controller
    {
        public HRMSDbContext Db;
        private static NotificationController _notificationController = null;
        // GET: Notification
        
        public static NotificationController GetInstant()
        {
            if (_notificationController==null)
            {
                _notificationController = new NotificationController();
            }
            return _notificationController;
        }
        public ActionResult Index()
        {
            return View();
        }
        public List<VMNotification> GetNotificationListByRoll(string rolll,string userName)
        {
            Db = new HRMSDbContext();
            List<VMNotification> notificationList = new List<VMNotification>();
            List<LeaveHistory> leaveHistories = new List<LeaveHistory>();
            List<Resignation> resignations = new List<Resignation>();
            List<Loan> loans = new List<Loan>();
            if (rolll == "Super Admin" || rolll == "Admin")
            {
                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Pending" || x.Status == "Recommended").ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Pending").ToList();
                loans = Db.Loan.Where(x => x.Status == "Pending").ToList();
            }
            else if (rolll == "Department Head")
            {
                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Pending").ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Pending").ToList();

                var customUserId = Db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                int employeeId = Db.Employee.Where(x => x.Sl == customUserId).Select(x => x.Sl).FirstOrDefault();
                loans = Db.Loan.Where(x => x.Status == "Considered" || x.Status== "Canceled" || x.Status=="Approved").Where(x => x.EmployeeId == employeeId).ToList();
            }
            else if (rolll == "Employee")
            {
                var customUserId = Db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                int employeeId = Db.Employee.Where(x => x.Sl == customUserId).Select(x => x.Sl).FirstOrDefault();

                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == employeeId).ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == employeeId).ToList();
                loans = Db.Loan.Where(x => x.Status == "Considered" || x.Status == "Canceled" || x.Status == "Approved").Where(x => x.EmployeeId == employeeId).ToList();
            }
            else
            {

            }
            if (leaveHistories != null)
            {
                foreach (var o in leaveHistories)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.FromDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Leave",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            if (resignations != null)
            {
                foreach (var o in resignations)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.ResignDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Resign",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            if (loans != null)
            {
                foreach (var o in loans)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.CreateDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Loan",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            return notificationList.OrderByDescending(d => d.Date).ToList();

        }
    }
}