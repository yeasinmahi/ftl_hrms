using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FTL_HRMS.Models.Payroll;

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

                #region Edit Employee
                Employee employee = _db.Employee.Find(promotionHistory.EmployeeId);
                employee.DesignationId = toDesignationId;
                employee.GrossSalary = toSalary;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                #region Role Transfer
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                string existingRole = _db.Designation.Where(i => i.Sl == fromDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string newRole = _db.Designation.Where(i => i.Sl == toDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string userId = _db.Users.Where(i => i.CustomUserId == promotionHistory.EmployeeId).Select(s => s.Id).FirstOrDefault();
                var result1 = userManager.RemoveFromRole(userId, existingRole);
                var result2 = userManager.AddToRole(userId, newRole);
                #endregion

                #region Edit Employee Salary Distribution
                if (_db.SalaryDistribution.Select(i => i.Sl).Count() > 0)
                {
                    int id = _db.SalaryDistribution.Select(i => i.Sl).FirstOrDefault();
                    SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);

                    int distributionId = _db.EmployeeSalaryDistribution.Where(i => i.EmployeeId == promotionHistory.EmployeeId).Select(i => i.Sl).FirstOrDefault();
                    EmployeeSalaryDistribution distribution = _db.EmployeeSalaryDistribution.Find(distributionId);
                    distribution.EmployeeId = employee.Sl;
                    distribution.GrossSalary = employee.GrossSalary;
                    distribution.BasicSalary = employee.GrossSalary * salaryDistribution.BasicSalary / 100;
                    distribution.HouseRent = employee.GrossSalary * salaryDistribution.HouseRent / 100;
                    distribution.MedicalAllowance = employee.GrossSalary * salaryDistribution.MedicalAllowance / 100;
                    distribution.LifeInsurance = employee.GrossSalary * salaryDistribution.LifeInsurance / 100;
                    distribution.FoodAllowance = employee.GrossSalary * salaryDistribution.FoodAllowance / 100;
                    distribution.Entertainment = employee.GrossSalary * salaryDistribution.Entertainment / 100;
                    _db.Entry(distribution).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    int distributionId = _db.EmployeeSalaryDistribution.Where(i => i.EmployeeId == promotionHistory.EmployeeId).Select(i => i.Sl).FirstOrDefault();
                    EmployeeSalaryDistribution distribution = _db.EmployeeSalaryDistribution.Find(distributionId);
                    distribution.EmployeeId = employee.Sl;
                    distribution.GrossSalary = employee.GrossSalary;
                    distribution.BasicSalary = employee.GrossSalary;
                    distribution.HouseRent = 0;
                    distribution.MedicalAllowance = 0;
                    distribution.LifeInsurance = 0;
                    distribution.FoodAllowance = 0;
                    distribution.Entertainment = 0;
                    _db.Entry(distribution).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                #endregion

                TempData["message"] ="Promotion" + DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] ="Promotion" + DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            string userName = User.Identity.Name;
            int userid = DbUtility.GetUserId(_db, userName);

            List<Employee> employeeList = new List<Employee>();
            employeeList = _db.Employee.Where(i => i.Status == true && i.IsSystemOrSuperAdmin == false && i.Sl != userid).ToList();
            ViewBag.EmployeeId = new SelectList(employeeList, "Sl", "Code", promotionHistory.EmployeeId);

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
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
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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
