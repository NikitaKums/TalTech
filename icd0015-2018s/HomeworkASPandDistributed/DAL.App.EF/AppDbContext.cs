using System;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Inventory> Inventory  { get; set; }
        public DbSet<ManuFacturer> ManuFacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInOrder> ProductsInOrder { get; set; }
        public DbSet<ProductReturned> ProductsReturned { get; set; }
        public DbSet<ProductSold> ProductsSold { get; set; }
        public DbSet<ProductWithDefect> ProductsWithDefect { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductInCategory> ProductsInCategory { get; set; }
        
        public DbSet<MultiLangString> MultiLangStrings { get; set; }
        public DbSet<Translation> Translations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }
            
            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}