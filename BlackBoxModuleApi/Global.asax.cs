using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.ApplicationInsights;
using SimpleInjector.Integration.WebApi;
using BlackBoxModuleApi.App_Start;

namespace BlackBoxModuleApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly TelemetryClient _logger = new TelemetryClient();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = ContainerConfig.Create();
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (_logger == null) return;

            try
            {
                var ex = Server.GetLastError();

                if (ex != null)
                {
                    _logger.TrackException(ex);
                }

                Server.ClearError();
            }
            catch (Exception ex)
            {
                _logger.TrackException(ex);
            }
        }
    }
}
