using Magento.RestApi;
using Magento.RestApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlackBoxModuleApi.Services
{
    public class MagentoService : IMagentoService
    {
        private IMagentoApi MagentoApi;

        private readonly string BaseUrl = "http://blackboxmagento.centralus.cloudapp.azure.com/";
        private readonly string ConsumerKey = "0qx7c7y46yvq574mgna41alwdcl6jefq";
        private readonly string ConsumerSecret = "3ywthdi95mfas652vj3nd93d5d4ira4g";
        private readonly string User = "adminuser";
        private readonly string UserSecret = "P@ssw0rdLMM01";


        public MagentoService(IMagentoApi api)
        {
            MagentoApi = api;
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                MagentoApi
                   .SetCustomAdminUrlPart("adminlogin")
                   .Initialize(BaseUrl, ConsumerKey, ConsumerSecret)
                   .AuthenticateAdmin(User, UserSecret);

            }
            catch (Exception e)
            {

            }

        }

        public async Task<IList<Product>> GetProducts()
        {
            var response = await MagentoApi.GetProducts(filter: new Magento.RestApi.Models.Filter()
            {
                SortDirection = Magento.RestApi.Models.SortDirection.asc
            });

            if (!response.HasErrors)
                return response.Result;
            return null;
        }
    }
}