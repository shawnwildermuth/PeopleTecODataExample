using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Persontec.Api.Controllers
{
  public class EmployeesODataController : ODataController
  {
    private readonly HrContext _ctx;

    public EmployeesODataController(HrContext ctx)
    {
      _ctx = ctx;
    }

    [EnableQuery()]
    public IQueryable<Employee> Get()
    {
      return _ctx.Employees;
    }

    [EnableQuery()]
    public IQueryable<Employee> GetStartingEmployees()
    {
      return _ctx.Employees.Where(c => c.EmployeeNumber < 500);
    }


    //[HttpGet]
    //public IQueryable<Person> Get()
    //{
    //  return _ctx.People;
    //}

    //[HttpPut("{id:int}")]
    //public async Task<IActionResult> Put(int id, [FromBody]Person model)
    //{

    //  using (var tx = new TransactionScope())
    //  {

    //    var history = new History()
    //    {
    //      User = User.Identity.Name,
    //      ChangeType = "Update",
    //      Fields = "Name, City",
    //      Time = DateTime.UtcNow
    //    };

    //    _ctx.History.Add(history);

    //    var person = _ctx.People.Where(p => p.Id == id).FirstOrDefault();
    //    if (person == null) return BadRequest();

    //    person.Name = model.Name;
    //    person.City = model.City;

    //    if (await _ctx.SaveChangesAsync() > 0)
    //    {
    //      return Ok(person);
    //    }

    //    tx.Complete();
    //  }
    //}
  }
}
