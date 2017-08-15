using MVCBlog.Core.Commands;
using MVCBlog.Core.Database;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Linq;
using System.Web.Http;
using MVCBlog.Api.Code;
using MVCBlog.Core.Service;

namespace MVCBlog.Api
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {

      // Create the container as usual.
      var container = new Container();
      container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

      var assemblies = new[] { typeof(ICommandHandler<>).Assembly };

      // Register your types, for instance using the scoped lifestyle:
      container.Register<IRepository, DatabaseContext>(Lifestyle.Scoped);
      container.RegisterSingleton<IMessageService, EmailMessageService>();

      var types = container.GetTypesToRegister(typeof(ICommandHandler<>),
        assemblies,
        new TypesToRegisterOptions { IncludeGenericTypeDefinitions = true })
        .ToList();

      container.Register(typeof(ICommandHandler<>), types.Where(t => !t.IsGenericTypeDefinition));

      foreach(var type in types.Where(t => t.IsGenericTypeDefinition))
      {
        container.Register(typeof(ICommandHandler<>), type);
      }

      // container.Register(typeof(ICommandHandler<>), assemblies);

      container.RegisterDecorator(
          typeof(ICommandHandler<>),
          typeof(CommandLoggingDecorator<>));

      // This is an extension method from the integration package.
      container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

      container.Verify();

      GlobalConfiguration.Configuration.DependencyResolver =
        new SimpleInjectorWebApiDependencyResolver(container);


      GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}
