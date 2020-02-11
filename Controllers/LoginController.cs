using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoginController : ControllerBase
  {
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<string>>> GetToken()
    {


      return Ok(new List<string> { "One", "Two", "Three" });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<string>>> GetToken(int id, string one = "One", int page = 0, int pageSize = 10)
    {
      return Ok(new
      {
        count = 25,
        results = new List<string> { one, "Two", "Three" }
      });
    }



  }
}
