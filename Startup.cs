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
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.IO;
using Persontec.Api.ViewModels;
using Persontec.Api.Attributes;

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
      services.AddSwaggerGen(opt =>
      {
        opt.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
          Version = "v1.0",
          Title = "Hello World"
        });
        //var filePath = Path.Combine(AppContext.BaseDirectory, "Persontec.Api.xml");
        //opt.IncludeXmlComments(filePath);
      });
      services.AddOData();
      services.AddControllers(opt =>
      {
        opt.EnableEndpointRouting = false;
        // Workaround: https://github.com/OData/WebApi/issues/1177
        foreach (var outputFormatter in opt.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
          outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
        foreach (var inputFormatter in opt.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
          inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }

        //opt.Filters.Add(new CustomAuthorizationFilter());
      })
        .AddNewtonsoftJson(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

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
        opt.Select().Filter().OrderBy().Expand().Count().MaxTop(25);
        opt.MapODataServiceRoute("odata", "odata", MakeEDMModel());
        opt.EnableDependencyInjection();
      });

      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllers();

      //});
    }

    private IEdmModel MakeEDMModel()
    {
      var bldr = new ODataConventionModelBuilder();
      bldr.EntitySet<Employee>("EmployeesOData");
      bldr.EntitySet<Transfer>("TransfersOData");

      //var func = bldr.Function("GetAllReports");
      var func = bldr.EntityType<Employee>().Collection.Function("GetAllReports");
      func.Parameter<int>("EmployeeNumber");
      func.ReturnsCollectionFromEntitySet<Employee>("EmployeesOData");


      return bldr.GetEdmModel();
    }
  }
}
