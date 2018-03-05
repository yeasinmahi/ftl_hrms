using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using static FTL_HRMS.Utility.DbUtility;

namespace FTL_HRMS.Controllers
{
    public class AttendanceController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        public void SyncAttendance()
        {
            MoveDeviceToDeviceAttendance();
            MoveDeviceAttendanceToFilterAttendance();
            MoveFilterAttendanceToMonthlyAttendance();
        }

        #region MoveDeviceToDeviceAttendance
        public void MoveDeviceToDeviceAttendance()
        {
            Device device = new Device();
            List<DeviceAttendance> deviceAttendances = device.GetDailyAttendance();
            if (deviceAttendances.Count>0)
            {
                List<int> userIds = deviceAttendances.Select(x => x.UserId).Distinct().ToList();
                foreach (DeviceAttendance deviceAttendance in deviceAttendances)
                {
                    if (_db.DeviceAttendance.Any(o => o.CheckTime == deviceAttendance.CheckTime && o.EmployeeCode == deviceAttendance.EmployeeCode)) continue;
                    _db.DeviceAttendance.Add(deviceAttendance);
                }
                try
                {
                    _db.SaveChanges();
                    device.UpdateCheckInOutStatus(userIds);

                }
                catch (Exception)
                {
                    // ignored
                }
            }
            
        }
        #endregion

        #region MoveDeviceAttendanceToFilterAttendance
        // GET: Attendance
        public Status MoveDeviceAttendanceToFilterAttendance()
        {
            List<DateTime> dateListExceptToday = DistinctDateListFromDeviceAttendance();

            var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();

            foreach (var date in dateListExceptToday)
            {
                foreach (var code in codes)
                {
                    FilterAttendance filterAttendance = GetFilterAttendanceByDate(code, date);
                    if (filterAttendance != null)
                    {
                        if (InsertFilterAttendance(filterAttendance))
                        {
                            if (!UpdateDeviceAttendanceStatus(code, date))
                            {
                                return Status.UpdateFailed;
                            }
                        }
                        else
                        {
                            return Status.AddFailed;
                        }
                    }
                }
            }
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                // ignored
            }

