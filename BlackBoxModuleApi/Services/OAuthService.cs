using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlackBoxModuleApi.Models;
using System.Diagnostics;
using System.Net;
using BlackBoxModuleApi.Cache;
using System.Text;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;

namespace BlackBoxModuleApi.Services
{
    public class OAuthService : IOAuthService
    {

        protected readonly ICacheManager Cache;

        public string CallbackUrl { get { return $"{ConfigurationManager.AppSettings["ApiUrl"]}/api/oauth/checklogin"; } }

        public OAuthService(ICacheManager cache)
        {
            Cache = cache;
        }

        public HttpResponseMessage Authorize(AuthorizeDataRequest data)
        {
            var response = new HttpResponseMessage();

            //alterei callback_url para success_call_back @:

            StringBuilder html = new StringBuilder();
            html.Append("<table width=\"300\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#CCCCCC\"><tr>");
            html.Append($"<form name=\"form1\" method=\"post\" action=\"{CallbackUrl}?oauth_consumer_key={data.oauth_consumer_key}&success_call_back={data.success_call_back}\">");
            html.Append("<td><table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" bgcolor=\"#FFFFFF\"><tr><td colspan=\"3\"><strong>BlackBox Login</strong></td></tr><tr><td width=\"78\">Username</td><td width=\"6\">:</td><td width=\"294\"><input name=\"myusername\" type=\"text\" id=\"myusername\"></td></tr><tr><td>Senha</td><td>:</td><td><input name=\"mypassword\" type=\"text\" id=\"mypassword\"></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><input type=\"submit\" name=\"Submit\" value=\"Login\"></td></tr></table></td></form></tr></table>");

            response.Content = new StringContent($"<html><body>{html.ToString()}</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public HttpResponseMessage CheckLogin(AuthorizeDataRequest data)
        {

            //suponha que os dados do login estão corretos...
            try
            {
                Trace.TraceInformation($"[STARTING_TOKENS]");


                Trace.TraceInformation($"[STARTING_GET_REQUEST_TOKEN]");
                var requestToken = GetRequestToken();
                if (requestToken != null)
                {
                    Trace.TraceInformation($"[REQUEST_TOKEN] => token: {requestToken.oauth_token} - secret: {requestToken.oauth_token_secret}");

                    var accessToken = GetAccessToken(requestToken);

                    if (accessToken != null)
                    {
                        Trace.TraceInformation($"[ACCESS_TOKEN] => token: {requestToken.oauth_token} - secret: {requestToken.oauth_token_secret}");

                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
            catch (Exception e)
            {
                Trace.TraceError($"[RequestToken] => {e.Message}");
                Trace.TraceError($"[RequestToken] => {e.StackTrace}");
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        public async Task<HttpResponseMessage> Endpoint(HttpContent content)
        {
            try
            {
                var data = await content.ReadAsFormDataAsync();
                var model = new EndpointDataRequest()
                {
                    oauth_consumer_key = data.Get("oauth_consumer_key"),
                    oauth_consumer_key_secret = data.Get("oauth_consumer_secret"),
                    oauth_verifier = data.Get("oauth_verifier"),
                    store_base_url = data.Get("store_base_url")
                };

                try
                {

                    var rTrace = "[Endpoint_RESPONSE] => ";
                    foreach (string key in data)
                        rTrace += $" {key} : {data[key]} ";
                    Trace.TraceInformation(rTrace);

                    var token = data.Get("oauth_token");
                    if (!string.IsNullOrEmpty(token))
                        Trace.TraceInformation($"[OAUTH_TOKEN] => {token}");
                    else
                        Trace.TraceError($"[OAUTH_TOKEN] => não existe token");
                }
                catch (Exception e)
                {
                    Trace.TraceError($"[OAUTH_TOKEN] => não existe token - {e.Message}");
                }

                Trace.TraceInformation($"Body Data : {model.oauth_consumer_key} \n,  {model.oauth_consumer_key_secret} \n,  {model.oauth_verifier} \n,  {model.store_base_url}");

                Cache.Set("OAuthConsumerKey", model);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private Token GetRequestToken()
        {
            var model = Cache.Get<EndpointDataRequest>("OAuthConsumerKey");

            var client = new RestClient("http://blackboxmagento.centralus.cloudapp.azure.com")
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(model.oauth_consumer_key, model.oauth_consumer_key_secret)
            };

            var request = new RestRequest(method: Method.POST, resource: "/oauth/token/request/");
            try
            {
                var response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Error)
                    Trace.TraceError($"[REST_REQUEST_TOKEN_ERROR] => {response.ErrorMessage}");
                else
                {
                    var token = Generate(response.Content);

                    Cache.Set("OAuthToken", token);
                    Trace.TraceInformation($"[REST_REQUEST_TOKEN_DATA] => {response.Content}");
                    return token;
                }

            }
            catch (Exception e)
            {
                Trace.TraceError($"[REST_REQUEST_TOKEN_ERROR] => {e.Message}");
            }

            return null;
        }

        private Token GetAccessToken(Token requestToken)
        {
            var model = Cache.Get<EndpointDataRequest>("OAuthConsumerKey");

            var client = new RestClient("http://blackboxmagento.centralus.cloudapp.azure.com")
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(model.oauth_consumer_key, model.oauth_consumer_key_secret, requestToken.oauth_token, requestToken.oauth_token_secret, model.oauth_verifier)
            };

            var request = new RestRequest(method: Method.POST, resource: "/oauth/token/access/");

            try
            {
                var response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Error)
                    Trace.TraceError($"[ACCESS_TOKEN_ERROR] => {response.ErrorMessage}");
                else
                {
                    Trace.TraceInformation($"[REST_ACCESS_TOKEN_DATA] => {response.Content}");
                    var token = Generate(response.Content);
                    Cache.Set("OAuthAccessToken", token);
                    return token;
                }
            }
            catch (Exception e)
            {
                Trace.TraceError($"[REST_ACCESS_TOKEN_ERROR] => {e.Message}");
            }
            return null;
        }

        public static Token Generate(string queryString)
        {
            var token = new Token();

            var s = queryString.Split('&');

            var otoken = s[0].Split('=');
            var osecret = s[1].Split('=');

            token.oauth_token = otoken[1];
            token.oauth_token_secret = osecret[1];

            return token;
        }
    }
}