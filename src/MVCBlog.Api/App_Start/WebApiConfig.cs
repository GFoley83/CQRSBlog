using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MVCBlog.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

#if DEBUG
      UseIndentJsonSerialization(config);
#endif

      UseCamelCaseJsonSerialization(config);

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }

    private static void UseCamelCaseJsonSerialization(HttpConfiguration config)
    {
      config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
        new CamelCasePropertyNamesContractResolver();
    }

    private static void UseIndentJsonSerialization(HttpConfiguration config)
    {
      config.Formatters.JsonFormatter.Indent = true;
    }
  }
}
