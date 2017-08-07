using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class DepartmentTransfersController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: DepartmentTransfers
        public ActionResult Index()
        {
            return View(db.DepartmentTransfer.ToList());
        }

        // GET: DepartmentTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // GET: DepartmentTransfers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,FromDepartmentId,ToDepartmentId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentTransfer.Add(departmentTransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departmentTransfer);
        }

        // GET: DepartmentTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // POST: DepartmentTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,FromDepartmentId,ToDepartmentId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmentTransfer);
        }

        // GET: DepartmentTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // POST: DepartmentTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            db.DepartmentTransfer.Remove(departmentTransfer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
