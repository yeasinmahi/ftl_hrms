using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System.Collections.Generic;
using System;

namespace FTL_HRMS.Controllers
{
    public class BranchTransfersController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: BranchTransfers
        public ActionResult Index()
        {
            return View(_db.BranchTransfer.Include(i=> i.Branch).Include(i=> i.Employee).ToList());
        }
        #endregion

        #region Details (We don't use it)
        // GET: BranchTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }
        #endregion

        #region Branch Transfer
        // GET: BranchTransfers/Create
        public ActionResult Create()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Code");

            List<Branch> BranchList = new List<Branch>();
            BranchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");
            return View();
        }

        // POST: BranchTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if (ModelState.IsValid)
            {
                int FromBranchId = _db.Employee.Where(i => i.Sl == branchTransfer.EmployeeId).Select(x => x.BranchId).FirstOrDefault();
                int ToBranchId = Convert.ToInt32(Request["BranchId"]);
                branchTransfer.FromBranchId = FromBranchId;
                branchTransfer.ToBranchId = ToBranchId;
                _db.BranchTransfer.Add(branchTransfer);
                _db.SaveChanges();

                #region Edit Employee
                Employee employee = _db.Employee.Find(branchTransfer.EmployeeId);
                employee.BranchId = ToBranchId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                TempData["SuccessMsg"] = "Transfered Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";

            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Code", branchTransfer.EmployeeId);

            List<Branch> BranchList = new List<Branch>();
            BranchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name", branchTransfer.ToBranchId);

            return View(branchTransfer);
        }
        #endregion

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetBranch()
        {
            string[] EmployeeData = new string[1];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = _db.Employee.Find(empId);

                int BranchId = _db.Employee.Where(i => i.Sl == empId).Select(x => x.BranchId).FirstOrDefault();
                EmployeeData[0] = _db.Branches.Where(i => i.Sl == BranchId).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                EmployeeData[0] = "";
            }
            return Json(EmployeeData.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit
        // GET: BranchTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            ViewBag.Branch = _db.Branches.Where(x => x.Sl == branchTransfer.ToBranchId).Select(t => t.Name).FirstOrDefault();
            List<Branch> BranchList = new List<Branch>();
            BranchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");

            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // POST: BranchTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if (ModelState.IsValid)
            {
                int ToBranchId = Convert.ToInt32(Request["BranchId"]);
                branchTransfer.ToBranchId = ToBranchId;
                int FromBranchId = _db.Employee.Where(i => i.Sl == branchTransfer.EmployeeId).Select(x => x.BranchId).FirstOrDefault();
                branchTransfer.FromBranchId = FromBranchId;
                _db.Entry(branchTransfer).State = EntityState.Modified;
                _db.SaveChanges();

                #region Edit Employee
                Employee employee = _db.Employee.Find(branchTransfer.EmployeeId);
                employee.BranchId = ToBranchId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                TempData["SuccessMsg"] = "Updated Successfully !!";
            }
            else
            {
                TempData["WarningMsg"] = "Something went wrong !!";
            }
            BranchTransfer BranchTransfer = _db.BranchTransfer.Find(branchTransfer.Sl);
            ViewBag.Branch = _db.Branches.Where(x => x.Sl == BranchTransfer.ToBranchId).Select(t => t.Name).FirstOrDefault();
            List<Branch> BranchList = new List<Branch>();
            BranchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");
            return View(branchTransfer);
        }
        #endregion

        #region Delete (We don't use it)
        // GET: BranchTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // POST: BranchTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            _db.BranchTransfer.Remove(branchTransfer);
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
