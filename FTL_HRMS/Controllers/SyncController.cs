using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.DAL;

namespace FTL_HRMS.Controllers
{
    public class SyncController : Controller
    {
        private readonly HRMSDbContext _db = new HRMSDbContext();
        // GET: Sync
        
        public ActionResult Index()
        {
            ViewData["LastSyncAttendance"] = GetLastMonthlyAttendanceDate().ToLongDateString();
            ViewData["LastSyncEarnLeave"] = GetLastEarnLeaveCountDate().ToLongDateString();
            return View();
        }
        public DateTime GetLastMonthlyAttendanceDate()
        {
            return _db.MonthlyAttendance.Max(i => i.Date);
        }
        public DateTime GetLastEarnLeaveCountDate()
        {
            return _db.Company.Select(i => i.LastEarnLeaveCountDate).FirstOrDefault() ?? Utility.Utility.GetDefaultDate();
        }
    }
}