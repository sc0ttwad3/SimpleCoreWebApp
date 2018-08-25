using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCoreWebApp
{
  public class Startup
  {
    // This OPTIONAL method gets called by the runtime. 
    // Use this method to add services to the container.
    // (For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940 )
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IComponent, ComponentB>();
    }

    // This REQUIRED method gets called by the runtime. 
    // Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IComponent component)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.Map("/foo", config => config.Use(async (context, next) => await context.Response.WriteAsync("FOO")));

      app.MapWhen(
        context =>  context.Request.Method == "POST" &&
                    context.Request.Path == "/bar",
        config  =>  config.Use(async (context, next) => await context.Response.WriteAsync("POST to BAR"))

      );

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync($"ROOT\n\nInjected component name: {component.Name}");
      });
    }
  }
}
