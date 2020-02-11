using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Persontec.Api.Data;

namespace Persontec.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //services.AddIdentity()
      //  .AddJwtIdenty()

      //services.AddDbContext<MyContext>();
      services.AddDbContext<PersonContext>();
      services.AddMvc(opt => opt.EnableEndpointRouting = false);
      services.AddOData();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      //app.UseRouting();

      //app.UseAuthorization();

      app.UseMvc(opt => {
        opt.Select().Filter().Expand();
        opt.MapODataServiceRoute("odata", "odata", MakeEDMModel());
      });

      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllers();

      //});
    }

    private IEdmModel MakeEDMModel()
    {
      var odataBuilder = new ODataConventionModelBuilder();
      odataBuilder.EntitySet<Person>("People");

      return odataBuilder.GetEdmModel();
    }
  }
}
