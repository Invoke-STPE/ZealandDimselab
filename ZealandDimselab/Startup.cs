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
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;
using ZealandDimselab.Services;
using ZealandDimselab.Services.Interfaces;

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


            // DATABASE START //
            services.AddDbContext<DimselabDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            // DATABASE END //

            // SERVICES START //
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IBookingService, BookingService>();

            services.AddTransient<ICategoryService, CategoryService>();
            // SERVICES END //

            // SESSION START //
            services.AddSession(); // Adds the ability to save into the users cache
            // SESSION END

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

                        //options.Conventions.AuthorizeFolder("/Items");
                        //options.Conventions.AllowAnonymousToPage("/Items/AllItems");
                        //options.Conventions.AllowAnonymousToPage("/Items/ItemDetails");
                        //options.Conventions.AllowAnonymousToPage("/Items/Cards/AllItems");
                        //options.Conventions.AllowAnonymousToPage("/Items/Cards/ItemDetails");


                        options.Conventions.AuthorizeFolder("/BookingPages");
                        options.Conventions.AllowAnonymousToPage("/BookingPages/BookingCart");
                        options.Conventions.AllowAnonymousToPage("/BookingPages/MyBookings");


                    }
                );
            // AUTHENTICATION END //
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

            // SESSION START //
            app.UseSession(); // Starter automatisk en session med brugeren.
            // SESSION END // 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
