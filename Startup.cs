using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persontec.Api.Data;
using Persontec.Api.Data.Entities;
using AutoMapper;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;

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

      services.AddScoped<IHrRepository, HrRepository>();

      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      
      //services.AddDbContext<MyContext>();
      services.AddDbContext<HrContext>();
      services.AddControllers(opt =>
      {
        opt.EnableEndpointRouting = false;
      })
        .AddNewtonsoftJson(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

      services.AddSwaggerGen(opt =>
      {
        opt.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
          Version = "v1.0",
          Title = "Hello World"
        });
      });
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
      app.UseSwagger();
      app.UseSwaggerUI(opt =>
      {
        opt.SwaggerEndpoint("/swagger/v1.0/swagger.json", "PeopleTec API");
      });

      //app.UseAuthorization();

      app.UseMvc(opt =>
      {
        opt.Select().Filter().OrderBy().Expand().Count();
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
      odataBuilder.EntitySet<Employee>("EmployeesOData");
      //var opt = odataBuilder.EntitySet<Employee>("Employees");

      return odataBuilder.GetEdmModel();
    }
  }
}
