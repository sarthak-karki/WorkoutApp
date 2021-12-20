using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using WorkoutTracker_v2.Models;

namespace WorkoutTracker_v2.Services
{
    public interface IAccountService
    {
        ClaimsPrincipal GetClaimsPrincipal(UserProfile user);
    }

    public class AccountService : IAccountService
    {
        public ClaimsPrincipal GetClaimsPrincipal(UserProfile user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
