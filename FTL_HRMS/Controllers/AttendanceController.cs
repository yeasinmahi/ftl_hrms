﻿using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FTL_HRMS.Utility.DbUtility;

namespace FTL_HRMS.Controllers
{
    public class AttendanceController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region MoveDeviceAttendanceToFilterAttendance
        // GET: Attendance
        public Status MoveDeviceAttendanceToFilterAttendance()
        {
            List<DateTime> DateListExceptToday = DistinctDateListFromDeviceAttendance();

            var Codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();

            foreach (var date in DateListExceptToday)
            {
                foreach (var code in Codes)
                {
                    FilterAttendance filterAttendance = GetFilterAttendanceByDate(code, date);
                    if (filterAttendance != null)
                    {
                        if (InsertFilterAttendance(filterAttendance))
                        {
                            if(!UpdateDeviceAttendanceStatus(code, date))
                            {
                                return Status.Fail;
                            }
                        }
                        else
                        {
                            return Status.Fail;
                        }
                    }
                }
            }
            _db.SaveChanges();
            return Status.Success;
        }

        public List<DateTime> DistinctDateListFromDeviceAttendance()
        {
            List<DateTime> DateList = _db.DeviceAttendance.Where(i => i.IsCalculated == false).ToList().Select(m => m.CheckTime.Date).Distinct().ToList();
            DateList.Remove(DateTime.Now.Date);
            return DateList;
        }

        public FilterAttendance GetFilterAttendanceByDate(string code, DateTime date)
        {
            List<DeviceAttendance> device = GetDeviceAttendance(code, date);
            FilterAttendance filterAttendance = null;
            if (device.Count > 0)
            {
                filterAttendance = new FilterAttendance();
                try
                {
                    DateTime InTime = device.Min(p => p.CheckTime);
                    DateTime OutTime = device.Max(p => p.CheckTime);

                    filterAttendance.EmployeeId = GetEmployeeSlByEmployeeCode(code);
                    filterAttendance.Date = date.Date;
                    filterAttendance.InTime = InTime;
                    filterAttendance.OutTime = OutTime;
                    filterAttendance.IsCalculated = false;
                }
                catch
                {
                    return null;
                }
            }
            return filterAttendance;
        }

        public int GetEmployeeSlByEmployeeCode(string code)
        {
            return _db.Employee.Where(i => i.Code == code).Select(i => i.Sl).FirstOrDefault();
        }

