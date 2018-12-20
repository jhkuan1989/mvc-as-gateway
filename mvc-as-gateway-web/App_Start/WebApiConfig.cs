using Autofac;
using Autofac.Integration.Mvc;
using mvc_as_gateway_web.Api;
using mvc_as_gateway_web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace mvc_as_gateway_web
{
    public static class WebApiConfig
    {
        private static IContainer Container { get; set; }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            RegisterDependencies(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        #region MVCController Registration
        //Don't Move this one from Here
        private static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //services
            builder.RegisterType<ApiService>().As<IApiService>().InstancePerLifetimeScope();

            //Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerLifetimeScope();

            Container = builder.Build();

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
        #endregion
    }
}
