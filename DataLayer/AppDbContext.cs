using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<PizzaModel> PizzaModel { get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }
    }
}
