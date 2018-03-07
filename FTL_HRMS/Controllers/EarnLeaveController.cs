using FTL_HRMS.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FTL_HRMS.Controllers
{
    public class EarnLeaveController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        // GET: EarnLeave
        public ActionResult Index()
        {
            return View();
        }

        public DateTime GetEmployeeJoiningDate(int sl)
        {
            return _db.Employee.Where(i => i.Sl == sl).Select(i => i.DateOfJoining).FirstOrDefault();
        }

        public DateTime GetLastEarnLeaveCountDate()
        {
            return _db.Company.Select(i => i.LastEarnLeaveCountDate).FirstOrDefault();
        }

        public int GetEarnLeaveStartingMonth()
        {
            return _db.Company.Select(i => i.EarnLeaveStartingMonth).FirstOrDefault();
        }

        public DateTime GetEarnLeaveCountStartingDate(DateTime EmployeeJoiningDate, int EarnLeaveStartingMonth)
        {
            return EmployeeJoiningDate.AddMonths(EarnLeaveStartingMonth);
        }

        public double GetEmployeePresentDays(int sl, DateTime StartDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= DateTime.Now.Date && (i.Status == "P" || i.Status == "L")).Count();
        }


    }
}