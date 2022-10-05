using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;
using ZealandDimselab.Infrastructure.InMemoryDataBase;

namespace ZealandDimselab.Infrastructure.Extensions
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
