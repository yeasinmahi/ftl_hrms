using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;
using FTL_HRMS.Models.Hr;
namespace FTL_HRMS.Controllers
{
    public class DeviceAttendancesController : Controller
    {
        private readonly HRMSDbContext _db = new HRMSDbContext();
        //readonly AttendanceController _attendanceController = new AttendanceController();
        #region List
        // GET: DeviceAttendances
        public ActionResult Index()
        {
            //_attendanceController.SyncAttendance();
            List<VMTodaysAttendance> todaysAttendance = new List<VMTodaysAttendance>();
            var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();
            DateTime nowTime = Utility.Utility.GetCurrentDateTime();
            foreach (var item in codes)
            {
                var device = _db.DeviceAttendance.Where(i => i.EmployeeCode == item && i.CheckTime.Day == nowTime.Day && i.CheckTime.Month == nowTime.Month && i.CheckTime.Year == nowTime.Year).ToList();
                if(device.Count > 0)
                {
                    DateTime checkTime = device.Min(p => p.CheckTime);
                    VMTodaysAttendance attendance = new VMTodaysAttendance
                    {
                        Code = item,
                        Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault(),
                        CheckTime = checkTime,
                        Status = "Present"
                    };
                    todaysAttendance.Add(attendance);
                }
            }
            return View(todaysAttendance);
        }

        public ActionResult GetTodaysAttendance()
        {
            string type = string.Empty;
            if (Request["SelectType"] != null)
            {
                type = Request["SelectType"];
            }
            List<VMTodaysAttendance> todaysAttendance = new List<VMTodaysAttendance>();
            DateTime nowTime = Utility.Utility.GetCurrentDateTime();
            if (type == "Present")
            {
                var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();
                foreach (var item in codes)
                {
                    var device = _db.DeviceAttendance.Where(i => i.EmployeeCode == item && i.CheckTime.Day == nowTime.Day && i.CheckTime.Month == nowTime.Month && i.CheckTime.Year == nowTime.Year).ToList();
                    if (device.Count > 0)
                    {
                        DateTime checkTime = device.Min(p => p.CheckTime);
                        VMTodaysAttendance attendance = new VMTodaysAttendance
                        {
                            Code = item,
                            Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault(),
                            CheckTime = checkTime,
                            Status = "Present"
                        };
                        todaysAttendance.Add(attendance);
                    }
                }
            }
            else if (type == "Absent")
            {
                var device = _db.DeviceAttendance.Where(i => i.CheckTime.Day == nowTime.Day && i.CheckTime.Month == nowTime.Month && i.CheckTime.Year == nowTime.Year).ToList();
                var codes = device.Select(m => m.EmployeeCode).Distinct();

                var leave = _db.LeaveHistories.Where(i => i.FromDate < nowTime && i.ToDate > nowTime).ToList();
                var empSl = leave.Select(m => m.EmployeeId).Distinct();

                List<Employee> employee = _db.Employee.Where(i => i.Status && i.IsSystemOrSuperAdmin != true).ToList();
                foreach (var item in codes)
                {
                    employee.Where(p => p.Code == item).ToList().ForEach(p => employee.Remove(p));
                }
                foreach (var item in empSl)
                {
                    employee.Where(p => p.Sl == item).ToList().ForEach(p => employee.Remove(p));
                }

                var empCode = employee.Select(m => m.Code).Distinct();
                foreach (var item in empCode)
                {
                    VMTodaysAttendance attendance = new VMTodaysAttendance
                    {
                        Code = item,
                        Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault(),
                        CheckTime = Utility.Utility.GetDefaultDate(),
                        Status = "Absent"
                    };
                    todaysAttendance.Add(attendance);
                }
            }
            else
            {
                var empSl = _db.LeaveHistories.Select(m => m.EmployeeId).Distinct();
                foreach (var item in empSl)
                {
                    if (_db.LeaveHistories.Any(i => i.EmployeeId == item && i.FromDate < Utility.Utility.GetCurrentDateTime() && i.ToDate > Utility.Utility.GetCurrentDateTime()))
                    {
                        VMTodaysAttendance attendance = new VMTodaysAttendance
                        {
                            Code = _db.Employee.Where(i => i.Sl == item).Select(i => i.Code).FirstOrDefault(),
                            Name = _db.Employee.Where(i => i.Sl == item).Select(i => i.Name).FirstOrDefault(),
                            CheckTime = Utility.Utility.GetDefaultDate(),
                            Status = "Leave"
                        };
                        todaysAttendance.Add(attendance);
                    }
                }
            }
            return Json(todaysAttendance, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Details (We don't use it)
        // GET: DeviceAttendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceAttendance deviceAttendance = _db.DeviceAttendance.Find(id);
            if (deviceAttendance == null)
            {
                return HttpNotFound();
            }
            return View(deviceAttendance);
        }
        #endregion

        #region Create (We don't use it)
        // GET: DeviceAttendances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeCode,Datetime,IsCalculated")] DeviceAttendance deviceAttendance)
        {
            if (ModelState.IsValid)
            {
                _db.DeviceAttendance.Add(deviceAttendance);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deviceAttendance);
        }
        #endregion

        #region Edit (We don't use it)
        // GET: DeviceAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceAttendance deviceAttendance = _db.DeviceAttendance.Find(id);
            if (deviceAttendance == null)
            {
                return HttpNotFound();
            }
            return View(deviceAttendance);
        }

        // POST: DeviceAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeCode,Datetime,IsCalculated")] DeviceAttendance deviceAttendance)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(deviceAttendance).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deviceAttendance);
        }
        #endregion

        #region Delete (We don't use it)
        // GET: DeviceAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceAttendance deviceAttendance = _db.DeviceAttendance.Find(id);
            if (deviceAttendance == null)
            {
                return HttpNotFound();
            }
            return View(deviceAttendance);
        }

        // POST: DeviceAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceAttendance deviceAttendance = _db.DeviceAttendance.Find(id);
            if (deviceAttendance != null) _db.DeviceAttendance.Remove(deviceAttendance);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}