using System.Web.Http;
using System.Web.Http.Dispatcher;
using TDD.Api;

public class BootStrap
{
    public BootStrap()
    {
    }

    public void Configure(HttpConfiguration config)
    {
        config.Routes.MapHttpRoute(
            name: "ApiDefault",
            routeTemplate: "{controller}/{id}",
            defaults: new 
            {
                //Por convención se omite la palabra Controller. Con ella no
                //funciona.
                controller = "Authentication",
                id = RouteParameter.Optional
            }
            );

        config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());
    }
}