using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private UserService _userService;

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

                if (UserName == user.UserName)
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        //LoggedInUser = user;
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, UserName)
                        };

                        if (UserName == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

                        var claimsIdentity =
                            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Item/GetAllItems");
                    }
                }

            Message = "Invalid attempt";
            return Page();
        }
    }
}