            return Status.AddSuccess;
        }

        public List<DateTime> DistinctDateListFromDeviceAttendance()
        {
            List<DateTime> dateList = _db.DeviceAttendance.Where(i => i.IsCalculated == false).ToList().Select(m => m.CheckTime.Date).Distinct().ToList();
            dateList.Remove(DateTime.Now.Date);
            return dateList;
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
                    DateTime inTime = device.Min(p => p.CheckTime);
                    DateTime outTime = device.Max(p => p.CheckTime);
                    int empId = GetEmployeeSlByEmployeeCode(code);

                    if (empId > 0)
                    {
                        filterAttendance.EmployeeId = empId;
                        filterAttendance.Date = date.Date;
                        filterAttendance.InTime = inTime;
                        filterAttendance.OutTime = outTime;
                        filterAttendance.IsCalculated = false;
                    }
                    else
                    {
                        filterAttendance = null;
                    }
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

        public string GetEmployeeCodeByEmployeeSl(int sl)
        {
            return _db.Employee.Where(x => x.Sl == sl).Select(x => x.Code).FirstOrDefault();
        }

        public bool InsertFilterAttendance(FilterAttendance filterAttendance)
        {
            try
            {
                if (_db.FilterAttendance.Where(i => i.EmployeeId == filterAttendance.EmployeeId && DbFunctions.TruncateTime(i.Date) == filterAttendance.Date.Date).ToList().Count > 0)
                {
                    int id = _db.FilterAttendance.Where(i => i.EmployeeId == filterAttendance.EmployeeId && DbFunctions.TruncateTime(i.Date) == filterAttendance.Date.Date).Select(i => i.Sl).FirstOrDefault();
                    FilterAttendance filter = _db.FilterAttendance.Find(id);
                    if (filter != null)
                    {
                        filter.InTime = filterAttendance.InTime;
                        filter.OutTime = filterAttendance.OutTime;
                        filter.IsCalculated = false;
                        _db.Entry(filter).State = EntityState.Modified;
                    }
                }
                else
                {
                    _db.FilterAttendance.Add(filterAttendance);
                }
                return true;
            }
            catch(Exception)
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
            catch (Exception)
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
            DateTime startDate = GetFirstDate();
            DateTime lastDate = GetLastDate();
            List<DateTime> dateList = GetDateList(startDate, lastDate);
            List<Employee> employeeList = GetEmployeeList();

            foreach (var date in dateList)
            {
                foreach (var emp in employeeList)
                {
                    MonthlyAttendance monthlyAttendance = GetMonthlyAttendance(date, emp);
                    if (monthlyAttendance != null)
                    {
                        if (InsertMonthlyAttendance(monthlyAttendance))
                        {
                            if (!UpdateFilterAttendanceStatus(emp.Sl, date))
                            {
                                return Status.UpdateFailed;
                            }
                        }
                        else
                        {
                            return Status.AddFailed;
                        }
                    }
                }
            }
            _db.SaveChanges();
            return Status.AddSuccess;
        }

        public DateTime GetFirstDate()
        {
            DateTime firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (_db.FilterAttendance.Where(i => i.IsCalculated).ToList().Count > 0)
            {
                firstDate = _db.FilterAttendance.Where(i=> i.IsCalculated).ToList().Max(i => i.Date.Date).AddDays(1);
            }
            return firstDate;
        }

        public DateTime GetLastDate()
        {
            DateTime lastDate = DateTime.Now.AddDays(-1).Date;
            return lastDate;
        }

        public List<DateTime> GetDateList(DateTime startDate, DateTime lastDate)
        {
            List<DateTime> dateList = new List<DateTime>();
            while (startDate.Date <= lastDate.Date)
            {
                dateList.Add(startDate);
                startDate = startDate.Date.AddDays(+1);
            }
            return dateList;
        }

        public List<Employee> GetEmployeeList()
        {
            return _db.Employee.Where(i => i.Status && i.IsSystemOrSuperAdmin != true).ToList();
        }

        public List<FilterAttendance> GetFilterAttendance(int sl, DateTime date)
        {
            return _db.FilterAttendance.Where(i => i.EmployeeId == sl && DbFunctions.TruncateTime(i.Date) == date.Date).ToList();
        }

        public bool IsEmployeeLate(DateTime inTime, DateTime openingTime, double lateConsiderationTime)
        {
            try
            {
                DateTime openingTimeWithConsidration = openingTime.AddMinutes(lateConsiderationTime);
                if (inTime.TimeOfDay >= openingTimeWithConsidration.TimeOfDay)
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

        public bool IsWeekend(int branchId, DateTime date)
        {
            try
            {
                List<DateTime> weekendList = GetWeekendList(branchId);
                foreach (var item in weekendList)
                {
                    if (item.DayOfWeek == date.DayOfWeek)
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

        public List<DateTime> GetWeekendList(int branchId)
        {
            return _db.Weekend.Where(i => i.BranchId == branchId).Select(i => i.Day).ToList();
        }

        public bool IsHoliday(DateTime date)
        {
            try
            {
                if (_db.Holiday.Where(i => DbFunctions.TruncateTime(i.Date) == date.Date).ToList().Count > 0)
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

        public MonthlyAttendance GetMonthlyAttendance(DateTime date, Employee emp)
        {
            List<FilterAttendance> employeeAttendance = GetFilterAttendance(emp.Sl, date);
            MonthlyAttendance monthlyAttendance;

            try
            {
                monthlyAttendance = new MonthlyAttendance();
                monthlyAttendance.Date = date;
                monthlyAttendance.EmployeeId = emp.Sl;
                monthlyAttendance.IsCalculated = false;

                if (employeeAttendance.Count > 0)
                {
                    if (emp.IsSpecialEmployee == false)
                    {
                        if (emp.Branch.IsLateCalculated)
                        {
                            double lateConsiderationTime = emp.Branch.LateConsiderationTime;
                            FilterAttendance filterAttendance = employeeAttendance.FirstOrDefault();
                            if (filterAttendance != null && IsEmployeeLate(filterAttendance.InTime, emp.Branch.OpeningTime, lateConsiderationTime))
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
                else if (IsUnofficialDay(emp.Sl, date))
                {
                    monthlyAttendance.Status = "U";
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
                    string leaveType = GetLeaveType(emp.Sl, date);
                    monthlyAttendance.Status = leaveType.Substring(0, 2) + "L";
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

        private bool IsUnofficialDay(int employeeId, DateTime date)
        {
            DateTime joiningDate = _db.Employee.Where(x => x.Sl.Equals(employeeId)).Select(x => x.DateOfJoining).FirstOrDefault();
            DateTime resignDate = _db.Resignation.Where(x => x.Sl.Equals(employeeId) && x.Status.Equals("Approved")).Select(x => x.ResignDate).FirstOrDefault();
            if (joiningDate > date)
            {
                return true;
            }
            if (resignDate != new DateTime(1, 1, 1))
            {
                if (resignDate < date)
                {
                    return true;
                }
            }
            return false;

        }

        public bool InsertMonthlyAttendance(MonthlyAttendance monthlyAttendance)
        {
            try
            {
                if(_db.MonthlyAttendance.Where(i=> i.EmployeeId == monthlyAttendance.EmployeeId && DbFunctions.TruncateTime(i.Date) == monthlyAttendance.Date.Date && i.IsCalculated == false).ToList().Count > 0)
                {
                    int id = _db.MonthlyAttendance.Where(i => i.EmployeeId == monthlyAttendance.EmployeeId && DbFunctions.TruncateTime(i.Date) == monthlyAttendance.Date.Date && i.IsCalculated == false).Select(i => i.Sl).FirstOrDefault();
                    MonthlyAttendance monthly = _db.MonthlyAttendance.Find(id);
                    if (monthly != null)
                    {
                        monthly.Status = monthlyAttendance.Status;
                        _db.Entry(monthly).State = EntityState.Modified;
                    }
                }
                else
                {
                    _db.MonthlyAttendance.Add(monthlyAttendance);
                }                
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
                List<FilterAttendance> employeeAttendance = GetFilterAttendance(sl, date);
                if (employeeAttendance.Count > 0)
                {
                    employeeAttendance.ForEach(x => x.IsCalculated = true);
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