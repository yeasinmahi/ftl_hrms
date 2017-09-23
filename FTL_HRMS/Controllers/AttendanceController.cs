using FTL_HRMS.Models;
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

        // GET: Attendance
        public Status MoveDeviceAttendanceToFilterAttendance()
        {
            List<DateTime> DateListExceptToday = DistintDateList();

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

        public List<DateTime> DistintDateList()
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
    }
}