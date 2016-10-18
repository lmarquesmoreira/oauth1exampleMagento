using BlackBoxModuleApi.Cache;
using BlackBoxModuleApi.Services;
using Magento.RestApi;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace BlackBoxModuleApi.App_Start
{
    public static class ContainerConfig
    {
        public static Container Create()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            /* Services */
            container.Register<ICacheManager, CacheManager>();
            container.Register<IOAuthService, OAuthService>();
            container.Register<IMagentoApi, MagentoApi>();
            container.Register<IMagentoService, MagentoService>();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            return container;
        }
    }
}