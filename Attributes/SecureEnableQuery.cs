using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Attributes
{
  public class SecureEnableQuery : EnableQueryAttribute
  {
    public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
    {
      return base.ApplyQuery(queryable, queryOptions);
    }

    public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
    {
      base.ValidateQuery(request, queryOptions);
    }

    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    {
      base.OnActionExecuted(actionExecutedContext);
    }

    public override object ApplyQuery(object entity, ODataQueryOptions queryOptions)
    {
      return base.ApplyQuery(entity, queryOptions);
    }

    public override IEdmModel GetModel(Type elementClrType, HttpRequest request, ActionDescriptor actionDescriptor)
    {
      return base.GetModel(elementClrType, request, actionDescriptor);
    }

  }
}
