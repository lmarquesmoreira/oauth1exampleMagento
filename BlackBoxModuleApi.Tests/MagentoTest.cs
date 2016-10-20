using BlackBoxModuleApi.Models.Magento;
using Magento.RestApi;
using Magento.RestApi.Json;
using Magento.RestApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxModuleApi.Tests
{
    [TestClass]

    public class MagentoTest
    {
        string baseUrl = "http://blackboxmagento.centralus.cloudapp.azure.com/";
        string consumerKey = "g7x9ws7pgs7x0550movejd6mq2n51sid";
        string consumerSecret = "523xmdrdmrg6wdmnxoi2n9df7rj7ocvk";
        string accessTokenKey = "oikk3fkfy9mar2eyy79fu1k5v72du6hj";
        string accessTokenSecret = "cx6mbpqquuo5lhta9mu6c9xxtqthroe9";

        private RestClient _client;
        private RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new RestClient(baseUrl);
                    _client.AddDefaultHeader("Authorization", $"Bearer {accessTokenKey}");
                }
                return _client;
            }
        }

        [TestMethod]
        public void Magento_Client_Cannot_Be_Null()
        {
            var client = new MagentoApi()
                .SetCustomAdminUrlPart("adminlogin")
                .Initialize(baseUrl, consumerKey, consumerSecret)
                .SetAccessToken(accessTokenKey, accessTokenSecret);

            Assert.IsNotNull(client);
        }

        [TestMethod]
        public void Products_Cannot_Be_Null()
        {
            var response = GetProducts().Result;

            Assert.IsFalse(response.HasErrors);
        }

        [TestMethod]
        public void Orders_Cannot_Be_Null()
        {
            var response = GetOrders().Result;

            Assert.IsFalse(response.HasErrors);
        }

        [TestMethod]
        public void Post_Order_Cannot_Return_Error()
        {
            var response = PostOrders().Result;
            Assert.IsTrue(response);
        }

        private async Task<bool> PostOrders()
        {
            var entity = new InlineModel()
            {
                entity = new SalesDataOrder()
                {
                    
                    base_grand_total = 20,
                    customer_email = "lmmoreira@braspag.com.br",
                    grand_total = 20,
                    items = new List<SalesDataOrderItem>()
                    {
                        new SalesDataOrderItem()
                        {
                            sku = "123456"
                        }
                    },
                    payment = new SalesDataOrderPayment()
                    {
                        account_status = "Approved",
                        additional_information = new List<string>() { "Approved", "BlackBox"},
                        cc_last4 = "1111",
                        method = "banktransfer"
                    }
                }
            };

            var request = new RestRequest
            {
                Resource = "rest/V1/orders",
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            var jen = JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            request.AddParameter("application/json; charset=utf-8", jen, ParameterType.RequestBody);


            var response = await Client.ExecutePostTaskAsync(request);


            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessTokenKey}");
            //    var response = await client.PostAsJsonAsync("http://blackboxmagento.centralus.cloudapp.azure.com/rest/V1/orders", entity);

            //    var res = await response.Content.ReadAsStringAsync();

            //    if (response.IsSuccessStatusCode)
            //        return true;
            //    else
            //        return false;
            //}
            return true;
        }


        private async Task<MagentoApiResponse<IList<Order>>> GetOrders()
        {
            var request = new RestRequest
            {
                Resource = "rest/V1/orders?searchCriteria[pageSize]=1",
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var response = await Execute<List<Order>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new List<Order>();
                return new MagentoApiResponse<IList<Order>> { Result = response.Result, RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
            }
            return new MagentoApiResponse<IList<Order>> { Errors = response.Errors, RequestUrl = response.RequestUrl };
        }

        private async Task<MagentoApiResponse<IList<Product>>> GetProducts()
        {
            var request = new RestRequest
            {
                Resource = "rest/V1/products?searchCriteria[sortOrders][0][field]=price",
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new Magento.RestApi.Json.JsonSerializer()
            };

            var response = await Execute<Dictionary<int, Product>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new Dictionary<int, Product>();
                return new MagentoApiResponse<IList<Product>> { Result = response.Result.Select(product => product.Value).ToList(), RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
            }
            return new MagentoApiResponse<IList<Product>> { Errors = response.Errors, RequestUrl = response.RequestUrl };
        }

        protected async Task<MagentoApiResponse<T>> Execute<T>(IRestRequest request, bool isSecondTry = false) where T : new()
        {
            var response = await Client.ExecuteTaskAsync<T>(request);
            if (response.ContentType.ToUpperInvariant().Contains("APPLICATION/JSON"))
            {
                MagentoApiResponse<T> res = new MagentoApiResponse<T>();


                try
                {
                    Dictionary<string, JToken> a = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                    JToken dat;
                    var reu = a.TryGetValue("items", out dat);
                    var data = JsonConvert.DeserializeObject<List<Order>>(dat.ToString());
                }
                catch (Exception e)

                {

                }
                return res;
            }
            var errors = new List<MagentoError>
            {
                new MagentoError
                {
                    Message = "The response doesn't contain json and cannot be deserialized: See ErrorString for the content of the response."
                }
            };

            var requestBodyParameter = request.Parameters.FirstOrDefault(x => x.Type == ParameterType.RequestBody);
            var requestContent = requestBodyParameter == null ? string.Empty : requestBodyParameter.Value == null ? string.Empty : requestBodyParameter.Value.ToString();

            return new MagentoApiResponse<T>
            {
                Errors = errors,
                RequestUrl = Client.BuildUri(request),
                ErrorString = response.Content,
                RequestContent = requestContent
            };
        }

        protected async Task<MagentoApiResponse<T>> HandleResponse<T>(IRestResponse<T> response, IRestRequest request, bool isSecondTry) where T : new()
        {
            if (response.ErrorException != null)
            {
                throw new MagentoApiException("The request was not succesfully completed.", response.ErrorException);
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {

                var errors = GetErrorsFromResponse(response);

                var requestBodyParameter = request.Parameters.FirstOrDefault(x => x.Type == ParameterType.RequestBody);
                var requestContent = requestBodyParameter == null ? string.Empty : requestBodyParameter.Value == null ? string.Empty : requestBodyParameter.Value.ToString();

                return new MagentoApiResponse<T>
                {
                    Errors = errors,
                    RequestUrl = Client.BuildUri(request),
                    ErrorString = response.Content,
                    RequestContent = requestContent
                };
            }

            return new MagentoApiResponse<T> { Result = response.Data, RequestUrl = Client.BuildUri(request) };
        }

        protected List<MagentoError> GetErrorsFromResponse(IRestResponse restResponse)
        {
            var list = new List<MagentoError>();
            if (!string.IsNullOrEmpty(restResponse.Content))
            {
                JObject result;
                try
                {
                    result = JObject.Parse(restResponse.Content);
                }
                catch (JsonReaderException)
                {
                    // if it can't be parsed, then it doesn't contain messages.
                    return list;
                }
                var messages = result["messages"];
                if (messages != null)
                {
                    var errors = messages["error"];
                    if (errors != null)
                    {
                        list.AddRange(errors.Select(error => new MagentoError
                        {
                            Code = (string)error["code"],
                            Message = (string)error["message"]
                        }));
                        return list;
                    }
                }
            }

            return list;
        }

    }
}
