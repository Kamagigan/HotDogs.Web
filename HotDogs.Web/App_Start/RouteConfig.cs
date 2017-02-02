using System.Web.Mvc;
using System.Web.Routing;

namespace HotDogs.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

