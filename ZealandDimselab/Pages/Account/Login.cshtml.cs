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
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<string> _passwordHasher;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            User user = null;
            try
            {
                user = await GetUserFromApiByEmailAsync();
            }
            catch
            {
                Message = "Invalid attempt";
                return Page();
            }

            VerifyPassword(user);
            ClaimsIdentity claimsIdentity = CreateClaimIdentity(user);
            // Sign in
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            // Redirect
            return RedirectToPage("../Index");
        }

        public async Task<IActionResult> OnPostModalLoginAsync(string paramEmail, string url)
        {
            User user = null;

            try
            {
                user = await GetUserFromApiByEmailAsync(paramEmail);
            }
            catch 
            {
                Message = "Invalid attempt";
            }

            if (user != null)
            {
                ClaimsIdentity claimsIdentity = CreateClaimIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage(url);
            }
            return Page();
        }

        private string BuildUrl(string paramEmail = "")
        {
            var builder = new UriBuilder(_configuration.GetValue<string>("UserAPI:BaseUrlUser"));
            string email = string.IsNullOrWhiteSpace(paramEmail) ? Email : paramEmail;
            builder.Query = $"email={email}";
            string url = builder.ToString();
            return url;
        }

        private async Task<User> GetUserFromApiByEmailAsync(string paramEmail = "")
        {
            string url = string.IsNullOrWhiteSpace(paramEmail) ? BuildUrl(Email) : BuildUrl(paramEmail);
            var response = await _httpClient.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(jsonResult); ;
        }

        private void VerifyPassword(User user)
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(null, user.Password, Password);
        }

        private ClaimsIdentity CreateClaimIdentity(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Email),
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return claimsIdentity;
        }
    }
}
