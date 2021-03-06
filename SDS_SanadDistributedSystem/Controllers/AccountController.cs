﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SDS_SanadDistributedSystem.Models;
using System.Collections.Generic;

namespace SDS_SanadDistributedSystem.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "superadmin,admin")]
    public class AccountController : BaseController
    {
        private bool[] enable = { true, false };
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private sds_dbEntities db = new sds_dbEntities();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Authorize(Roles = "superadmin,admin")]
        public ActionResult AdminPage(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
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
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult SmartLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SmartLogin(LoginViewModel model, string returnUrl)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {

                case SignInStatus.Success:
                    var login_user = db.AspNetUsers.Where(u => u.Email.Equals(model.Email));
                    //   var roles = login_user.First().AspNetRoles.First().Name;
                    // var allroles = db.AspNetRoles;
                    List<string> roles = new List<string>();
                    foreach (var r in login_user.First().AspNetRoles)
                        roles.Add(r.Name);

                    string action_name = "";
                    string controller_name = "";
                    Session["UserName"] = login_user.First().ShowName;

                    if (roles.Count > 0)
                    {
                        if (roles.Contains("caseManager"))
                        {
                            action_name = "index";
                            controller_name = "referalpersons";
                        }
                        if (roles.Contains("cmIOutReachTeam"))
                        {
                            action_name = "IndexOutReach";
                            controller_name = "referalpersons";
                        }
                        if (roles.Contains("coEducation") || roles.Contains("coProfessional") || roles.Contains("coChildProtection")
                            || roles.Contains("coPsychologicalSupport")
                            || roles.Contains("coDayCare") || roles.Contains("coHomeCare")
                            || roles.Contains("coAwareness")
                            || roles.Contains("coSmallProjects") || roles.Contains("coOutReachTeam") || roles.Contains("coInkindAssistance"))
                        {
                            action_name = "IndexCo";
                            controller_name = "referalpersons";
                        }
                        if (roles.Contains("receptionist"))
                        {
                            action_name = "index";
                            controller_name = "families";
                        }
                        if (roles.Contains("cmSGBV"))
                        {
                            action_name = "index";
                            controller_name = "secretPeople";
                        }
                        if (roles.Contains("reporter"))
                        {
                            action_name = "index";
                            controller_name = "Reporting";
                        }
                        if (roles.Contains("admin"))
                        {
                            action_name = "AdminPage";
                            controller_name = "Account";
                        }

                    }

                    else
                    {
                        action_name = "index";
                        controller_name = "Account";
                    }

                    return RedirectToAction(action_name, controller_name);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "محاولة تسجيل دخول خاطئة");
                    return View(model);
            }
        }


        //
        // GET: /Account/VerifyCode
        //        [AllowAnonymous]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        //      [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //public JsonResult IsRolesEmpty(string roleName)
        //{
        //    try
        //    {
        //        return Json(checkRoles(roleName));
        //    }
        //    catch { return Json(true); }
        //}
        //public bool checkRoles(string roleName)
        //{
        //    // Assume these details coming from database  
        //    //List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

        //    //var RegUserName = (from u in db.AspNetUsers
        //    //                   where u.UserName.ToUpper() == UserName.ToUpper()
        //    //                   select new { UserName }).FirstOrDefault();

        //    bool status;
        //    if (roleName == null)
        //    {
        //        //Already registered  
        //        status = false;
        //    }
        //    else
        //    {
        //        //Available to use  
        //        status = true;
        //    }

        //    return status;
        //}

        public JsonResult CheckUserName(string Email)
        {
            //   sds_dbEntities db = new sds_dbEntities();
            ApplicationDbContext db = new ApplicationDbContext();
            var result = true;
            var user = db.Users.Where(x => x.Email == Email).FirstOrDefault();

            if (user != null)
                result = false;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsAlreadySignedUserName(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
                return Json(IsUserAvailable(UserName));
            return Json(true);
        }
        public bool IsUserAvailable(string UserName)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegUserName = (from u in db.AspNetUsers
                               where u.UserName.ToUpper() == UserName.ToUpper()
                               select new { UserName }).FirstOrDefault();

            bool status;
            if (RegUserName != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public JsonResult IsAlreadySignedEmail(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
                return Json(IsEmailAvailable(Email));
            return Json(true);
        }
        public bool IsEmailAvailable(string Email)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegEmail = (from u in db.AspNetUsers
                            where u.Email.ToUpper() == Email.ToUpper()
                            select new { Email }).FirstOrDefault();

            bool status;
            if (RegEmail != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public JsonResult IsAlreadySignedPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
                return Json(IsPhoneAvailable(phone));
            return Json(true);

        }
        public bool IsPhoneAvailable(string phone)
        {
            // Assume these details coming from database  
            List<RegisterViewModel> RegisterUsers = new List<RegisterViewModel>();

            var RegEmail = (from u in db.AspNetUsers
                            where u.PhoneNumber == phone
                            select new { phone }).FirstOrDefault();

            bool status;
            if (RegEmail != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }
        //
        // GET: /Account/Register
        //[AllowAnonymous]
        [Authorize(Roles = "superadmin,admin")]
        public ActionResult Register(string idcenter_FK)
        {
            RegisterViewModel register = new RegisterViewModel();
            if (!string.IsNullOrEmpty(idcenter_FK))
                ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", idcenter_FK);
            else
                ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");

            ViewBag.RolesID = new SelectList(db.AspNetRoles, "Name", "NameAR", db.AspNetRoles.First().NameAR);
            ViewBag.enableOptions = enable;
            return View(register);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> Register(RegisterViewModel model, string[] RolesID)//, string idCenter_FK)//
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, enabled = model.enabled, idcenter_FK = model.idcenter_FK, ShowName = model.ShowName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    if (RolesID != null)
                        await this.UserManager.AddToRolesAsync(user.Id, RolesID);
                    //return new JsonResult { Data = "" };//
                    RedirectToAction("Register", "Account");
                }
                if (result.Succeeded == false)
                {
                    var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
                    throw new Exception(exceptionText);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            //return new JsonResult { Data = "" };//
            //           return View(model);
            RegisterViewModel register = new RegisterViewModel();
            if (!string.IsNullOrEmpty(model.idcenter_FK))
                ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name", model.idcenter_FK);
            else
                ViewBag.idcenter_FK = new SelectList(db.centers, "idcenter", "name");

            ViewBag.RolesID = new SelectList(db.AspNetRoles, "Name", "NameAR", db.AspNetRoles.First().NameAR);
            ViewBag.enableOptions = enable;

            return RedirectToAction("Index", "AspNetUsers");
        }

        //
        // GET: /Account/ConfirmEmail
        //        [AllowAnonymous]
        //     [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        //        [AllowAnonymous]
        //   [Authorize(Roles = "superadmin,admin")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        //        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //     [Authorize(Roles = "superadmin,admin")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        //   [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        // [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //   [Authorize(Roles = "superadmin,admin")]

        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        //        [AllowAnonymous]
        //     [Authorize(Roles = "superadmin,admin")]

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        //        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //     [Authorize(Roles = "superadmin,admin")]

        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        //        [AllowAnonymous]
        [Authorize(Roles = "superadmin,admin")]

        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        //        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin")]

        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        //        [AllowAnonymous]
        [Authorize(Roles = "superadmin,admin")]

        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        //        [AllowAnonymous]
        [Authorize(Roles = "superadmin,admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("SmartLogin", "Account");
        }


        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}