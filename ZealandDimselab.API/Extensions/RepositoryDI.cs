﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Services;
using ZealandDimselab.API.DataAccess;
using ZealandDimselab.API.DataAccess.Interfaces;

namespace ZealandDimselab.API.Extensions
{
    /// <summary>
    /// This class contains all of our custom services injection.
    /// </summary>
    public static class RepositoryDI
    {
        public static IServiceCollection OldAddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IBookingRepository, BookingRepository>
            return services;
        }
    }
}
