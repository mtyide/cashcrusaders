using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CashCrusadersPOModule
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"^[0-9]+$" });
            configuration.Routes.MapHttpRoute("ApiWithActionAndName", "api/{controller}/{action}/{name}", null, new { id = @"^[a-z]+$" });
            configuration.Routes.MapHttpRoute("ApiWithAction", "api/{controller}/{action}", null, null);
        }
    }
}
