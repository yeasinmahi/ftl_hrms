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
        readonly AttendanceController _attendanceController = new AttendanceController();
        #region List
        // GET: DeviceAttendances
        public ActionResult Index()
        {
            _attendanceController.SyncAttendance();
            List<VMTodaysAttendance> todaysAttendance = new List<VMTodaysAttendance>();
            var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();
            foreach (var item in codes)
            {
                List<DeviceAttendance> device;
                device = _db.DeviceAttendance.Where(i => i.EmployeeCode == item && i.CheckTime.Day == DateTime.Now.Day && i.CheckTime.Month == DateTime.Now.Month && i.CheckTime.Year == DateTime.Now.Year).ToList();
                if(device.Count > 0)
                {
                    DateTime checkTime = device.Min(p => p.CheckTime);
                    VMTodaysAttendance attendance = new VMTodaysAttendance();
                    attendance.Code = item;
                    attendance.Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault();
                    attendance.CheckTime = checkTime;
                    attendance.Status = "Present";
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
                type = Request["SelectType"].ToString();
            }
            List<VMTodaysAttendance> todaysAttendance = new List<VMTodaysAttendance>();
            if (type == "Present")
            {
                var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();
                foreach (var item in codes)
                {
                    List<DeviceAttendance> device = new List<DeviceAttendance>();
                    device = _db.DeviceAttendance.Where(i => i.EmployeeCode == item && i.CheckTime.Day == DateTime.Now.Day && i.CheckTime.Month == DateTime.Now.Month && i.CheckTime.Year == DateTime.Now.Year).ToList();
                    if (device.Count > 0)
                    {
                        DateTime checkTime = device.Min(p => p.CheckTime);
                        VMTodaysAttendance attendance = new VMTodaysAttendance();
                        attendance.Code = item;
                        attendance.Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault();
                        attendance.CheckTime = checkTime;
                        attendance.Status = "Present";
                        todaysAttendance.Add(attendance);
                    }
                }
            }
            else if (type == "Absent")
            {
                List<DeviceAttendance> device = new List<DeviceAttendance>();
                device = _db.DeviceAttendance.Where(i => i.CheckTime.Day == DateTime.Now.Day && i.CheckTime.Month == DateTime.Now.Month && i.CheckTime.Year == DateTime.Now.Year).ToList();
                var codes = device.Select(m => m.EmployeeCode).Distinct();

                List<LeaveHistory> leave = new List<LeaveHistory>();
                leave = _db.LeaveHistories.Where(i => i.FromDate < DateTime.Now && i.ToDate > DateTime.Now).ToList();
                var empSl = leave.Select(m => m.EmployeeId).Distinct();

                List<Employee> employee = _db.Employee.Where(i => i.Status != false && i.IsSystemOrSuperAdmin != true).ToList();
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
                    VMTodaysAttendance attendance = new VMTodaysAttendance();
                    attendance.Code = item;
                    attendance.Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault();
                    attendance.CheckTime = Utility.Utility.GetDefaultDate();
                    attendance.Status = "Absent";
                    todaysAttendance.Add(attendance);
                }
            }
            else
            {
                var empSl = _db.LeaveHistories.Select(m => m.EmployeeId).Distinct();
                foreach (var item in empSl)
                {
                    if (_db.LeaveHistories.Where(i => i.EmployeeId == item && i.FromDate < DateTime.Now && i.ToDate > DateTime.Now).Count() > 0)
                    {
                        VMTodaysAttendance attendance = new VMTodaysAttendance();
                        attendance.Code = _db.Employee.Where(i => i.Sl == item).Select(i => i.Code).FirstOrDefault();
                        attendance.Name = _db.Employee.Where(i => i.Sl == item).Select(i => i.Name).FirstOrDefault();
                        attendance.CheckTime = Utility.Utility.GetDefaultDate();
                        attendance.Status = "Leave";
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