using Persontec.Api.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persontec.Api.Data
{
  public interface IHrRepository
  {
    Task<IEnumerable<Employee>> GetAllEmployees(bool includeOrganization = false);
    Task<bool> SaveAllAsync();
    void Add<T>(T entity) where T : new();
    Task<Employee> GetEmployee(int id);
    Task<Organization> GetOrganization(int organizationCode);
    void Delete<T>(T entity) where T : new();
  }
}