using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persontec.Api.Attributes;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  public class EmployeesODataController : ODataController
  {
    private readonly HrContext _ctx;
    private readonly IMapper _mapper;

    public EmployeesODataController(HrContext ctx,
    IMapper mapper)
    {
      _ctx = ctx;
      _mapper = mapper;
    }

    [SecureEnableQuery(MaxExpansionDepth = 3)]
    public IQueryable<Employee> Get(ODataQueryOptions opt)
    {
      IQueryable<Employee> qry = _ctx.Employees;

      if (true) // User's is in VP's heiarchy
      {
        qry = qry.Where(c => c.EmployeeNumber < 1000);
      }

      return qry;
    }

    [EnableQuery(MaxExpansionDepth = 3)]
    public IQueryable<Employee> Get(int key)
    {
      var qry = _ctx.Employees.Where(c => c.EmployeeId == key);

      if (key == 1)
      {
        qry = qry.Select(c => _mapper.Map<Employee, Employee>(c)
        //new Employee()
        //{
        //  EmployeeId = c.EmployeeId,
        //  EmployeeNumber = c.EmployeeNumber,
        //  EmploymentPeriods = c.EmploymentPeriods,
        //  FirstName = c.FirstName,
        //  LastName = "YOUR NOT ALLOWED",
        //  DirectReports = c.DirectReports,
        //  Organization = c.Organization,
        //  Transfers = c.Transfers,
        //  OrganizationId = c.OrganizationId
        //}
        );
      }
      return qry;
    }

    [EnableQuery(MaxExpansionDepth = 3)]
    public IQueryable<EmploymentPeriod> GetEmploymentPeriods(int key)
    {
      return _ctx.Employees.Where(c => c.EmployeeId == key).SelectMany(p => p.EmploymentPeriods);
    }

    [EnableQuery(MaxExpansionDepth = 3)]
    public IQueryable<Transfer> GetTransfers(int key)
    {
      return _ctx.Employees.Where(c => c.EmployeeId == key).SelectMany(p => p.Transfers);
    }

    [EnableQuery(MaxExpansionDepth = 3)]
    public SingleResult<Organization> GetOrganization(int key)
    {
      var result = _ctx.Employees
      .Include(c => c.Organization)
      .ThenInclude(o => o.VicePresident)
      .Where(c => c.EmployeeId == key)
      .Select(p => p.Organization);

      return SingleResult.Create(result);
    }

    [EnableQuery]
    [ODataRoute("EmployeesOData/GetAllReports(EmployeeNumber={employeeNumber})")]
    public async Task<ActionResult<IEnumerable<Employee>>>
      GetAllReports(int employeeNumber)
    {
      var employee = await _ctx.Employees
        .Where(e => e.EmployeeNumber == employeeNumber)
        .FirstOrDefaultAsync();

      var reports = new List<Employee>();
      await GetDirectReports(employee, reports);

      return reports;
    }

    private async Task GetDirectReports(Employee employee, List<Employee> reports)
    {
      var results = await _ctx.Employees
        .Include(c => c.DirectReports)
        .Where(c => c.EmployeeId == employee.EmployeeId)
        .SelectMany(s => s.DirectReports)
        .ToListAsync();

      reports.AddRange(results);

      foreach (var report in results)
      {
        await GetDirectReports(report, reports);
      }
    }

    //[EnableQuery()]
    //public IQueryable<Employee> GetStartingEmployees()
    //{
    //  return _ctx.Employees.Where(c => c.EmployeeNumber < 500);
    //}


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
