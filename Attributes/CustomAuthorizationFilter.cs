using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OData.UriParser;

namespace Persontec.Api.Attributes
{
  public class CustomAuthorizationFilter : IAuthorizationFilter
  {
    public bool AllowMultiple { get { return false; } }

    public void OnAuthorization(
        AuthorizationFilterContext ctx)
    {
      var request = ctx.HttpContext.Request;
      var features = ctx.HttpContext.ODataFeature();
      //features.Path.Path.Append(new ODataPathSegment().)
      var bldr = new QueryBuilder();
      bldr.Add("$filter", "FirstName eq 'Dennis'");
      request.QueryString = bldr.ToQueryString();

      // check the auth
      //var request = ctx.actionContext.Request;
      //var odataPath = request.ODataProperties().Path;
      //if (odataPath != null && odataPath.NavigationSource != null &&
      //    odataPath.NavigationSource.Name == "Products")
      //{
      //  // only allow admin access
      //  IEnumerable<string> users;
      //  request.Headers.TryGetValues("user", out users);
      //  if (users == null || users.FirstOrDefault() != "admin")
      //  {
      //    throw new HttpResponseException(HttpStatusCode.Unauthorized);
      //  }
      //}

      //return continuation();
    }
  }
}
