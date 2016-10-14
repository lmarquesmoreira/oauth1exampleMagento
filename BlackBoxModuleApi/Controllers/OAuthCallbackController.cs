using BlackBoxModuleApi.Models;
using BlackBoxModuleApi.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackBoxModuleApi.Controllers
{
    [RoutePrefix("api/oauth")]
    public class OAuthCallbackController : ApiController
    {

        protected readonly IOAuthService Service;

        public OAuthCallbackController(IOAuthService service)
        {
            Service = service;
        }

        [Route("Endpoint")]
        [HttpPost]
        public async Task<HttpResponseMessage> Endpoint() => await Service.Endpoint(Request.Content);


        [Route("Authorize")]
        [HttpGet]
        public HttpResponseMessage Authorize([FromUri] AuthorizeDataRequest data) => Service.Authorize(data);
       

        [Route("CheckLogin")]
        [HttpPost]
        public async Task<HttpResponseMessage> CheckLogin([FromUri]AuthorizeDataRequest data) => await Service.CheckLogin(data);

    }
}
