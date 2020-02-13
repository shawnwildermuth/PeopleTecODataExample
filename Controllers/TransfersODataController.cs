using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Persontec.Api.Attributes;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Controllers
{
  //[ODataRoutePrefix("Transfers")]
  public class TransfersODataController : ODataController
  {
    private HrContext _ctx;
    private IMapper _mapper;

    public TransfersODataController(HrContext ctx, IMapper mapper)
    {
      _ctx = ctx;
      _mapper = mapper;
    }

    [SecureEnableQuery(MaxExpansionDepth = 3)]
    public IQueryable<Transfer> Get()
    {
      return _ctx.Transfers;
    }

    [EnableQuery()]
    public IQueryable<Transfer> Get(int key)
    {
      return _ctx.Transfers.Where(c => c.TransferId == key);
    }

    //[EnableQuery()]
    //public SingleResult<Organization> GetCurrentOrganization(int key)
    //{
    //  return SingleResult.Create(_ctx.Transfers.Where(c => c.TransferId == key).Select(c => c.CurrentOrganization));
    //}

    //[EnableQuery()]
    //public SingleResult<Employee> GetEmployee(int key)
    //{
    //  return SingleResult.Create(_ctx.Transfers.Where(c => c.TransferId == key).Select(c => c.Employee));
    //}

  }
}
