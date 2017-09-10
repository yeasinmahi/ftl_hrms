using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class PromotionHistoriesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: PromotionHistories
        public ActionResult Index()
        {
            var promotionHistories = _db.PromotionHistories.Include(p => p.Employee).Include(p => p.FromDesignation).Include(p => p.ToDesignation);
            return View(promotionHistories.ToList());
        }

        // GET: PromotionHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionHistory promotionHistory = _db.PromotionHistories.Find(id);
            if (promotionHistory == null)
            {
                return HttpNotFound();
            }
            return View(promotionHistory);
        }
        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetDesignation()
        {
            string[] employeeData = new string[4];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = _db.Employee.Find(empId);

                int designationId = _db.Employee.Where(i => i.Sl == empId).Select(x => x.DesignationId).FirstOrDefault();
                employeeData[0] = _db.Designation.Where(i => i.Sl == designationId).Select(x => x.Name).FirstOrDefault();

                int departmentId = _db.Designation.Where(i => i.Sl == designationId).Select(x => x.DepartmentId).FirstOrDefault();
                employeeData[1] = _db.Department.Where(i => i.Sl == departmentId).Select(x => x.Name).FirstOrDefault();

                int departmentGroupId = _db.Department.Where(i => i.Sl == departmentId).Select(x => x.DepartmentGroupId).FirstOrDefault();
                employeeData[2] = _db.DepartmentGroup.Where(i => i.Sl == departmentGroupId).Select(x => x.Name).FirstOrDefault();

                double salary = _db.Employee.Where(i => i.Sl == empId).Select(x => x.GrossSalary).FirstOrDefault();
                employeeData[3] = Convert.ToString(salary);
            }
            else
            {
                employeeData[0] = "";
            }
            return Json(employeeData.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        // GET: PromotionHistories/Create
        public ActionResult Create()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);

            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code");

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
            return View();
        }

        // POST: PromotionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,FromDesignationId,ToDesignationId,PromotionDate,FromSalary,ToSalary")] PromotionHistory promotionHistory)
        {
            if (ModelState.IsValid)
            {
                int fromDesignationId = _db.Employee.Where(i => i.Sl == promotionHistory.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                int toDesignationId = Convert.ToInt32(Request["ddl_designation"]);

                double fromSalary = _db.Employee.Where(i => i.Sl == promotionHistory.EmployeeId).Select(x => x.GrossSalary).FirstOrDefault();
                double toSalary = Convert.ToDouble(Request["to_salary"]);

                promotionHistory.FromDesignationId = fromDesignationId;
                promotionHistory.ToDesignationId = toDesignationId;

                promotionHistory.FromSalary = fromSalary;
                promotionHistory.ToSalary = toSalary;

                _db.PromotionHistories.Add(promotionHistory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", promotionHistory.EmployeeId);
            ViewBag.FromDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.FromDesignationId);
            ViewBag.ToDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.ToDesignationId);
            return View(promotionHistory);
        }

        // GET: PromotionHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionHistory promotionHistory = _db.PromotionHistories.Find(id);
            if (promotionHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", promotionHistory.EmployeeId);
            ViewBag.FromDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.FromDesignationId);
            ViewBag.ToDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.ToDesignationId);
            return View(promotionHistory);
        }

        // POST: PromotionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,FromDesignationId,ToDesignationId,PromotionDate,FromSalary,ToSalary")] PromotionHistory promotionHistory)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(promotionHistory).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", promotionHistory.EmployeeId);
            ViewBag.FromDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.FromDesignationId);
            ViewBag.ToDesignationId = new SelectList(_db.Designation, "Sl", "Code", promotionHistory.ToDesignationId);
            return View(promotionHistory);
        }

        // GET: PromotionHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionHistory promotionHistory = _db.PromotionHistories.Find(id);
            if (promotionHistory == null)
            {
                return HttpNotFound();
            }
            return View(promotionHistory);
        }

        // POST: PromotionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionHistory promotionHistory = _db.PromotionHistories.Find(id);
            _db.PromotionHistories.Remove(promotionHistory);
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
