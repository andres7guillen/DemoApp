using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options): base(options) 
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Company);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }



    }
}
