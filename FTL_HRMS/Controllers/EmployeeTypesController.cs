using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class EmployeeTypesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: EmployeeTypes
        public ActionResult Index()
        {
            return View(_db.EmployeeType.Where(i => i.Status == true).ToList());
        }
        #endregion

        #region Details
        // GET: EmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }
        #endregion

        #region Create
        // GET: EmployeeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Status")] EmployeeType employeeType)
        {
            if (employeeType.Name != "")
            {
                employeeType.Status = true;
                _db.EmployeeType.Add(employeeType);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(employeeType);
        }
        #endregion

        #region Edit
        // GET: EmployeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Status")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employeeType).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(employeeType);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(employeeType);
        }
        #endregion

        #region Delete
        // GET: EmployeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType != null)
            {
                employeeType.Status = false;
                _db.Entry(employeeType).State = EntityState.Modified;
            }
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
