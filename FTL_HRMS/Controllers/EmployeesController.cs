using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
using static FTL_HRMS.Models.AccountViewModels;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;

namespace FTL_HRMS.Controllers
{
    public class EmployeesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        UserManager<FTL_HRMS.Models.ApplicationUser> _userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new HRMSDbContext()));

        #region List
        // GET: Employees
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Employee> employeeList = _db.Employee.Include(a => a.SourceOfHire).Include(a => a.Designation).Include(a => a.EmployeeType).Include(a => a.Branch).Where(i => i.Status && i.IsSystemOrSuperAdmin == false && i.Sl != userId).ToList();
            return View(employeeList);
        }
        #endregion

        #region Reset Password
        public async Task<ActionResult> ResetPassword()
        {
            try
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(_db);
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(store);
                string empCode = Convert.ToString(Request["field-1"]);
                String userId = _db.Users.Where(u => u.UserName == empCode).Select(i => i.Id).FirstOrDefault();//"<YourLogicAssignsRequestedUserId>";
                String newPassword = Convert.ToString(Request["field-2"]); //"<PasswordAsTypedByUser>";
                String hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
                ApplicationUser cUser = await store.FindByIdAsync(userId);
                await store.SetPasswordHashAsync(cUser, hashedNewPassword);
                await store.UpdateAsync(cUser);
                TempData["message"] = "1Password reset successfully!";
            }
            catch
            {
                TempData["message"] = "0Password reset failed!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
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
            return View(employee);
        }
        #endregion

        #region GetExistingEmployee
        public ActionResult GetExistingEmployee()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            string codeOrName = Request["CodeOrName"];
            List<Employee> employeeList = _db.Employee.Where(r => (r.Name.Contains(codeOrName) || r.Code.Contains(codeOrName)) && r.Sl != userId && r.Status && r.IsSystemOrSuperAdmin == false).ToList();

            List<VMEmployee> empList = new List<VMEmployee>();
            foreach (var item in employeeList)
            {
                VMEmployee emp = new VMEmployee();
                emp.Sl = item.Sl;
                emp.Code = item.Code;
                emp.Name = item.Name;
                empList.Add(emp);
            }
            return Json(empList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetSelectedEducation()
        {
            if (Request["Institute"].ToString() != "")
            {
                string instituteName = Request["Institute"].ToString();
                string program = Request["Program"].ToString();
                string fromDate = Request["FromDate"].ToString();
                string toDate = Request["ToDate"].ToString();
                string board = Request["Board"].ToString();
                string result = Request["Result"].ToString();

                List<Education> educationList = new List<Education>();
                educationList = (List<Education>)Session["EducationList"];
                if (educationList.Count > 0)
                {
                    Education education = new Education();
                    education.Sl = educationList.Count() + 1;
                    education.InstituteName = instituteName;
                    education.Program = program;
                    education.FromDate = Convert.ToDateTime(fromDate);
                    education.ToDate = Convert.ToDateTime(toDate);
                    education.Board = board;
                    education.Result = result;
                    educationList.Add(education);
                    Session["EducationList"] = educationList;
                }
                else
                {
                    List<Education> educationLst = new List<Education>();
                    Education education = new Education();
                    education.Sl = 1;
                    education.InstituteName = instituteName;
                    education.Program = program;
                    education.FromDate = Convert.ToDateTime(fromDate);
                    education.ToDate = Convert.ToDateTime(toDate);
                    education.Board = board;
                    education.Result = result;
                    educationLst.Add(education);
                    Session["EducationList"] = educationLst;
                }
            }
            return Json(Session["EducationList"], JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteFromEduList()
        {
            int sl = Convert.ToInt32(Request["Sl"]);
            List<Education> educationLst = new List<Education>();
            educationLst = (List<Education>)Session["EducationList"];
            for (int i = 0; i < educationLst.Count; i++)
            {
                if (educationLst[i].Sl == sl)
                {
                    educationLst.Remove(educationLst[i]);
                }
            }
            Session["EducationLst"] = null;

            if (educationLst.Count > 0)
            {
                Session["EducationList"] = educationLst;
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
                string instituteName = Request["Institute"].ToString();
                string address = Request["Address"].ToString();
                string fromDate = Request["FromDate"].ToString();
                string toDate = Request["ToDate"].ToString();
                string website = Request["Website"].ToString();
                string phone = Request["Phone"].ToString();
                string designation = Request["Designation"].ToString();

                List<Experience> experienceList = new List<Experience>();
                experienceList = (List<Experience>)Session["ExperienceList"];
                if (experienceList.Count > 0)
                {
                    Experience experience = new Experience();
                    experience.Sl = experienceList.Count() + 1;
                    experience.InstituteName = instituteName;
                    experience.InstituteAddress = address;
                    experience.FromDate = Convert.ToDateTime(fromDate);
                    experience.ToDate = Convert.ToDateTime(toDate);
                    experience.Website = website;
                    experience.Phone = phone;
                    experience.Designation = designation;
                    experienceList.Add(experience);
                    Session["ExperienceList"] = experienceList;
                }
                else
                {
                    List<Experience> experienceLst = new List<Experience>();
                    Experience experience = new Experience();
                    experience.Sl = experienceList.Count() + 1;
                    experience.InstituteName = instituteName;
                    experience.InstituteAddress = address;
                    experience.FromDate = Convert.ToDateTime(fromDate);
                    experience.ToDate = Convert.ToDateTime(toDate);
                    experience.Website = website;
                    experience.Phone = phone;
                    experience.Designation = designation;
                    experienceLst.Add(experience);
                    Session["ExperienceList"] = experienceLst;
                }
            }
            return Json(Session["ExperienceList"], JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteFromExpList()
        {
            int sl = Convert.ToInt32(Request["Sl"]);
            List<Experience> experienceLst = new List<Experience>();
            experienceLst = (List<Experience>)Session["ExperienceList"];
            for (int i = 0; i < experienceLst.Count; i++)
            {
                if (experienceLst[i].Sl == sl)
                {
                    experienceLst.Remove(experienceLst[i]);
                }
            }
            Session["ExperienceLst"] = null;

            if (experienceLst.Count > 0)
            {
                Session["ExperienceList"] = experienceLst;
            }
            else
            {
                Session["ExperienceList"] = new List<Experience>();
            }

            return Json(Session["ExperienceList"], JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        // GET: Employees/Create
        public ActionResult Create()
        {
            Session["EducationList"] = new List<Education>();
            Session["ExperienceList"] = new List<Experience>();

            Employee employee = (Employee)TempData["Employee"];

            List<SourceOfHire> sourceOfHireList = _db.SourceOfHire.Where(i => i.Status).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name");

            List<Branch> branchList = _db.Branches.Where(i => i.Status).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name");

            List<EmployeeType> employeeTypeList = _db.EmployeeType.Where(i => i.Status).ToList();
            ViewBag.EmployeeTypeId = new SelectList(employeeTypeList, "Sl", "Name");

            List<DepartmentGroup> departmentGroupList = _db.DepartmentGroup.Where(i => i.Status).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

            if (employee != null)
            {
                return View(employee);
            }

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Gender,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,IsSystemOrSuperAdmin,ProbationStatus,IsSpecialEmployee,ParmanentDate,EmergencyMobile,RelationEmergencyMobile,BloodGroup,MedicalHistory,Height,Weight,ExtraCurricularActivities")] Employee employee, HttpPostedFileBase image1)
        {
            if (UserValidation(employee.Code, Request["Password"], Request["ConfirmPassword"]) &&
                Convert.ToString(Request["Password"]).Length >= 6)
            {
                #region Add Employee

                string permanentAddress = Request["PermanentAddress"].ToString();
                string presentAddress = Request["PresentAddress"].ToString();
                int designationId = Convert.ToInt32(Request["ddl_designation"]);

                string role =
                    _db.Designation.Where(i => i.Sl == designationId).Select(i => i.RoleName).FirstOrDefault();

                employee.PresentAddress = presentAddress;
                employee.PermanentAddress = permanentAddress;
                employee.DesignationId = designationId;
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                employee.CreatedBy = userId;
                employee.CreateDate = Utility.Utility.GetCurrentDateTime();
                employee.IsSystemOrSuperAdmin = false;
                employee.Status = true;
                if (employee.ProbationStatus != true)
                {
                    employee.ParmanentDate = employee.DateOfJoining;
                }
                _db.Employee.Add(employee);
                try
                {
                    if (ModelState.IsValid)
                    {
                        _db.SaveChanges();
                    }
                    else
                    {
                        List<String> errors = Utility.Utility.GetErrorListFromModelState(ModelState);
                        TempData["message"] = "0";
                        foreach (string error in errors)
                        {
                            TempData["message"] +=error + "  \n";
                        }
                        //TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
                        return EmployeeDefaultList(employee);
                    }
                }
                catch (Exception)
                {
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
                    return EmployeeDefaultList(employee);
                }

               // var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

                ApplicationUser user = new ApplicationUser();
                user.IsActive = true;
                user.Email = employee.Email;
                user.EmailConfirmed = true;
                user.PhoneNumber = employee.Mobile;
                user.PhoneNumberConfirmed = true;
                user.TwoFactorEnabled = true;
                user.LockoutEnabled = true;
                user.AccessFailedCount = 0;
                user.UserName = employee.Code;
                user.CustomUserId = employee.Sl;
                string password = Convert.ToString(Request["Password"]);
                string userPwd = password;

                var chkUser = userManager.Create(user, userPwd);

                //Add default User to Role Customer   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, role);
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.RoleAddFailed);
                        return EmployeeDefaultList(employee);
                    }

                }
                else
                {
                    try
                    {
                        Employee emp = _db.Employee.Find(employee.Sl);
                        _db.Employee.Remove(emp);
                        _db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.CodeFormat);
                    return EmployeeDefaultList(employee);
                }

                #endregion

                #region Add Education

                List<Education> educationList = new List<Education>();
                educationList = (List<Education>)Session["EducationList"];

                for (int i = 0; i < educationList.Count; i++)
                {
                    Education education = new Education();
                    education.InstituteName = educationList[i].InstituteName;
                    education.Program = educationList[i].Program;
                    education.FromDate = educationList[i].FromDate;
                    education.ToDate = educationList[i].ToDate;
                    education.Board = educationList[i].Board;
                    education.Result = educationList[i].Result;
                    education.EmployeeId = employee.Sl;
                    _db.Education.Add(education);
                    _db.SaveChanges();
                }

                #endregion

                #region Add Experience

                List<Experience> experienceList = new List<Experience>();
                experienceList = (List<Experience>)Session["ExperienceList"];

                for (int i = 0; i < experienceList.Count; i++)
                {
                    Experience experience = new Experience();
                    experience.InstituteName = experienceList[i].InstituteName;
                    experience.InstituteAddress = experienceList[i].InstituteAddress;
                    experience.FromDate = experienceList[i].FromDate;
                    experience.ToDate = experienceList[i].ToDate;
                    experience.Website = experienceList[i].Website;
                    experience.Phone = experienceList[i].Phone;
                    experience.Designation = experienceList[i].Designation;
                    experience.EmployeeId = employee.Sl;
                    _db.Experience.Add(experience);
                    _db.SaveChanges();
                }

                #endregion

                #region Add Image

                if (image1 != null)
                {
                    Images image = new Images();
                    image.EmployeeId = employee.Sl;
                    image.Image = new byte[image1.ContentLength];
                    image1.InputStream.Read(image.Image, 0, image1.ContentLength);
                    _db.Images.Add(image);
                    _db.SaveChanges();
                }

                #endregion

                #region Add Leave Count

                List<LeaveType> typeList = new List<LeaveType>();
                typeList = _db.LeaveTypes.ToList();

                for (int i = 0; i < typeList.Count; i++)
                {
                    LeaveCount leaveCount = new LeaveCount();
                    leaveCount.EmployeeId = employee.Sl;
                    leaveCount.LeaveTypeId = typeList[i].Sl;
                    leaveCount.AvailableDay = typeList[i].Day;
                    _db.LeaveCounts.Add(leaveCount);
                    _db.SaveChanges();
                }

                #endregion

                #region Add Employee Salary Distribution

                if (_db.SalaryDistribution.Select(i => i.Sl).Count() > 0)
                {
                    int id = _db.SalaryDistribution.Select(i => i.Sl).FirstOrDefault();
                    SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);

                    EmployeeSalaryDistribution distribution = new EmployeeSalaryDistribution();
                    distribution.EmployeeId = employee.Sl;
                    distribution.GrossSalary = employee.GrossSalary;
                    distribution.BasicSalary = employee.GrossSalary * salaryDistribution.BasicSalary / 100;
                    distribution.HouseRent = employee.GrossSalary * salaryDistribution.HouseRent / 100;
                    distribution.MedicalAllowance = employee.GrossSalary * salaryDistribution.MedicalAllowance / 100;
                    distribution.LifeInsurance = employee.GrossSalary * salaryDistribution.LifeInsurance / 100;
                    distribution.FoodAllowance = employee.GrossSalary * salaryDistribution.FoodAllowance / 100;
                    distribution.Entertainment = employee.GrossSalary * salaryDistribution.Entertainment / 100;
                    _db.EmployeeSalaryDistribution.Add(distribution);
                    _db.SaveChanges();
                }
                else
                {
                    EmployeeSalaryDistribution distribution = new EmployeeSalaryDistribution();
                    distribution.EmployeeId = employee.Sl;
                    distribution.GrossSalary = employee.GrossSalary;
                    distribution.BasicSalary = employee.GrossSalary;
                    distribution.HouseRent = 0;
                    distribution.MedicalAllowance = 0;
                    distribution.LifeInsurance = 0;
                    distribution.FoodAllowance = 0;
                    distribution.Entertainment = 0;
                    _db.EmployeeSalaryDistribution.Add(distribution);
                    _db.SaveChanges();
                }

                #endregion

                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create", "Employees");
            }
            else
            {
                List<SourceOfHire> sourceOfHireList = _db.SourceOfHire.Where(i => i.Status).ToList();
                ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name", employee.SourceOfHireId);

                List<Branch> branchList = _db.Branches.Where(i => i.Status).ToList();
                ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", employee.BranchId);

                List<EmployeeType> employeeTypeList = _db.EmployeeType.Where(i => i.Status).ToList();
                ViewBag.EmployeeTypeId = new SelectList(employeeTypeList, "Sl", "Name", employee.EmployeeTypeId);

                List<DepartmentGroup> departmentGroupList =
                    _db.DepartmentGroup.Where(i => i.Status).ToList();
                ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

                return View(employee);
            }

        }
        #endregion

        #region Edit
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
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
            sourceOfHireList = _db.SourceOfHire.Where(i => i.Status).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name", employee.SourceOfHireId);

            List<EmployeeType> employeeTypeList = new List<EmployeeType>();
            employeeTypeList = _db.EmployeeType.Where(i => i.Status).ToList();
            ViewBag.EmployeeTypeId = new SelectList(employeeTypeList, "Sl", "Name", employee.EmployeeTypeId);

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Gender,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,IsSystemOrSuperAdmin,Status,ProbationStatus,IsSpecialEmployee,ParmanentDate,EmergencyMobile,RelationEmergencyMobile,BloodGroup,MedicalHistory,Height,Weight,ExtraCurricularActivities")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeCode = _db.Employee.Where(c => c.Sl == employee.Sl).Select(i => i.Code).FirstOrDefault();
                string userId = _db.Users.Where(u => u.UserName == employeeCode).Select(i => i.Id).FirstOrDefault();
                if (employeeCode != employee.Code)
                {
                    if (_db.Users.Where(u => u.UserName == employee.Code).Count() > 0)
                    {
                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Exist);
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
                        employee.UpdateDate = Utility.Utility.GetCurrentDateTime();
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

                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
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
                    employee.UpdateDate = Utility.Utility.GetCurrentDateTime();
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

                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                }
            }
            else
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            }
            List<SourceOfHire> sourceOfHireList = new List<SourceOfHire>();
            sourceOfHireList = _db.SourceOfHire.Where(i => i.Status).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name", employee.SourceOfHireId);

            List<EmployeeType> employeeTypeList = new List<EmployeeType>();
            employeeTypeList = _db.EmployeeType.Where(i => i.Status).ToList();
            ViewBag.EmployeeTypeId = new SelectList(employeeTypeList, "Sl", "Name", employee.EmployeeTypeId);

            return View(employee);
        }
        #endregion

        #region Delete
        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
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
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _db.Employee.Find(id);
            employee.Status = false;
            _db.Entry(employee).State = EntityState.Modified;
            _db.SaveChanges();

            string employeeUserId = _db.Users.Where(u => u.UserName == employee.Code).Select(i => i.Id).FirstOrDefault();
            ApplicationUser user = _db.Users.Find(employeeUserId);
            _db.Users.Remove(user);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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

        #region Validation
        public bool UserValidation(string username, string password, string confirmPassword)
        {
            bool isValidate = true;
            if (_db.Users.Where(i => i.UserName == username).Count() > 0)
            {
                isValidate = false;
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.CodeExist);
            }
            else
            {
                if (!password.Equals(confirmPassword))
                {
                    isValidate = false;
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.PasswordMismatch);
                }
            }
            return isValidate;
        }

        public async Task<Boolean> ChangePassword(ManageUserViewModel model, string oldPassword, string newPassword, string confirmPassword)
        {
            bool isPassChangeSuccess = true;
            model.OldPassword = oldPassword;
            model.NewPassword = newPassword;
            model.ConfirmPassword = confirmPassword;
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
                        isPassChangeSuccess = false;
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return isPassChangeSuccess;
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

        #region Photo
        public FileContentResult Photo(string userid)
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            Images image = _db.Images.FirstOrDefault(i => i.EmployeeId == userId);

            if (image != null) return new FileContentResult(image.Image, "image/jpeg");

            byte[] defaultImage = Utility.Utility.GetBytesFromImagePath("~/Content/AdminTemplate/data/profile/profile.png");
            return new FileContentResult(defaultImage, "image/jpeg");
        }
        #endregion

        public ActionResult EmployeeDefaultList(Employee employee)
        {
            TempData["Employee"] = employee;
            return RedirectToAction("Create", "Employees");
        }

    }
}

