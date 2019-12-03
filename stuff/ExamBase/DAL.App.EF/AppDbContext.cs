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
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkInOrder> DrinksInOrder { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PizzaInOrder> PizzasInOrder { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<ToppingOnPizza> ToppingsOnPizzas { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Order>()
                .Property(p => p.OrderState)
                .HasConversion<int>();
        }
    }
    
    
}