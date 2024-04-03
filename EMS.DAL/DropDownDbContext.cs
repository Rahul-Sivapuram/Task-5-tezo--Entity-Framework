using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EMS.DAL;

public class DropDownDbContext : DbContext
{
    public DbSet<DropDown> Location { get; set; }
    public DbSet<DropDown> Department { get; set; }
    public DbSet<DropDown> Project { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=10.0.0.27;Database=rahul;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<DropDown>().ToTable("Location");
         modelBuilder.Entity<DropDown>().ToTable("Department");
         modelBuilder.Entity<DropDown>().ToTable("Project");
    }
}
