using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Data
{
  public class HrContext : DbContext
  {
    private readonly IConfiguration _config;

    public HrContext(IConfiguration config)
    {
      _config = config;
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder bldr)
    {
      base.OnConfiguring(bldr);
      bldr.UseSqlServer(_config.GetConnectionString("PeopleTecConnection"));
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    {
      base.OnModelCreating(bldr);

      bldr.Entity<Organization>()
        .HasOne(c => c.VicePresident);

      bldr.Entity<Organization>()
        .HasData(
        new Organization {
          OrganizationId = 1,
          OrganizationName = "Group J",
          OrganizationCode = 12345,
          VicePresidentId = 1
        });

      bldr.Entity<Employee>()
        .HasData(new Employee()
        {
          EmployeeId = 1,
          FirstName = "Dennis",
          LastName = "Dunaway",
          Supervisor = null,
          EmployeeNumber = 101
        });

      bldr.Entity<EmploymentPeriod>()
       .HasData(new
       {
         EmploymentPeriodId = 1,
         StartingDate = DateTime.Parse("2015-01-01"),
         EndingDate = DateTime.Parse("2020-02-13"),
         Status = "Active",
         EmployeeId = 1
       });
    }

  }
}
