using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly IHttpClientUser _httpClientUser;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IHttpClientUser httpClientUser)
        {
            _passwordHasher = new PasswordHasher<string>();
            _httpClientUser = httpClientUser;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            UserDto user = null;
            try
            {
                user = await _httpClientUser.GetUserByEmailAsync(Email);
            }
            catch
            {
                Message = "Invalid attempt";
                return Page();
            }

            var result = VerifyPassword(user);
            if (result == PasswordVerificationResult.Success)
            {
                ClaimsIdentity claimsIdentity = CreateClaimIdentity(user);
                // Sign in
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                // Redirect
                
            }
            return RedirectToPage("../Index");

        }

        public async Task<IActionResult> OnPostModalLoginAsync(string paramEmail, string url)
        {
            UserDto user = null;

            try
            {
                user = await _httpClientUser.GetUserByEmailAsync(paramEmail);
            }
            catch 
            {
                await _httpClientUser.AddUserAsync(paramEmail);
                user = await _httpClientUser.GetUserByEmailAsync(paramEmail);
            }
            
  
                
            

            if (user != null)
            {
                ClaimsIdentity claimsIdentity = CreateClaimIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage(url);
            }
            return Page();
        }

        private PasswordVerificationResult VerifyPassword(UserDto user)
        {
            return _passwordHasher.VerifyHashedPassword(null, user.Password, Password);
        }

        private ClaimsIdentity CreateClaimIdentity(UserDto user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return claimsIdentity;
        }
    }
}
