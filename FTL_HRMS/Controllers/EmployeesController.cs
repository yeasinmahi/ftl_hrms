using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using static FTL_HRMS.Models.AccountViewModels;

namespace FTL_HRMS.Controllers
{
    public class EmployeesController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        HRMSDbContext db = new HRMSDbContext();

        UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new FTL_HRMS.Models.HRMSDbContext()));

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employee.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetSelectedEducation()
        {
            if (Request["Institute"].ToString() != "")
            {
                string InstituteName = Request["Institute"].ToString();
                string Program = Request["Program"].ToString();
                string FromDate = Request["FromDate"].ToString();
                string ToDate = Request["ToDate"].ToString();
                string Board = Request["Board"].ToString();
                string Result = Request["Result"].ToString();

                List<Education> EducationList = new List<Education>();
                EducationList = (List<Education>)Session["EducationList"];
                if (EducationList.Count > 0)
                {
                    Education education = new Education();
                    education.Sl = EducationList.Count() + 1;
                    education.InstituteName = InstituteName;
                    education.Program = Program;
                    education.FromDate = Convert.ToDateTime(FromDate);
                    education.ToDate = Convert.ToDateTime(ToDate);
                    education.Board = Board;
                    education.Result = Result;
                    EducationList.Add(education);
                    Session["EducationList"] = EducationList;
                }
                else
                {
                    List<Education> EducationLst = new List<Education>();
                    Education education = new Education();
                    education.Sl = 1;
                    education.InstituteName = InstituteName;
                    education.Program = Program;
                    education.FromDate = Convert.ToDateTime(FromDate);
                    education.ToDate = Convert.ToDateTime(ToDate);
                    education.Board = Board;
                    education.Result = Result;
                    EducationLst.Add(education);
                    Session["EducationList"] = EducationLst;
                }
            }
            return Json(Session["EducationList"], JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteFromEduList()
        {
            int Sl = Convert.ToInt32(Request["Sl"]);
            List<Education> EducationLst = new List<Education>();
            EducationLst = (List<Education>)Session["EducationList"];
            for (int i = 0; i < EducationLst.Count; i++)
            {
                if (EducationLst[i].Sl == Sl)
                {
                    EducationLst.Remove(EducationLst[i]);
                }
            }
            Session["EducationLst"] = null;

            if (EducationLst.Count > 0)
            {
                Session["EducationList"] = EducationLst;
            }
            else
            {
                Session["EducationList"] = new List<Education>();
            }

            return Json(Session["EducationList"], JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetSelectedExperience()
        {
            if (Request["Institute"].ToString() != "")
            {
                string InstituteName = Request["Institute"].ToString();
                string Address = Request["Address"].ToString();
                string FromDate = Request["FromDate"].ToString();
                string ToDate = Request["ToDate"].ToString();
                string Website = Request["Website"].ToString();
                string Phone = Request["Phone"].ToString();
                string Designation = Request["Designation"].ToString();

                List<Experience> ExperienceList = new List<Experience>();
                ExperienceList = (List<Experience>)Session["ExperienceList"];
                if (ExperienceList.Count > 0)
                {
                    Experience experience = new Experience();
                    experience.Sl = ExperienceList.Count() + 1;
                    experience.InstituteName = InstituteName;
                    experience.InstituteAddress = Address;
                    experience.FromDate = Convert.ToDateTime(FromDate);
                    experience.ToDate = Convert.ToDateTime(ToDate);
                    experience.Website = Website;
                    experience.Phone = Phone;
                    experience.Designation = Designation;
                    ExperienceList.Add(experience);
                    Session["ExperienceList"] = ExperienceList;
                }
                else
                {
                    List<Experience> ExperienceLst = new List<Experience>();
                    Experience experience = new Experience();
                    experience.Sl = ExperienceList.Count() + 1;
                    experience.InstituteName = InstituteName;
                    experience.InstituteAddress = Address;
                    experience.FromDate = Convert.ToDateTime(FromDate);
                    experience.ToDate = Convert.ToDateTime(ToDate);
                    experience.Website = Website;
                    experience.Phone = Phone;
                    experience.Designation = Designation;
                    ExperienceLst.Add(experience);
                    Session["ExperienceList"] = ExperienceLst;
                }
            }
            return Json(Session["ExperienceList"], JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteFromExpList()
        {
            int Sl = Convert.ToInt32(Request["Sl"]);
            List<Experience> ExperienceLst = new List<Experience>();
            ExperienceLst = (List<Experience>)Session["ExperienceList"];
            for (int i = 0; i < ExperienceLst.Count; i++)
            {
                if (ExperienceLst[i].Sl == Sl)
                {
                    ExperienceLst.Remove(ExperienceLst[i]);
                }
            }
            Session["ExperienceLst"] = null;

            if (ExperienceLst.Count > 0)
            {
                Session["ExperienceList"] = ExperienceLst;
            }
            else
            {
                Session["ExperienceList"] = new List<Experience>();
            }

            return Json(Session["ExperienceList"], JsonRequestBehavior.AllowGet);
        }
        #endregion

        // GET: Employees/Create
        public ActionResult Create()
        {
            List<SourceOfHire> SourceOfHireList = new List<SourceOfHire>();
            SourceOfHireList = db.SourceOfHire.Where(i => i.Status == true).ToList();
            ViewBag.SourceOfHireId = new SelectList(SourceOfHireList, "Sl", "Name");

            List<Branch> BranchList = new List<Branch>();
            BranchList = db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");

            List<EmployeeType> EmployeeTypeList = new List<EmployeeType>();
            EmployeeTypeList = db.EmployeeType.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeTypeId = new SelectList(EmployeeTypeList, "Sl", "Name");

            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");

            Session["EducationList"] = new List<Education>();
            Session["ExperienceList"] = new List<Experience>();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Employee employee,
            [Bind(Include = "Id,IsActive,RoleId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,CustomUserId")] ApplicationUser user, HttpPostedFileBase image1)
        {
            if (UserValidation(employee.Code, Request["Password"], Request["ConfirmPassword"]))
            {
                #region Add Employee
                string PermanentAddress = Request["PermanentAddress"].ToString();
                string PresentAddress = Request["PresentAddress"].ToString();
                int DesignationId = Convert.ToInt32(Request["ddl_designation"]);

                employee.PresentAddress = PresentAddress;
                employee.PermanentAddress = PermanentAddress;
                employee.DesignationId = DesignationId;
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                employee.CreatedBy = userId;
                employee.CreateDate = DateTime.Now;
                employee.Status = true;
                db.Employee.Add(employee);
                //db.SaveChanges();

                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                //if (!roleManager.RoleExists("Employee"))
                //{
                //    // first we create Receptionist role   
                //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                //    role.Name = "Employee";
                //    roleManager.Create(role);
                //}
                ////Here we create a Receptionist user who will maintain the website                  
                //user.IsActive = true;
                //user.Email = employee.Email;
                //user.EmailConfirmed = true;
                //user.PhoneNumber = employee.Mobile;
                //user.PhoneNumberConfirmed = true;
                //user.TwoFactorEnabled = true;
                //user.LockoutEnabled = true;
                //user.AccessFailedCount = 0;
                //user.UserName = employee.Code;
                //user.CustomUserId = employee.Sl;
                //string Password = Convert.ToString(Request["Password"]);
                //string userPWD = Password;

                //var chkUser = UserManager.Create(user, userPWD);

                ////Add default User to Role Customer   
                //if (chkUser.Succeeded)
                //{
                //    var result1 = UserManager.AddToRole(user.Id, "Employee");
                //}
                //db.SaveChanges();
                #endregion

                #region Add Education
                List<Education> EducationList = new List<Education>();
                EducationList = (List<Education>)Session["EducationList"];

                for (int i = 0; i < EducationList.Count; i++)
                {
                    Education education = new Education();
                    education.InstituteName = EducationList[i].InstituteName;
                    education.Program = EducationList[i].Program;
                    education.FromDate = EducationList[i].FromDate;
                    education.ToDate = EducationList[i].ToDate;
                    education.Board = EducationList[i].Board;
                    education.Result = EducationList[i].Result;
                    education.EmployeeId = employee.Sl;
                    db.Education.Add(education);
                    //db.SaveChanges();
                }
                #endregion

                #region Add Experience
                List<Experience> ExperienceList = new List<Experience>();
                ExperienceList = (List<Experience>)Session["ExperienceList"];

                for (int i = 0; i < ExperienceList.Count; i++)
                {
                    Experience experience = new Experience();
                    experience.InstituteName = ExperienceList[i].InstituteName;
                    experience.InstituteAddress = ExperienceList[i].InstituteAddress;
                    experience.FromDate = ExperienceList[i].FromDate;
                    experience.ToDate = ExperienceList[i].ToDate;
                    experience.Website = ExperienceList[i].Website;
                    experience.Phone = ExperienceList[i].Phone;
                    experience.Designation = ExperienceList[i].Designation;
                    experience.EmployeeId = employee.Sl;
                    db.Experience.Add(experience);
                    //db.SaveChanges();
                }
                #endregion

                #region Add Image
                if (image1 != null)
                {
                    Images image = new Images();
                    image.EmployeeId = employee.Sl;
                    image.Image = new byte[image1.ContentLength];
                    image1.InputStream.Read(image.Image, 0, image1.ContentLength);

                    db.Images.Add(image);
                    db.SaveChanges();
                }
                #endregion

                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create", "Employees");
            }
            else
            {
                List<SourceOfHire> SourceOfHireList = new List<SourceOfHire>();
                SourceOfHireList = db.SourceOfHire.Where(i => i.Status == true).ToList();
                ViewBag.SourceOfHireId = new SelectList(SourceOfHireList, "Sl", "Name", employee.SourceOfHireId);

                List<Branch> BranchList = new List<Branch>();
                BranchList = db.Branches.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name", employee.BranchId);

                List<EmployeeType> EmployeeTypeList = new List<EmployeeType>();
                EmployeeTypeList = db.EmployeeType.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeTypeId = new SelectList(EmployeeTypeList, "Sl", "Name", employee.EmployeeTypeId);

                List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
                DepartmentGroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");

                TempData["WarningMsg"] = "Something went wrong !!";
                return View(employee);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
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

        #region Validation
        public bool UserValidation(string Username, string Password, string ConfirmPassword)
        {
            bool IsValidate = true;
            if (db.Users.Where(i => i.UserName == Username).Count() > 0)
            {
                IsValidate = false;
                TempData["WarningMsg"] = "Username already exist!!!";
            }
            else
            {
                if (!Password.Equals(ConfirmPassword))
                {
                    IsValidate = false;
                    TempData["WarningMsg"] = "Password does not match!!!";
                }
            }
            return IsValidate;
        }

        public async Task<Boolean> ChangePassword(ManageUserViewModel model, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            bool IsPassChangeSuccess = true;
            model.OldPassword = OldPassword;
            model.NewPassword = NewPassword;
            model.ConfirmPassword = ConfirmPassword;
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage", "Account");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        IsPassChangeSuccess = false;
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return IsPassChangeSuccess;
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
        #endregion
    }
}

