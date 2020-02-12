using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Data
{
  public class HrRepository : IHrRepository
  {
    private readonly HrContext _context;

    public HrRepository(HrContext context)
    {
      _context = context;
    }

    public void Add<T>(T entity) where T : new()
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : new()
    {
      _context.Remove(entity);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees(bool includeOrganization = false)
    {
      IQueryable<Employee> qry = _context.Employees
        .OrderBy(c => c.EmployeeNumber);

      if (includeOrganization)
      {
        qry = qry.Include(c => c.Organization);
      }

      return await qry.ToArrayAsync();
    }

    public async Task<Employee> GetEmployee(int id)
    {
      return await _context.Employees.Where(c => c.EmployeeId == id).FirstOrDefaultAsync();
    }

    public async Task<Organization> GetOrganization(int organizationCode)
    {
      return await _context.Organizations
        .Where(o => o.OrganizationCode == organizationCode)
        .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}
