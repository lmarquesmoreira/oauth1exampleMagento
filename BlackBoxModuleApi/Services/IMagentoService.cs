using Magento.RestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackBoxModuleApi.Services
{
    public interface IMagentoService
    {
        Task<IList<Product>> GetProducts();
    }
}
