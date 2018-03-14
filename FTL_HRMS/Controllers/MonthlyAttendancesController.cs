using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;
using FTL_HRMS.Utility;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class MonthlyAttendancesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        //AttendanceController _att = new AttendanceController();

        // GET: MonthlyAttendances
        public ActionResult Index()
        {
            var monthlyAttendance = _db.MonthlyAttendance.Include(m => m.Employee);
            return View(monthlyAttendance.ToList());
        }

        #region Employee Attendence Report
        public ActionResult EmployeeAttendenceReport()
        {
            //_att.SyncAttendance();
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");
            List<MonthlyAttendance> attendenceList = GetEmployeeList(0, 0, Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }

        [HttpPost]
        public ActionResult EmployeeAttendenceReport(string departmentGroupId, string ddlDept)
        {
            int dgid, did;
            Int32.TryParse(departmentGroupId, out dgid);
            Int32.TryParse(ddlDept, out did);
            DateTime date = Utility.Utility.GetDefaultDate();
            DateTime.TryParse(Request["Date"], out date);

            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");

            ViewBag.Status = "SelectType";
            TempData["dgid"] = dgid;
            TempData["did"] = did;
            TempData["Date"] = date;
            List<MonthlyAttendance> attendenceList = GetEmployeeList(dgid, did, date);
            return View(attendenceList);
        }
        public List<MonthlyAttendance> GetEmployeeList(int departmentGroupId, int departmentId, DateTime date)
        {
            List<MonthlyAttendance> attendenceList = new List<MonthlyAttendance>();
            if (date.Equals(Utility.Utility.GetDefaultDate()))
            {
                date = Utility.Utility.GetCurrentDateTime().AddDays(-1);
            }
            if (departmentGroupId > 0)
            {
                if (departmentId > 0)
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Employee.Designation.Department.Sl.Equals(departmentId)).Where(x=> x.Date==date).ToList();
                }
                else
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Date == date).ToList();
                    
                }

            }
            else if (departmentGroupId == 0)
            {
                attendenceList = _db.MonthlyAttendance.Include(x => x.Employee).Where(x=>x.Date == date).ToList();
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
            //_att.SyncAttendance();
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            List<MonthlyAttendance> attendenceList = GetEmployeewiseList(0, Utility.Utility.GetDefaultDate(), Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }
        [HttpPost]
        public ActionResult EmployeewiseAttendenceReport(string employeeId)
        {
            int eid;
            Int32.TryParse(employeeId, out eid);
            DateTime fromDate = Utility.Utility.GetDefaultDate();
            if(Request["FromDate"]!="")
            {
                DateTime.TryParse(Request["FromDate"], out fromDate);
            }
            DateTime toDate = Utility.Utility.GetDefaultDate();
            if (Request["ToDate"]!="")
            {
                DateTime.TryParse(Request["ToDate"], out toDate);
            }
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            ViewBag.Status = "SelectType";
            TempData["dgid"] = eid;
            TempData["FromDate"] = fromDate;
            TempData["ToDate"] = toDate;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.EmpId = Int32.TryParse(employeeId, out eid);
            List<MonthlyAttendance> attendenceList = GetEmployeewiseList(eid, fromDate, toDate);
            return View(attendenceList);
        }

        public List<MonthlyAttendance> GetEmployeewiseList(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<MonthlyAttendance> attendenceList = new List<MonthlyAttendance>();
            if (fromDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                fromDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
                toDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
            }
            if (toDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                toDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
            }
            if (employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] != "" || 
                employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] != "" ||
                employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] == "")
            {
                attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Sl.Equals(employeeId)).Where(x => x.Date >= fromDate  && x.Date <= toDate).ToList();
            }
            else if (employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] == "")
            {
                attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Sl.Equals(employeeId)).Where(x => x.Date.Day == toDate.Day && x.Date.Month == toDate.Month && x.Date.Year == toDate.Year).ToList();
            }
            else 
            {
                attendenceList = _db.MonthlyAttendance.Where(x => DbFunctions.TruncateTime(x.Date) >= fromDate.Date && DbFunctions.TruncateTime(x.Date) <= toDate.Date).ToList();
            }
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.EmpId = employeeId;
            return attendenceList;
        }

        #endregion


        #region EmployeeWise Attendence
        public ActionResult EmployeewiseFilterAttendenceReport()
        {
            //_att.SyncAttendance();
            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            List<FilterAttendanceView> attendenceList = GetEmployeewiseFilterList(0, Utility.Utility.GetDefaultDate(), Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }
        [HttpPost]
        public ActionResult EmployeewiseFilterAttendenceReport(string employeeId)
        {
            int eid;
            Int32.TryParse(employeeId, out eid);
            DateTime fromDate = Utility.Utility.GetDefaultDate();
            if (Request["FromDate"] != "")
            {
                DateTime.TryParse(Request["FromDate"], out fromDate);
            }
            DateTime toDate = Utility.Utility.GetDefaultDate();
            if (Request["ToDate"] != "")
            {
                DateTime.TryParse(Request["ToDate"], out toDate);
            }
            var employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");
            ViewBag.Status = "SelectType";
            TempData["dgid"] = eid;
            TempData["FromDate"] = fromDate;
            TempData["ToDate"] = toDate;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.EmpId = Int32.TryParse(employeeId, out eid);
            List<FilterAttendanceView> attendenceList = GetEmployeewiseFilterList(eid, fromDate, toDate);
            return View(attendenceList);
        }

        public List<FilterAttendanceView> GetEmployeewiseFilterList(int employeeId, DateTime fromDate, DateTime toDate)
        {
            string query;
            if (fromDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                fromDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
                toDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
            }
            if (toDate.Equals(Utility.Utility.GetDefaultDate()))
            {
                toDate = Utility.Utility.GetCurrentDateTime().AddDays(-1);
            }
            if (employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] != "" ||
                employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] != "" ||
                employeeId > 0 && Request["ToDate"] != "" && Request["FromDate"] == "")
            {
                //attendenceList = _db.FilterAttendanceView.SqlQuery("select * from FilterAttendanceView").ToList();
                //attendenceList = _db.FilterAttendanceView.Where(x => x.EmployeeId.Equals(employeeId)).Where(x => DbFunctions.TruncateTime(x.Date) >= FromDate.Date && DbFunctions.TruncateTime(x.Date) <= ToDate.Date).ToList();
                query = "select * from FilterAttendanceView where EmployeeId = "+employeeId +" and Date between '"+fromDate.Date+"' and '"+toDate.Date+"'";
            }
            else if (employeeId > 0 && Request["ToDate"] == "" && Request["FromDate"] == "")
            {
                //attendenceList = _db.FilterAttendanceView.Where(x => x.EmployeeId.Equals(employeeId)).Where(x => DbFunctions.TruncateTime(x.Date) >= FromDate.Date && DbFunctions.TruncateTime(x.Date) <= ToDate.Date).ToList();
                query = "select * from FilterAttendanceView where EmployeeId = " + employeeId + " and Date between '" + fromDate.Date + "' and '" + toDate.Date + "'";
            }
            else
            {
                //attendenceList = _db.FilterAttendanceView.Where(x => DbFunctions.TruncateTime(x.Date) >= FromDate.Date && DbFunctions.TruncateTime(x.Date) <= ToDate.Date).ToList();
                query = "select * from FilterAttendanceView where Date between '" + fromDate.Date + "' and '" + toDate.Date + "'";
            }
            var attendenceList = new FilterAttendanceViewGatway().GetFilterAttendanceView(query);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
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
                int userId = DbUtility.GetUserId(_db, userName);
                monthlyAttendance.UpdatedBy = userId;
                monthlyAttendance.UpdateDate = Utility.Utility.GetCurrentDateTime();
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
