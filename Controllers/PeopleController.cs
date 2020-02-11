using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Persontec.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PeopleController : ControllerBase
  {
    private readonly PersonContext _ctx;

    public PeopleController(PersonContext ctx)
    {
      _ctx = ctx;
    }

    [EnableQuery()]
    public IQueryable<Person> GetPeopleQuery()
    {
      if (!this.User.IsInRole("Admin"))
      {
        return _ctx.People.Where(c => c.City != "Atlanta");
      }

      return _ctx.People;
    }

    [HttpGet]
    public IQueryable<Person> Get()
    {
      return _ctx.People;
    }
  }
}
