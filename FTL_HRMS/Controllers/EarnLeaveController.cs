using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class EarnLeaveController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        // GET: EarnLeave
        public ActionResult Index()
        {
            ViewData["LastSync"] = GetLastEarnLeaveCountDate();
            return View();
        }

        public ActionResult CalculateEarnLeave()
        {
            List<Employee> employeeList = GetEmployeeList();
            DateTime LastEarnLeaveCountDate = GetLastEarnLeaveCountDate();
            int EarnLeaveStartingMonth = GetEarnLeaveStartingMonth();
            int EarnLeaveCountDay = GetEarnLeaveCountDay();
            DateTime LastDateFromFilterAttendance = GetLastDateFromFilterAttendance();
            DateTime FirstDate = Utility.Utility.GetDefaultDate();

            foreach (var emp in employeeList)
            {
                DateTime EarnLeaveCountStartingDate = GetEarnLeaveCountStartingDate(emp.DateOfJoining, EarnLeaveStartingMonth);
                if (LastDateFromFilterAttendance.Date > EarnLeaveCountStartingDate.Date)
                {
                    if (LastEarnLeaveCountDate != null)
                    {
                        if(EarnLeaveCountStartingDate.Date > LastEarnLeaveCountDate.Date)
                        {
                            FirstDate = EarnLeaveCountStartingDate;
                        }
                        else
                        {
                            FirstDate = LastEarnLeaveCountDate.AddDays(1);
                        }
                    }
                    else
                    {
                        FirstDate = EarnLeaveCountStartingDate;
                    }
                }
                else
                {
                    //Nothing To Do!
                }

                int EmployeePresentDays = GetEmployeePresentDays(emp.Sl, FirstDate, LastDateFromFilterAttendance);
                double EmployeeEarnLeaveCount = GetEmployeeEarnLeaveCount(EmployeePresentDays, EarnLeaveCountDay);
                if (UpdateEmployeeLeaveCounts(emp.Sl, EmployeeEarnLeaveCount))
                {
                    //Success!
                }
            }

            if (UpdateCompanyInformation(LastDateFromFilterAttendance))
            {
                //Success!
            }
            else
            {
                //Failed!
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.SyncSuccess);
            return RedirectToAction("Index", "Sync");
        }

        public List<Employee> GetEmployeeList()
        {
            return _db.Employee.Where(i => i.Status && i.IsSystemOrSuperAdmin != true).ToList();
        }

        public DateTime GetLastEarnLeaveCountDate()
        {
            return _db.Company.Select(i => i.LastEarnLeaveCountDate).FirstOrDefault() ?? Utility.Utility.GetDefaultDate();
        }

        public int GetEarnLeaveStartingMonth()
        {
            return _db.Company.Select(i => i.EarnLeaveDuration).FirstOrDefault();
        }

        public int GetEarnLeaveCountDay()
        {
            return _db.Company.Select(i => i.EarnLeaveCountDay).FirstOrDefault();
        }

        public DateTime GetLastDateFromFilterAttendance()
        {
            if(_db.FilterAttendance.Where(i => i.IsCalculated == true).Count() > 0)
            {
                return _db.FilterAttendance.Where(i => i.IsCalculated == true).Max(i => i.Date);
            }
            else
            {
                return Utility.Utility.GetDefaultDate();
            }           
        }

        public DateTime GetEarnLeaveCountStartingDate(DateTime EmployeeJoiningDate, int EarnLeaveStartingMonth)
        {
            return EmployeeJoiningDate.AddMonths(EarnLeaveStartingMonth).AddDays(1);
        }

        public int GetEmployeePresentDays(int sl, DateTime StartDate, DateTime LastDate)
        {
            return _db.MonthlyAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) >= StartDate.Date && DbFunctions.TruncateTime(i.Date) <= LastDate.Date && (i.Status == "P" || i.Status == "L")).Count();
        }

        public double GetEmployeeEarnLeaveCount(int EmployeePresentDays, int EarnLeaveCountDay)
        {
            return Convert.ToDouble(EmployeePresentDays) / Convert.ToDouble(EarnLeaveCountDay);
        }

        public bool UpdateEmployeeLeaveCounts(int sl, double EmployeeEarnLeaveCount)
        {
            try
            {
                int earnLeaveId = _db.LeaveCounts.Where(i => i.EmployeeId == sl && i.LeaveType.Name == "Earn").Select(i => i.Sl).FirstOrDefault();
                var earnLeave = _db.LeaveCounts.Find(earnLeaveId);
                earnLeave.AvailableDay += EmployeeEarnLeaveCount;
                _db.Entry(earnLeave).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool UpdateCompanyInformation(DateTime LastDate)
        {
            if (_db.Company.Select(i => i.Sl).Count() > 0)
            {
                int id = _db.Company.Select(i => i.Sl).FirstOrDefault();
                Company company = _db.Company.Find(id);
                company.LastEarnLeaveCountDate = LastDate;
                _db.Entry(company).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}