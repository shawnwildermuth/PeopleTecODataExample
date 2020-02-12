﻿using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Persontec.Api.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Persontec.Api.Controllers
{
  public class EmployeesODataController : ODataController
  {
    private readonly HrContext _ctx;
    private readonly IMapper _mapper;

    public EmployeesODataController(HrContext ctx, IMapper mapper)
    {
      _ctx = ctx;
      _mapper = mapper;
    }

    [EnableQuery()]
    public IQueryable<Employee> Get()
    {
      return _ctx.Employees;
    }

    [EnableQuery()]
    public IQueryable<Employee> Get(int key)
    {
      return _ctx.Employees.Where(c => c.EmployeeId == key);
    }

    [EnableQuery]
    public IQueryable<EmploymentPeriod> GetEmploymentPeriods(int key) 
    {
      return _ctx.Employees.Where(c => c.EmployeeId == key).SelectMany(p => p.EmploymentPeriods);
    }

    [EnableQuery]
    public SingleResult<Organization> GetOrganization(int key)
    {
      var result = _ctx.Employees
      //.Include(c => c.Organization)
      .Where(c => c.EmployeeId == key)
      .Select(p => p.Organization);

      return SingleResult.Create(result);
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
