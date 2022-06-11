using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Interfaces;
using ZealandDimselab.MockData;
using ZealandDimselab.Models;
using ZealandDimselab.Services;
using ZealandDimselab.Services.Interfaces;

namespace ZealandDimselab.Helpers
{
    /// <summary>
    /// This class contains all of our custom services injection.
    /// </summary>
    public static class DISetup
    {
        public static IServiceCollection AddDIInfo(this IServiceCollection services)
        {
         
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            // DATABASE END //

            // SERVICES START //
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IItemService, MockDataItems>();
            services.AddTransient<IBookingService, BookingService>();

            services.AddTransient<ICategoryService, MockCategory>();

            return services;
        }
    }
}
