using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SimpleCoreWebApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build()
                                .Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               // overrides any inline defined config sections
               // on the web-host builder
               .UseStartup<Startup>();
  }


  /*
   *  Section is for illustrating Dependency Injection features
   *  built into ASP.NET Core.
   */
  public interface IComponent
  {
    string Name { get; }
  }


  public class ComponentA
  {
    private readonly IComponent _componentB;

    public ComponentA(IComponent componentB)
    {
      this._componentB = componentB;
    }
  }


  public class ComponentB: IComponent
  {
    public string Name { get; set; } = nameof(ComponentB);
  }
}
