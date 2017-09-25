using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FTL_HRMS.Controllers
{
    public class SalarySheetController : Controller
    {
        // GET: SalarySheet
        private HRMSDbContext _db = new HRMSDbContext();

        public List<int> GetEmployeeSlFromMonthlyAttendance(DateTime StartDate, DateTime EndDate)
        {
            return _db.MonthlyAttendance.Where(i => i.IsCalculated == false && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= EndDate.Date).ToList().Select(m => m.EmployeeId).Distinct().ToList();
        }

        public double GetWorkingDays(DateTime StartDate)
        {
            return System.DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
        }
    }
}