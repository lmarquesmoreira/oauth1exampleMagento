using BlackBoxModuleApi.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlackBoxModuleApi.Services
{
    public interface IOAuthService
    {
        string CallbackUrl { get; }

        Task<HttpResponseMessage> Endpoint(HttpContent data);

        HttpResponseMessage Authorize(AuthorizeDataRequest data);

        HttpResponseMessage CheckLogin(AuthorizeDataRequest data);
    }
}
