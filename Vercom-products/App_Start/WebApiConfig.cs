using System.Web.Http;

namespace Vercom_products
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/products",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
