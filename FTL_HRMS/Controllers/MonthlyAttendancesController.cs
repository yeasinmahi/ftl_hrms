using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Utility;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class MonthlyAttendancesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        AttendanceController att = new AttendanceController();

        // GET: MonthlyAttendances
        public ActionResult Index()
        {
            var monthlyAttendance = _db.MonthlyAttendance.Include(m => m.Employee);
            return View(monthlyAttendance.ToList());
        }

        #region Employee Attendence Report
        public ActionResult EmployeeAttendenceReport()
        {
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");
            List<MonthlyAttendance> attendenceList = GetEmployeeList(0, 0, Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }

        [HttpPost]
        public ActionResult EmployeeAttendenceReport(string departmentGroupId, string ddl_dept)
        {
            att.SyncAttendance();
            int dgid, did;
            Int32.TryParse(departmentGroupId, out dgid);
            Int32.TryParse(ddl_dept, out did);
            DateTime Date = Utility.Utility.GetDefaultDate();
            DateTime.TryParse(Request["Date"], out Date);

            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");

            ViewBag.Status = "SelectType";
            TempData["dgid"] = dgid;
            TempData["did"] = did;
            TempData["Date"] = Date;
            List<MonthlyAttendance> attendenceList = GetEmployeeList(dgid, did, Date);
            return View(attendenceList);
        }
        public List<MonthlyAttendance> GetEmployeeList(int departmentGroupId, int departmentId, DateTime Date)
        {
            List<MonthlyAttendance> attendenceList = new List<MonthlyAttendance>();
            if (Date.Equals(Utility.Utility.GetDefaultDate()))
            {
                Date = DateTime.Now.AddDays(-1);
            }
            if (departmentGroupId > 0)
            {
                if (departmentId > 0)
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Employee.Designation.Department.Sl.Equals(departmentId)).Where(x=> x.Date==Date).ToList();
                }
                else
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Date == Date).ToList();
                    
                }

            }
            else if (departmentGroupId == 0)
            {
                attendenceList = _db.MonthlyAttendance.Include(x => x.Employee).Where(x=>x.Date == Date).ToList();
            }
            else
            {
                attendenceList = _db.MonthlyAttendance.Include(x=> x.Employee).ToList();
            }
            return attendenceList;
        }
        #endregion

        #region EmployeeWise Attendence
        public ActionResult EmployeewiseAttendenceReport()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            List<MonthlyAttendance> attendenceList = GetEmployeewiseList(0, Utility.Utility.GetDefaultDate(), Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }
        [HttpPost]
        public ActionResult EmployeewiseAttendenceReport(string EmployeeId)
        {
            att.SyncAttendance();
            int eid;
            Int32.TryParse(EmployeeId, out eid);
            DateTime FromDate = Utility.Utility.GetDefaultDate();
            if(Request["FromDate"]!="")
            {
                DateTime.TryParse(Request["FromDate"], out FromDate);
            }
            DateTime ToDate = Utility.Utility.GetDefaultDate();
            if (Request["ToDate"]!="")
            {
                DateTime.TryParse(Request["ToDate"], out ToDate);
            }
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            ViewBag.Status = "SelectType";
            TempData["dgid"] = eid;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            ViewBag.EmpId = Int32.TryParse(EmployeeId, out eid);
            List<MonthlyAttendance> attendenceList = GetEmployeewiseList(eid, FromDate, ToDate);
            return View(attendenceList);
        }

        public List<MonthlyAttendance> GetEmployeewiseList(int employeeId, DateTime FromDate, DateTime ToDate)
        {
            List<MonthlyAttendance> attendenceList = new List<MonthlyAttendance>();
            if (FromDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                FromDate = DateTime.Now.AddDays(-1);
                ToDate = DateTime.Now.AddDays(-1);
            }
            if (ToDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                ToDate = DateTime.Now.AddDays(-1);
            }
            if (employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] != "" || 
                employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] != "" ||
                employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] == "")
            {
                attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Sl.Equals(employeeId)).Where(x => x.Date >= FromDate  && x.Date <= ToDate).ToList();
            }
            else if (employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] == "")
            {
                attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Sl.Equals(employeeId)).Where(x => x.Date.Day == ToDate.Day && x.Date.Month == ToDate.Month && x.Date.Year == ToDate.Year).ToList();
            }
            else 
            {
                attendenceList = _db.MonthlyAttendance.Where(x => DbFunctions.TruncateTime(x.Date) >= FromDate.Date && DbFunctions.TruncateTime(x.Date) <= ToDate.Date).ToList();
            }
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            ViewBag.EmpId = employeeId;
            return attendenceList;
        }

        #endregion

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code");
            return View();
        }

        // POST: MonthlyAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,Date,Status,IsCalculated,UpdatedBy,UpdateDate")] MonthlyAttendance monthlyAttendance)
        {
            if (ModelState.IsValid)
            {
                _db.MonthlyAttendance.Add(monthlyAttendance);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", monthlyAttendance.EmployeeId);
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = _db.MonthlyAttendance.Where(x => x.EmployeeId == id).Select(x => x.EmployeeId).FirstOrDefault();
            ViewBag.EmployeeCode = _db.MonthlyAttendance.Where(x => x.EmployeeId == id).Select(x=> x.Employee.Code).FirstOrDefault();
            return View(monthlyAttendance);
        }

        // POST: MonthlyAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,Date,Status,IsCalculated,UpdatedBy,UpdateDate")] MonthlyAttendance monthlyAttendance)
        {
            if (ModelState.IsValid)
            {
                monthlyAttendance.EmployeeId = monthlyAttendance.EmployeeId;
                monthlyAttendance.IsCalculated = false;
                string userName = User.Identity.Name;
                int UserId = DbUtility.GetUserId(_db, userName);
                monthlyAttendance.UpdatedBy = UserId;
                monthlyAttendance.UpdateDate = DateTime.Now;
                _db.Entry(monthlyAttendance).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                ViewBag.EmployeeCode = _db.MonthlyAttendance.Where(x => x.EmployeeId == monthlyAttendance.EmployeeId).Select(x => x.Employee.Code).FirstOrDefault();
                return View(monthlyAttendance);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            ViewBag.EmployeeCode = _db.MonthlyAttendance.Where(x => x.EmployeeId == monthlyAttendance.EmployeeId).Select(x => x.Employee.Code).FirstOrDefault();
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(monthlyAttendance);
        }

        // POST: MonthlyAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            _db.MonthlyAttendance.Remove(monthlyAttendance);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            return RedirectToAction("Index");
        }

        public ActionResult Synchronization()
        {
            return View();
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
