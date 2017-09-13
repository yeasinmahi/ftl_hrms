using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class DeviceAttendancesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: DeviceAttendances
        public ActionResult Index()
        {
            string type = string.Empty;
            if (Request["SelectType"] != null)
            {
                type = Request["SelectType"].ToString();
            } 
                       
            List<VMTodaysAttendance> todaysAttendance = new List<VMTodaysAttendance>();
            if (type == "" || type == "Present")
            {
                var codes = _db.DeviceAttendance.Select(m => m.EmployeeCode).Distinct();
                foreach (var item in codes)
                {
                    List<DeviceAttendance>  device = _db.DeviceAttendance.Where(i => i.EmployeeCode == item && i.CheckTime.Day == DateTime.Now.Day).ToList();
                    DateTime checkTime = device.Min(p => p.CheckTime);

                    VMTodaysAttendance attendance = new VMTodaysAttendance();
                    attendance.Code = item;
                    attendance.Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault();
                    attendance.CheckTime = checkTime;
                    attendance.Status = "Present";
                    todaysAttendance.Add(attendance);
                }
                ViewBag.SelectType = "Present";
            }
            else if(type == "Absent")
            {
                List<DeviceAttendance> device = new List<DeviceAttendance>();
                device = _db.DeviceAttendance.Where(i => i.CheckTime.Day == DateTime.Now.Day).ToList();
                var code = device.Select(m => m.EmployeeCode).Distinct();

                List<Employee> employee = _db.Employee.Where(i => i.Status != false && i.IsSystemOrSuperAdmin != true).ToList();
                foreach (var item in code)
                {
                    employee.Where(p => p.Code == item).ToList().ForEach(p => employee.Remove(p));
                }

                var empCode = employee.Select(m => m.Code).Distinct();
                foreach (var item in empCode)
                {
                    VMTodaysAttendance attendance = new VMTodaysAttendance();
                    attendance.Code = item;
                    attendance.Name = _db.Employee.Where(i => i.Code == item).Select(i => i.Name).FirstOrDefault();
                    attendance.Status = "Absent";
                    todaysAttendance.Add(attendance);
                }
                ViewBag.SelectType = "Absent";
            }
            return View(todaysAttendance);
        }
        
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
