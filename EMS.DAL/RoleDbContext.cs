using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL;

public class RoleDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=10.0.0.27;Database=rahul;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        base.OnConfiguring(optionsBuilder);
    }
}
