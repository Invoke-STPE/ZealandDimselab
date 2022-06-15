using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Helpers;
using ZealandDimselab.Helpers.HttpClients;

namespace ZealandDimselab
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSession();
            services.AddSingleton<HttpClient>();
            services.AddTransient<IHttpClientItem, HttpClientItem>();
            services.AddTransient<IHttpClientCategory, HttpClientCategory>();
            services.AddTransient<IHttpClientUser, HttpClientUser>();
            services.AddTransient<IHttpClientBooking, HttpClientBooking>();

            // AUTHENTICATION START //
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "admin"));
            });
            services.AddMvc()
                .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AuthorizeFolder("/Account");
                        options.Conventions.AllowAnonymousToPage("/Account/Login");
                        options.Conventions.AuthorizeFolder("/BookingPages");
                        options.Conventions.AllowAnonymousToPage("/BookingPages/BookingCart");
                        options.Conventions.AllowAnonymousToPage("/BookingPages/MyBookings");


                    }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSession(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
