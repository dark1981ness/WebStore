using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                UserName = model.UserName
            };

            var registration_result = await _userManager.CreateAsync(user, model.Password);

            if (registration_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role._users);

                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        #endregion

        #region Login
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login_result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
#if DEBUG
                false
#else
                true
#endif

                );

            if (login_result.Succeeded)
            {
                //if (Url.IsLocalUrl(model.ReturnUrl))
                //    return Redirect(model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль!");
            return View(model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
