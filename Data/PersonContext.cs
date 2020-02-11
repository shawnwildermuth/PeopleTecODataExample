using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Data
{
  public class PersonContext : DbContext
  {
    private readonly IConfiguration _config;

    public PersonContext(IConfiguration config)
    {
      _config = config;
    }

    public DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder bldr)
    {
      base.OnConfiguring(bldr);
      bldr.UseSqlServer(_config.GetConnectionString("PeopleConnection"));
    }

    protected override void OnModelCreating(ModelBuilder bldr)
    {
      base.OnModelCreating(bldr);

      bldr.Entity<Person>()
        .HasData(new
        {
          Id = 1,
          Name = "Shawn",
          City = "Atlanta"
        });
    }
  }
}
