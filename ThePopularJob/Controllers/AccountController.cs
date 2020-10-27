using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ThePopularJob.ViewModels;
using JobsLibrary;

namespace ThePopularJob.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,
                    isCompany = model.isCompany, Name = model.Name};
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (user.isCompany) 
                        result = await userManager.AddToRoleAsync(user, Role.Company);
                    else 
                        result = await userManager.AddToRoleAsync(user, Role.User);

                    if (result.Succeeded)
                    {
                        // send confirmation link
                        return RedirectToRegistrationMessageView();
                    }
                }
                PrintModelErrors(result.Errors);
            }
            return View();
        }


        // PRIVATE METHODS
        private void PrintModelErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        private IActionResult RedirectToRegistrationMessageView()
        {
            ViewBag.ErrorTitle = "Registration successful";
            //ViewBag.ErrorMessage = "Before you can login, please confirm your email by clicking on the confirmatoin link we have sent you";
            ViewBag.ErrorMessage = "Welcome to The Popular Job! You can now login with your username and password.";
            return View("Error");
        }
    }
}
