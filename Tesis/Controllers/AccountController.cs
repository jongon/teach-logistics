using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MvcFlash.Core.Extensions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tesis.DAL;
using Tesis.Models;
using Tesis.ViewModels;

namespace Tesis.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            ViewBag.Roles = RoleManager.Roles.ToList();
            var users = UserManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<JsonResult> GetUsersBySection(string UserName, string SectionId)
        {
            if (String.IsNullOrEmpty(SectionId))
            {
                return Json("Error");
            }
            var users = await UserManager.Users.Where(
                x => (x.FirstName.Contains(UserName) || 
                x.LastName.Contains(UserName) || 
                x.Email.Contains(UserName)) &&
                x.SectionId.ToString().Equals(SectionId))
                .Select(
                    c => new { 
                        Id = c.Id,
                        UserName = c.FirstName + " " + c.LastName,
                        FirstName = c.FirstName,
                        LastName = c.LastName
                    })
                .ToListAsync();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.Users.Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();

            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                var userModel = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    IdCard = user.IdCard,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Section = (user.Section == null) ? null : user.Section
                };
                ViewBag.Roles = UserManager.GetRoles(user.Id);
                return View(userModel);
            }
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = UserManager.GetRoles(user.Id);
            EditUserViewModel editedUser = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdCard = user.IdCard,
                EmailConfirmed = user.EmailConfirmed,
                Password = "",
                ConfirmPassword = "",
                SectionId = (user.SectionId == null) ? null : user.SectionId,
                SemesterId = (user.SectionId == null) ? Guid.Empty : user.Section.SemesterId
            };
            return View(editedUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Email, FirstName, LastName, IdCard, Password, ConfirmPassword, SectionId, SemesterId, EmailConfirmed")] EditUserViewModel userViewModel)
        {
            var rolesUser = UserManager.GetRoles(userViewModel.Id);
            var roleCurrentUser = UserManager.GetRoles(User.Identity.GetUserId());
            if (!rolesUser.Contains("Estudiante") || roleCurrentUser.Contains("Estudiante"))
            {
                ModelState.Remove("SectionId");
                ModelState.Remove("SemesterId");
                ModelState.Remove("EmailConfirmed");
            }
            else if (userViewModel.SectionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }

            if (String.IsNullOrEmpty(userViewModel.Password) && String.IsNullOrEmpty(userViewModel.ConfirmPassword))
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = UserManager.FindById(userViewModel.Id);
                    user.UserName = userViewModel.Email;
                    user.Email = userViewModel.Email;
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.IdCard = userViewModel.IdCard;
                    user.EmailConfirmed = userViewModel.EmailConfirmed;
                    user.SectionId = userViewModel.SectionId;
                    if (!String.IsNullOrEmpty(userViewModel.Password))
                    {
                        var passwordHash = new PasswordHasher();
                        user.PasswordHash = passwordHash.HashPassword(userViewModel.Password);
                    }
                    await UserManager.UpdateAsync(user);
                    Flash.Success("OK", "Usuario editado exitosamente");
                    if (roleCurrentUser.Contains("Administrador"))
                    {
                        return RedirectToAction("Index");
                    }
                    else if (roleCurrentUser.Contains("Estudiante"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception e)
                {
                    Flash.Error("Error", e.Message);
                    return View(userViewModel);
                }
            }
            ViewBag.Roles = rolesUser;
            Flash.Error("Error", "Ha ocurrido un error editando el usuario");
            return View(userViewModel);
        }

        public async Task<ActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = UserManager.GetRoles(user.Id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteConfirmation(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
                Flash.Success("OK!", "Usuario eliminado exitosamente");
                return RedirectToAction("Index");
            }
            Flash.Error("Error", "No se ha podido eliminar el usaurio");
            return View(user);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!AuthenticationManager.User.Identity.IsAuthenticated)
                return View();
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Flash.Error("Error", "Ha ocurrido un error iniciando sesión");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = await UserManager.FindByEmailAsync(model.Email);
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (user!= null && !await UserManager.IsEmailConfirmedAsync(user.Id) && result == SignInStatus.Success)
            {
                return RedirectToAction("Confirmation", "Account", new { UserId = user.Id, returnUrl = returnUrl, rememberMe = model.RememberMe });
            }
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { returnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    Flash.Error("Error", "Intento de Inicio de sesión inválido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "Administrador")]
        public ActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel();
            ViewBag.Roles = register.Roles;
            return View();
        }

        // POST: /Account/Register
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Email, FirstName, LastName, IdCard, Password, ConfirmPassword, SemesterId, SectionId, RoleName")] RegisterViewModel model)
        {
            RegisterViewModel register = new RegisterViewModel();
            ViewBag.Roles = register.Roles;
            if (ModelState.IsValid)
            {
                if (model.SectionId == null)
                {
                    Flash.Error("Error", "Ha Ocurrido un error");
                    return View(model);
                }
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IdCard = model.IdCard,
                    EmailConfirmed = false,
                };

                if (model.RoleName == "Estudiante")
                {
                    user.SectionId = model.SectionId;
                }

                var result = await UserManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.RoleName);
                    ViewBag.SectionId = model.SectionId.ToString();
                    ViewBag.SemesterId = model.SemesterId.ToString();
                    ModelState.Clear();
                    Flash.Success("Ok", "Usuario fue registrado exitosamente");
                    return View();
                }
                AddErrors(result);
            }
            foreach (ModelState modelState in ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Flash.Error("Error", error.ErrorMessage);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Confirmation(string userId, string returnUrl, bool rememberMe = false)
        {
            AuthenticationManager.SignOut();
            if (String.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            ViewBag.userId = userId;
            ViewBag.returnUrl = returnUrl;
            ViewBag.rememberMe = rememberMe;
            return View(new ConfirmationViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IdCard = user.IdCard
                }
            );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Confirmation([Bind(Include = "Id, Email, FirstName, LastName, IdCard, Password, ConfirmPassword")] ConfirmationViewModel model, string returnUrl, bool rememberMe = false)
        {
            if (ModelState.IsValid) {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
                }
                else if (user.EmailConfirmed)
                {
                    Flash.Error("Error", "Este usuario ya ha sido confirmado");
                    return RedirectToAction("Login", "Account");
                }
                PasswordHasher passwordHash = new PasswordHasher();
                passwordHash.HashPassword(model.Password);
                user.Email = model.Email;
                user.UserName = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IdCard = model.IdCard;
                user.PasswordHash = passwordHash.HashPassword(model.Password);
                user.EmailConfirmed = true;
                var updateResult = await UserManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", "Intento de confirmación inválido.");
                    AddErrors(updateResult);
                }
                else
                {
                    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, rememberMe, shouldLockout: false);
                    if (result == SignInStatus.Success)
                    {
                        Flash.Success("Ok", "Usuario Confirmado exitosamente");
                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("", "Intento de inicio de sesión inválido.");
                }
            }
            foreach (ModelState modelState in ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Flash.Error("Error", error.ErrorMessage);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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