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
using ZealandDimselab.Services.Interfaces;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Used for login through blazor component.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(string paramEmail = "")
        {
            if (String.IsNullOrWhiteSpace(paramEmail) == false)
            {
                bool emailInUse = await _userService.EmailInUseAsync(paramEmail);
                if (emailInUse == false)
                {
                    User user = new User(paramEmail, "student");
                    await _userService.AddUserAsync(user);
                }
                ClaimsIdentity claimsIdentity = await _userService.CreateClaimIdentity(paramEmail);
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = Request.Host.Value
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);
                return RedirectToPage("../Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (await _userService.ValidateEmail(Email, Password))
            {
                var claimsIdentity = await _userService.CreateClaimIdentity(Email);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("../Index");
            }

            Message = "Invalid attempt";
            return Page();
        }
    }
}