        public bool InsertFilterAttendance(FilterAttendance filterAttendance)
        {           
            try
            {
                _db.FilterAttendance.Add(filterAttendance);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDeviceAttendanceStatus(string code, DateTime date)
        {
            try
            {
                List<DeviceAttendance> device = GetDeviceAttendance(code, date);
                device.ForEach(x => x.IsCalculated = true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<DeviceAttendance> GetDeviceAttendance(string code, DateTime date)
        {
            return _db.DeviceAttendance.Where(i => i.EmployeeCode == code && DbFunctions.TruncateTime(i.CheckTime) == date.Date).ToList();
        }
        #endregion

        #region MoveFilterAttendanceToMonthlyAttendance
        public Status MoveFilterAttendanceToMonthlyAttendance()
        {
            DateTime StartDate = GetFirstDate();
            DateTime LastDate = GetLastDate();
            List<DateTime> DateList = GetDateList(StartDate, LastDate);
            List<Employee> EmployeeList = GetEmployeeList();

            foreach(var date in DateList)
            {
                foreach(var emp in EmployeeList)
                {
                    MonthlyAttendance monthlyAttendance = GetMonthlyAttendance(date, emp);
                    if (monthlyAttendance != null)
                    {
                        if (InsertMonthlyAttendance(monthlyAttendance))
                        {
                            if (!UpdateFilterAttendanceStatus(emp.Sl, date))
                            {
                                return Status.Fail;
                            }
                        }
                        else
                        {
                            return Status.Fail;
                        }
                    }
                }
            }
            _db.SaveChanges();
            return Status.Success;
        }

        public DateTime GetFirstDate()
        {
            double Day = DateTime.Now.Day-1;
            DateTime FirstDate = DateTime.Now.Date.AddDays(-Day);
            if (_db.FilterAttendance.Where(i => i.IsCalculated == true).ToList().Count > 0)
            {
                FirstDate = _db.FilterAttendance.Where(i => i.IsCalculated == true).ToList().Max(i => i.Date.Date).AddDays(1);
            }
            return FirstDate;
        }

        public DateTime GetLastDate()
        {
            DateTime LastDate = DateTime.Now.AddDays(-1).Date;
            return LastDate;
        }

        public List<DateTime> GetDateList(DateTime StartDate, DateTime LastDate)
        {
            List<DateTime> DateList = new List<DateTime>();
            while (StartDate.Day <= LastDate.Day)
            {
                DateList.Add(StartDate);
                StartDate = StartDate.Date.AddDays(+1);
            }
            return DateList;
        }

        public List<Employee> GetEmployeeList()
        {
            return _db.Employee.Where(i => i.Status != false && i.IsSystemOrSuperAdmin != true).ToList();
        }

        public List<FilterAttendance> GetFilterAttendance(int sl, DateTime date)
        {
            return _db.FilterAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) == date.Date).ToList();
        }

        public bool IsEmployeeLate(DateTime InTime, DateTime OpeningTime, double LateConsiderationTime)
        {
            try
            {
                if(InTime.TimeOfDay <= OpeningTime.AddMinutes(LateConsiderationTime).TimeOfDay)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch
            {
                return false;
            }
        }

        public bool IsWeekend(int BranchId, DateTime date)
        {
            try
            {
                List<DateTime> WeekendList = GetWeekendList(BranchId);
                foreach(var item in WeekendList)
                {
                    if(item.DayOfWeek == date.DayOfWeek)
                    {
                        return true;
                    }
                }
                return false;               
            }
            catch
            {
                return false;
            }
        }

        public List<DateTime> GetWeekendList(int BranchId)
        {
            return _db.Weekend.Where(i => i.BranchId == BranchId).Select(i=> i.Day).ToList();
        }

        public bool IsHoliday(DateTime date)
        {
            try
            {
                if(_db.Holiday.Where(i=> DbFunctions.TruncateTime(i.Date) == date.Date).ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch
            {
                return false;
            }
        }

        public bool IsLeave(int sl, DateTime date)
        {
            try
            {
                if (_db.LeaveHistories.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.FromDate) <= date.Date && DbFunctions.TruncateTime(i.ToDate) >= date.Date).ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetLeaveType(int sl, DateTime date)
        {
            try
            {
                return _db.LeaveHistories.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.FromDate) <= date.Date && DbFunctions.TruncateTime(i.ToDate) >= date.Date).Select(i => i.LeaveType.Name).FirstOrDefault();
            }
            catch
            {
                return "Not Found";
            }          
        }

        public MonthlyAttendance GetMonthlyAttendance(DateTime date,Employee emp)
        {
            List<FilterAttendance> EmployeeAttendance = GetFilterAttendance(emp.Sl, date);
            MonthlyAttendance monthlyAttendance = null;
            
            try
            {
                monthlyAttendance = new MonthlyAttendance();
                monthlyAttendance.Date = date;
                monthlyAttendance.EmployeeId = emp.Sl;
                monthlyAttendance.IsCalculated = false;

                if (EmployeeAttendance.Count > 0)
                {
                    if (emp.IsSpecialEmployee == false)
                    {
                        if (emp.Branch.IsLateCalculated == true)
                        {
                            double LateConsiderationTime = (double)emp.Branch.LateConsiderationTime;
                            if (IsEmployeeLate(EmployeeAttendance.FirstOrDefault().InTime, emp.Branch.OpeningTime, LateConsiderationTime))
                            {
                                monthlyAttendance.Status = "L";
                            }
                            else
                            {
                                monthlyAttendance.Status = "P";
                            }
                        }
                        else
                        {
                            monthlyAttendance.Status = "P";
                        }
                    }
                    else
                    {
                        monthlyAttendance.Status = "P";
                    }
                }
                else if (IsWeekend(emp.BranchId, date))
                {
                    monthlyAttendance.Status = "W";
                }
                else if (IsHoliday(date))
                {
                    monthlyAttendance.Status = "H";
                }
                else if (IsLeave(emp.Sl, date))
                {
                    string LeaveType = GetLeaveType(emp.Sl, date);
                    if (LeaveType == "Casual Leave")
                    {
                        monthlyAttendance.Status = "CL";
                    }
                    else if (LeaveType == "Sick Leave")
                    {
                        monthlyAttendance.Status = "SL";
                    }
                    else if (LeaveType == "Without Pay Leave")
                    {
                        monthlyAttendance.Status = "WL";
                    }
                    else if (LeaveType == "Earn Leave")
                    {
                        monthlyAttendance.Status = "EL";
                    }
                    else
                    {
                        monthlyAttendance.Status = LeaveType;
                    }
                }
                else
                {
                    monthlyAttendance.Status = "A";
                }
            }
            catch
            {
                return null;
            }
        return monthlyAttendance;
        }

        public bool InsertMonthlyAttendance(MonthlyAttendance monthlyAttendance)
        {
            try
            {
                _db.MonthlyAttendance.Add(monthlyAttendance);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateFilterAttendanceStatus(int sl, DateTime date)
        {
            try
            {
                List<FilterAttendance> EmployeeAttendance = GetFilterAttendance(sl, date);
                if (EmployeeAttendance.Count > 0)
                {
                    EmployeeAttendance.ForEach(x => x.IsCalculated = true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}