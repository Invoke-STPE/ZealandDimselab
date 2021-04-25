using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }


        public async Task<IActionResult> OnPost()
        {

            if (_userService.ValidateLogin(Email, Password))
            {
                //LoggedInUser = user;
                //if (UserName == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Email)
                    };

                var claimsIdentity = _userService.CreateClaimIdentity(Email);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("../Index");
            }

            //List<User> users = _userService.GetUsers();
            //foreach (User user in users)
            //{
            //    if (Email == user.Email)
            //    {
            //        var passwordHasher = new PasswordHasher<string>();
            //        if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
            //        {
            //            //LoggedInUser = user;
            //            var claims = new List<Claim>
            //            {
            //                new Claim(ClaimTypes.Name, Email)
            //            };

            //            if (Email == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

            //            var claimsIdentity =
            //                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //                new ClaimsPrincipal(claimsIdentity));
            //            return RedirectToPage("../Privacy");
            //        }
            //    }
            //}

            Message = "Invalid attempt";
            return Page();
        }
    }
}
