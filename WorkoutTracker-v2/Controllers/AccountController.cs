using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkoutTracker_v2.Models;
using WorkoutTracker_v2.Repositories;
using System.Linq;
using WorkoutTracker_v2.Services;

namespace WorkoutTracker_v2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserProfileRepository _userRepository;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IUserProfileRepository userRepository, IAccountService accountService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accountService = accountService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = _userRepository.GetUsernameAndPassword(model.Username, model.Password);

            if (user == null)
            {
                model.ReturnUrl = "/Home/Error";
                return LocalRedirect(model.ReturnUrl);
            }

            var principal = _accountService.GetClaimsPrincipal(user);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);

        }

        [AllowAnonymous]
        public IActionResult LoginWithGoogle(string returnUrl = "/")
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback"),
                Items =
                {
                    { "returnUrl", returnUrl }
                }
            };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }     

        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(
                ExternalAuthenticationDefaults.AuthenticationScheme);

            var externalClaims = result.Principal.Claims.ToList();

            var userName = externalClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var userEmail = externalClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var user = _userRepository.GetUserByEmail(userEmail);

            if (user == null)
            {
                _userRepository.InsertUser(userName, userEmail);
                user = _userRepository.GetUserByEmail(userEmail);
            }

            var principal = _accountService.GetClaimsPrincipal(user);

            await HttpContext.SignOutAsync(ExternalAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return LocalRedirect(result.Properties.Items["returnUrl"]);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return Content("This View is not yet implemented");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:44393/");
            return Redirect("https://localhost:44393/");
        }
    }
}
