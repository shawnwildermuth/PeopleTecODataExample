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
    public DbSet<Transfer> Transfers { get; set; }

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
        new
        {
          OrganizationId = 1,
          OrganizationName = "Group J",
          OrganizationCode = 12345,
          VicePresidentId = 1
        });

      bldr.Entity<Employee>()
        .HasOne(e => e.Organization)
        .WithOne()
        .HasForeignKey<Organization>(o => o.OrganizationId)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired(false);

      bldr.Entity<Employee>()
        .HasData(new
        {
          EmployeeId = 1,
          FirstName = "Dennis",
          LastName = "Dunaway",
          EmployeeNumber = 101,
          OrganizationId = 1
        }, new
        {
          EmployeeId = 2,
          FirstName = "James",
          LastName = "Smith",
          EmployeeNumber = 115,
          OrganizationId = 1,
          SupervisorId = 1
        }, new
        {
          EmployeeId = 3,
          FirstName = "Jake ",
          LastName = "Dwight",
          EmployeeNumber = 1016,
          OrganizationId = 1,
          SupervisorId = 2
        }, new
        {
          EmployeeId = 4,
          FirstName = "Tim",
          LastName = "Tunney",
          EmployeeNumber = 1010,
          OrganizationId = 1,
          SupervisorId = 2
        }, new
        {
          EmployeeId = 5,
          FirstName = "Alec",
          LastName = "Smart",
          EmployeeNumber = 102,
          OrganizationId = 1,
          SupervisorId = 4
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

      bldr.Entity<Transfer>()
        .HasData(new
        {
          TransferId = 1,
          EmployeeId = 1,
          StartingDate = DateTime.Parse("2015-01-01"),
          EndingDate = DateTime.Parse("2020-02-13"),
          CurrentOrganizationId = 1
        }, new
        {
          TransferId = 2,
          EmployeeId = 1,
          StartingDate = DateTime.Parse("2011-01-01"),
          EndingDate = DateTime.Parse("2014-12-31"),
          CurrentOrganizationId = 1
        });
    }

  }
}
