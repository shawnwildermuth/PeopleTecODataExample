using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using Persontec.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {
    private readonly ILogger<EmployeesController> _logger;
    private readonly IHrRepository _repository;
    private readonly IMapper _mapper;

    public EmployeesController(ILogger<EmployeesController> logger,
      IHrRepository repository,
      IMapper mapper)
    {
      _logger = logger;
      _repository = repository;
      _mapper = mapper;
    }

    /// <summary>
    /// Get all employees, sorted by employee number.
    /// </summary>
    /// <param name="includeOrgs">Option to include organization data in the result.</param>
    /// <returns>Collection of <see cref="EmployeeViewModel"/></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeViewModel>), 200)]
    public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> Get(bool includeOrgs = false)
    {
      var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(
        await _repository.GetAllEmployees(includeOrgs));

      return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetEmployee")]
    [ProducesResponseType(typeof(EmployeeViewModel), 200)]
    public async Task<ActionResult<EmployeeViewModel>> Get(int id)
    {
      var employee = await _repository.GetEmployee(id);
      if (employee == null) return NotFound();

      return Ok(_mapper.Map<EmployeeViewModel>(employee));
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeViewModel), 201)]
    [ProducesResponseType(400)]
    [ProducesErrorResponseType(typeof(string))]
    public async Task<ActionResult<EmployeeViewModel>> Post([FromBody] EmployeeViewModel model)
    {
      try
      {
        var newEmployee = _mapper.Map<Employee>(model);
        _repository.Add(newEmployee);
        var org = await _repository.GetOrganization(model.OrganizationCode);
        if (org != null) newEmployee.Organization = org;

        if (await _repository.SaveAllAsync())
        {
          return CreatedAtRoute("GetEmployee", new { id = newEmployee.EmployeeId }, _mapper.Map<EmployeeViewModel>(newEmployee));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Bad things happen to good developers {ex}");
      }

      return BadRequest("Database failure..");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeViewModel>> Put(int id, [FromBody] EmployeeViewModel model)
    {
      try
      {
        var oldEmployee = await _repository.GetEmployee(id);
        if (oldEmployee == null) return NotFound();

        _mapper.Map(model, oldEmployee);

        if (await _repository.SaveAllAsync())
        {
          return Ok(oldEmployee);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Bad things happen to good developers {ex}");
      }

      return BadRequest("Database failure..");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {

      try
      {
        var oldEmployee = await _repository.GetEmployee(id);
        if (oldEmployee == null) return NotFound();
        _repository.Delete(oldEmployee);
        if (await _repository.SaveAllAsync())
        {
          return Ok();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Bad things happen to good developers {ex}");
        return StatusCode(500);
      }

      return BadRequest();
    }
  }
}




