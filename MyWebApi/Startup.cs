using System.Web.Http;
using MyWebApi.Service;
using Owin;

namespace MyWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // 依赖注入（简化：使用简单的自定义 Resolver）
            config.DependencyResolver = new SimpleResolver();

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}


