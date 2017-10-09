using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FTL_HRMS.Controllers
{
    class NotificationController : Controller
    {
        public HRMSDbContext _db;
        private static NotificationController notificationController = null;
        // GET: Notification
        
        public static NotificationController GetInstant()
        {
            if (notificationController==null)
            {
                notificationController = new NotificationController();
            }
            return notificationController;
        }
        public ActionResult Index()
        {
            return View();
        }
        public List<VMNotification> GetNotificationListByRoll(string rolll,string UserName)
        {
            _db = new HRMSDbContext();
            List<VMNotification> NotificationList = new List<VMNotification>();
            List<LeaveHistory> NotifyList = new List<LeaveHistory>();
            List<Resignation> NotifyResignList = new List<Resignation>();
            if (rolll == "Super Admin" || rolll == "Admin")
            {
                NotifyList = _db.LeaveHistories.Where(x => x.Status == "Pending" || x.Status == "Recommended").ToList();
                NotifyResignList = _db.Resignation.Where(x => x.Status == "Pending").ToList();
            }
            else if (rolll == "Department Head")
            {
                NotifyList = _db.LeaveHistories.Where(x => x.Status == "Pending").ToList();
                NotifyResignList = _db.Resignation.Where(x => x.Status == "Pending").ToList();
            }
            else if (rolll == "Employee")
            {
                var CustomUserId = _db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                int EmployeeId = _db.Employee.Where(x => x.Sl == CustomUserId).Select(x => x.Sl).FirstOrDefault();

                NotifyList = _db.LeaveHistories.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == EmployeeId).ToList();
                NotifyResignList = _db.Resignation.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == EmployeeId).ToList();
            }
            else
            {

            }
            if (NotifyList != null)
            {
                foreach (var o in NotifyList)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.FromDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Leave",
                        Status = o.Status,
                    };

                    NotificationList.Add(ord);
                }
            }
            if (NotifyResignList != null)
            {
                foreach (var o in NotifyResignList)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.ResignDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Resign",
                        Status = o.Status,
                    };

                    NotificationList.Add(ord);
                }
            }
            return NotificationList.OrderByDescending(d => d.Date).ToList();

        }
    }
}