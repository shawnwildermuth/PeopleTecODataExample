using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  

  [Route("api/employee/{employeeid:int}/organization")]
  [ApiController]
  public class EmployeeOrganizationController : ControllerBase
  {
    private readonly ILogger<EmployeeOrganizationController> _logger;
    private readonly IHrRepository _repository;

    public EmployeeOrganizationController(ILogger<EmployeeOrganizationController> logger,
      IHrRepository repository)
    {
      _logger = logger;
      _repository = repository;

      var child = new Child
      {
        Name = "Shawn",
        Age = 10,
        BirthDate = DateTime.Now
      };

      
      var children = new List<Child>()
      {
        new Child(),
        new Child()
      };

    }

    [HttpGet]
    public async Task<ActionResult<Organization>> Get(int employeeid)
    {
      var employee = await _repository.GetEmployee(employeeid);
      if (employee == null) return NotFound();
      if (employee.Organization == null) return NotFound();

      return Ok(employee.Organization);
    }

  }
}
