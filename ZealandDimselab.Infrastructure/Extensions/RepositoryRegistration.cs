using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;
using ZealandDimselab.Infrastructure.InMemoryDataBase;

namespace ZealandDimselab.Infrastructure.Extensions
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBookingRepository, BookingRepository>();
        }
    }
}
