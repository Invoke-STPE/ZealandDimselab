using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Models;

namespace ZealandDimselab.Models
{
    public class DimselabDbContext : DbContext
    {
        public DimselabDbContext()
        {
            
        }

        public DimselabDbContext(DbContextOptions<DimselabDbContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(@"Server=tcp:dimselab.database.windows.net,1433;Initial Catalog=dimselabDb;Persist Security Info=False;User ID=dimselabadmin;Password=516zVIbTxK5T;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
    }
}