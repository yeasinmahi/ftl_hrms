using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;

namespace FTL_HRMS.Controllers
{
    public class ProbationController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        #region List
        // GET: Probation
        public ActionResult Index()
        {
            var empList = _db.Employee.Where(x => x.Status == true).ToList();
            var ProbationList = empList.Where(x=> x.ProbationStatus == true).ToList();
            return View(ProbationList);
        }
        #endregion

        #region MakeParmanent
        // GET: Probation
        public ActionResult MakeParmanent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            List<SourceOfHire> sourceOfHireList = new List<SourceOfHire>();
            sourceOfHireList = _db.SourceOfHire.Where(i => i.Status == true).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name", employee.SourceOfHireId);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeParmanent([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Gender,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,IsSystemOrSuperAdmin,Status,ProbationStatus,IsSpecialEmployee,ParmanentDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeCode = _db.Employee.Where(c => c.Sl == employee.Sl).Select(i => i.Code).FirstOrDefault();
                string userId = _db.Users.Where(u => u.UserName == employeeCode).Select(i => i.Id).FirstOrDefault();
                if (employeeCode != employee.Code)
                {
                    if (_db.Users.Where(u => u.UserName == employee.Code).Count() > 0)
                    {
                        TempData["WarningMsg"] = "Username already exist!!!";
                    }
                    else
                    {
                        string permanentAddress = Request["PermanentAddress"].ToString();
                        string presentAddress = Request["PresentAddress"].ToString();

                        employee.PresentAddress = presentAddress;
                        employee.PermanentAddress = permanentAddress;
                        if (employee.ProbationStatus != true)
                        {
                            employee.ParmanentDate = employee.DateOfJoining;
                        }
                        string userName = User.Identity.Name;
                        int UserId = DbUtility.GetUserId(_db, userName);
                        employee.UpdatedBy = UserId;
                        employee.UpdateDate = DateTime.Now;
                        _db.Entry(employee).State = EntityState.Modified;
                        _db.SaveChanges();

                        //user table update
                        ApplicationUser user = _db.Users.Find(userId);
                        user.IsActive = user.IsActive;
                        user.RoleId = user.RoleId;
                        user.Email = employee.Email;
                        user.EmailConfirmed = user.EmailConfirmed;
                        user.PasswordHash = user.PasswordHash;
                        user.SecurityStamp = user.SecurityStamp;
                        user.PhoneNumber = employee.Mobile;
                        user.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                        user.TwoFactorEnabled = user.TwoFactorEnabled;
                        user.LockoutEndDateUtc = user.LockoutEndDateUtc;
                        user.LockoutEnabled = user.LockoutEnabled;
                        user.AccessFailedCount = user.AccessFailedCount;
                        user.UserName = employee.Code;
                        user.CustomUserId = user.CustomUserId;
                        _db.Entry(user).State = EntityState.Modified;
                        _db.SaveChanges();

                        TempData["SuccessMsg"] = "Information updated successfully!";
                    }
                }
                else
                {
                    string permanentAddress = Request["PermanentAddress"].ToString();
                    string presentAddress = Request["PresentAddress"].ToString();

                    employee.PresentAddress = presentAddress;
                    employee.PermanentAddress = permanentAddress;
                    string userName = User.Identity.Name;
                    int UserId = DbUtility.GetUserId(_db, userName);
                    employee.UpdatedBy = UserId;
                    employee.UpdateDate = DateTime.Now;
                    _db.Entry(employee).State = EntityState.Modified;
                    _db.SaveChanges();

                    //user table update
                    ApplicationUser user = _db.Users.Find(userId);
                    user.IsActive = user.IsActive;
                    user.RoleId = user.RoleId;
                    user.Email = employee.Email;
                    user.EmailConfirmed = user.EmailConfirmed;
                    user.PasswordHash = user.PasswordHash;
                    user.SecurityStamp = user.SecurityStamp;
                    user.PhoneNumber = employee.Mobile;
                    user.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                    user.TwoFactorEnabled = user.TwoFactorEnabled;
                    user.LockoutEndDateUtc = user.LockoutEndDateUtc;
                    user.LockoutEnabled = user.LockoutEnabled;
                    user.AccessFailedCount = user.AccessFailedCount;
                    user.UserName = employee.Code;
                    user.CustomUserId = user.CustomUserId;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();

                    TempData["SuccessMsg"] = "Information updated successfully!";
                }
            }
            else
            {
                TempData["WarningMsg"] = "Something went wrong !!";
            }
            List<SourceOfHire> sourceOfHireList = new List<SourceOfHire>();
            sourceOfHireList = _db.SourceOfHire.Where(i => i.Status == true).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name", employee.SourceOfHireId);

            return View(employee);
        }
        #endregion

    }
}