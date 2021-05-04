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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public DimselabDbContext()
        {
            
        }

        public DimselabDbContext(DbContextOptions<DimselabDbContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ItemCategory>()
                .HasKey(ic => new {ic.Id, ic.CategoryId});

            modelBuilder.Entity<Models.ItemCategory>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.ItemCategories)
                .HasForeignKey(ic => ic.Id);

            modelBuilder.Entity<ItemCategory>()
                .HasOne(ic => ic.Category)
                .WithMany(c => c.ItemCategories)
                .HasForeignKey(ic => ic.CategoryId);




            //modelBuilder.Entity<Item>()
            //    .HasMany(i => i.Categories)
            //    .WithMany(c => c.Items)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "PostTag",
            //        j => j
            //            .HasOne<Category>()
            //            .WithMany()
            //            .HasForeignKey("CategoryId")
            //            .HasConstraintName("FK_CategoryItem_Categories_CategoriesCategoryId")
            //            .OnDelete(DeleteBehavior.Cascade),
            //        j => j
            //            .HasOne<Item>()
            //            .WithMany()
            //            .HasForeignKey("ItemId")
            //            .HasConstraintName("FK_CategoryItem_Items_ItemsId")
            //            .OnDelete(DeleteBehavior.ClientCascade));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) // If no options provided by DimselabDbContext constructor, use this:
            {
                options.UseSqlServer(@"Server=tcp:dimselab.database.windows.net,1433;Initial Catalog=dimselabDb;Persist Security Info=False;User ID=dimselabadmin;Password=516zVIbTxK5T;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }


    }
}