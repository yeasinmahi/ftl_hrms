using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using System.Data.Entity;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using static FTL_HRMS.Models.AccountViewModels;
using FTL_HRMS.Utility;
using FTL_HRMS.Models.Hr;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace FTL_HRMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        HRMSDbContext _dbCtx = new HRMSDbContext();

        UserManager<FTL_HRMS.Models.ApplicationUser> _userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new HRMSDbContext()));

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HRMSDbContext())), new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HRMSDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Help()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string captchaText)
        {
            var user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                int customUserId = _dbCtx.Users.Where(i => i.UserName == model.UserName).Select(i => i.CustomUserId).FirstOrDefault();
                string code = _dbCtx.Employee.Where(i => i.Sl == customUserId).Select(i => i.Code).FirstOrDefault();
                if (code != "SystemAdmin")
                {
                    if (_dbCtx.Subscription.Select(i => i.Sl).Count() > 0)
                    {
                        int id = _dbCtx.Subscription.Select(i => i.Sl).FirstOrDefault();
                        Subscription subscription = _dbCtx.Subscription.Find(id);
                        if (DecryptString(subscription.Code) == "▓╖▓╖▓╖")
                        {
                            if (subscription.Date.Date >= Utility.Utility.GetCurrentDateTime().Date)
                            {
                                await SignInAsync(user, model.RememberMe);
                                return RedirectToAction("AdminDashboard", "Home");
                            }
                            else
                            {
                                string strEncrypted = "╖▓╖▓╖▓";
                                subscription.Code = EncryptString(strEncrypted);
                                _dbCtx.Entry(subscription).State = EntityState.Modified;
                                _dbCtx.SaveChanges();
                                TempData["ErrLogin"] = "Subscription Failed !!";
                                return RedirectToAction("Login", "Account");
                            }
                        }
                        else
                        {
                            TempData["ErrLogin"] = "Subscription Failed !!";
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        TempData["ErrLogin"] = "Subscription Failed !!";
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToAction("AdminDashboard", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                TempData["ErrLogin"] = "Invalid Username or Password!!";
                return RedirectToAction("Login", "Account");
            }
        }

        #region Encrypt And Decrypt
        public string EncryptString(string str)
        {
            string encrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] iv = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(encrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptString(string str)
        {
            str = str.Replace(" ", "+");
            string decryptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] iv = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        #endregion


        protected string GetIpAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    TempData["SucMessage"] = "User is Registered successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }






        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                state?.Errors.Clear();

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user != null)
        //    {
        //        await SignInAsync(user, isPersistent: false);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then prompt the user to create an account
        //        ViewBag.ReturnUrl = returnUrl;
        //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        //    }
        //}

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Manage");
        //    }
        //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser() { UserName = model.UserName };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff


        #region Profile
        [AllowAnonymous]
        public ActionResult ProfileInfo()
        {
            UserManager<FTL_HRMS.Models.ApplicationUser> userManager = new UserManager<FTL_HRMS.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<FTL_HRMS.Models.ApplicationUser>(new HRMSDbContext()));
            string rolll = userManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            if (rolll == "System Admin" || rolll == "Super Admin")
            {
                return RedirectToAction("AdminInformation", "Account");
            }
            else
            {
                return RedirectToAction("AccountInformation", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult AdminInformation()
        {
            ApplicationUser user = _dbCtx.Users.Find(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminInformation([Bind(Include = "Id,IsActive,RoleId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,CustomUserId")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var userEmail = _dbCtx.Users.Where(c => c.Id == userId).Select(i => i.Email).FirstOrDefault();
                if (userEmail != user.Email)
                {
                    if (_dbCtx.Users.Where(u => u.Email == user.Email).Count() > 0)
                    {
                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Exist);
                    }
                    else
                    {
                        _dbCtx.Entry(user).State = EntityState.Modified;
                        _dbCtx.SaveChanges();

                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                    }
                }
                else
                {
                    _dbCtx.Entry(user).State = EntityState.Modified;
                    _dbCtx.SaveChanges();

                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                }

                if (Request["OldPassword"] != "")
                {
                    if (Request["Password"] != "" && Request["ConfirmPassword"] != "")
                    {
                        if (!Request["Password"].Equals(Request["ConfirmPassword"]))
                        {
                            TempData["message"] = "0Password does not match!!!";
                        }
                        else
                        {
                            ManageUserViewModel objUserVm = new ManageUserViewModel();
                            bool isSuccess = await ChangePassword(objUserVm, Request["OldPassword"], Request["Password"], Request["ConfirmPassword"]);
                            if (isSuccess == true)
                            {
                                TempData["message"] = "1Password changed successfully!";
                            }
                            else
                            {
                                TempData["message"] = "0Old Password does not match!";
                            }
                        }
                    }
                }
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult AccountInformation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AccountInformation(int? id)
        {
            if (Request["OldPassword"] != "")
            {
                if (Request["Password"] != "" && Request["ConfirmPassword"] != "")
                {
                    if (!Request["Password"].Equals(Request["ConfirmPassword"]))
                    {
                        TempData["message"] = "0Password does not match!!!";
                    }
                    else
                    {
                        ManageUserViewModel objUserVm = new ManageUserViewModel();
                        bool isSuccess = await ChangePassword(objUserVm, Request["OldPassword"], Request["Password"], Request["ConfirmPassword"]);
                        if (isSuccess == true)
                        {
                            TempData["message"] = "1Password changed successfully!";
                        }
                        else
                        {
                            TempData["message"] = "0Old Password does not match!";
                        }
                    }
                }
            }
            return View();
        }
        #endregion

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        public bool UserValidation(string username, string password, string confirmPassword)
        {
            bool isValidate = true;
            if (_dbCtx.Users.Where(i => i.UserName == username).Count() > 0)
            {
                isValidate = false;
                TempData["message"] =  DbUtility.GetStatusMessage(DbUtility.Status.Exist);
            }
            else
            {
                if (!password.Equals(confirmPassword))
                {
                    isValidate = false;
                    TempData["message"] = "0Password does not match!!!";
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
            ViewBag.ReturnUrl = Url.Action("Manage");
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
    }
}