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

        private async Task<MagentoApiResponse<IList<Order>>> GetOrders()
        {
            var request = new RestRequest
            {
                Resource = "rest/V1/orders?searchCriteria[pageSize]=1",
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new Magento.RestApi.Json.JsonSerializer()
            };

            var response = await Execute<Dictionary<int, Order>>(request);
            if (!response.HasErrors)
            {
                if (response.Result == null) response.Result = new Dictionary<int, Order>();
                return new MagentoApiResponse<IList<Order>> { Result = response.Result.Select(order => order.Value).ToList(), RequestUrl = response.RequestUrl, ErrorString = response.ErrorString };
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
            Client.FollowRedirects = request.Method != Method.POST;
            var response = await Client.ExecuteTaskAsync<T>(request);
            if (response.ContentType.ToUpperInvariant().Contains("APPLICATION/JSON"))
            {
                return await HandleResponse(response, request, isSecondTry);
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


    public class Rate
    {
        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("percent")]
        public int percent { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }
    public class AppliedTax
    {
        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("percent")]
        public int percent { get; set; }

        [JsonProperty("amount")]
        public int amount { get; set; }

        [JsonProperty("base_amount")]
        public int base_amount { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }
    public class ItemAppliedTax
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("item_id")]
        public int item_id { get; set; }

        [JsonProperty("associated_item_id")]
        public int associated_item_id { get; set; }

        [JsonProperty("applied_taxes")]
        public List<AppliedTax> applied_taxes { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class VaultPaymentToken
    {
        [JsonProperty("entity_id")]
        public int entity_id { get; set; }
        [JsonProperty("customer_id")]
        public int customer_id { get; set; }
        [JsonProperty("public_hash")]
        public string public_hash { get; set; }
        [JsonProperty("payment_method_code")]
        public string payment_method_code { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("created_at")]
        public string created_at { get; set; }
        [JsonProperty("expires_at")]
        public string expires_at { get; set; }
        [JsonProperty("gateway_token")]
        public string gateway_token { get; set; }
        [JsonProperty("token_details")]
        public string token_details { get; set; }
        [JsonProperty("is_active")]
        public bool is_active { get; set; }
        [JsonProperty("is_visible")]
        public bool is_visible { get; set; }
    }

    public class FileInfo
    {

        [JsonProperty("base64_encoded_data")]
        public string base64_encoded_data { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class CustomOption
    {

        [JsonProperty("option_id")]
        public string option_id { get; set; }

        [JsonProperty("option_value")]
        public string option_value { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class BundleOption
    {

        [JsonProperty("option_id")]
        public int option_id { get; set; }

        [JsonProperty("option_qty")]
        public int option_qty { get; set; }

        [JsonProperty("option_selections")]
        public IList<int> option_selections { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class DownloadableOption
    {

        [JsonProperty("downloadable_links")]
        public IList<int> downloadable_links { get; set; }
    }

    public class ConfigurableItemOption
    {

        [JsonProperty("option_id")]
        public string option_id { get; set; }

        [JsonProperty("option_value")]
        public int option_value { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class GiftMessage
    {
        [JsonProperty("gift_message_id")]
        public int gift_message_id { get; set; }
        [JsonProperty("customer_id")]
        public int customer_id { get; set; }
        [JsonProperty("sender")]
        public string sender { get; set; }
        [JsonProperty("recipient")]
        public string recipient { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Address
    {
        [JsonProperty("address_type")]
        public string address_type { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("company")]
        public string company { get; set; }

        [JsonProperty("country_id")]
        public string country_id { get; set; }

        [JsonProperty("customer_address_id")]
        public int customer_address_id { get; set; }

        [JsonProperty("customer_id")]
        public int customer_id { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("entity_id")]
        public int entity_id { get; set; }

        [JsonProperty("fax")]
        public string fax { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("middlename")]
        public string middlename { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("postcode")]
        public string postcode { get; set; }

        [JsonProperty("prefix")]
        public string prefix { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }

        [JsonProperty("region_code")]
        public string region_code { get; set; }

        [JsonProperty("region_id")]
        public int region_id { get; set; }

        [JsonProperty("street")]
        public List<string> street { get; set; }

        [JsonProperty("suffix")]
        public string suffix { get; set; }

        [JsonProperty("telephone")]
        public string telephone { get; set; }

        [JsonProperty("vat_id")]
        public string vat_id { get; set; }

        [JsonProperty("vat_is_valid")]
        public int vat_is_valid { get; set; }

        [JsonProperty("vat_request_date")]
        public string vat_request_date { get; set; }

        [JsonProperty("vat_request_id")]
        public string vat_request_id { get; set; }

        [JsonProperty("vat_request_success")]
        public int vat_request_success { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Total
    {
        [JsonProperty("base_shipping_amount")]
        public int base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_canceled")]
        public int base_shipping_canceled { get; set; }

        [JsonProperty("base_shipping_discount_amount")]
        public int base_shipping_discount_amount { get; set; }

        [JsonProperty("base_shipping_discount_tax_compensation_amnt")]
        public int base_shipping_discount_tax_compensation_amnt { get; set; }

        [JsonProperty("base_shipping_incl_tax")]
        public int base_shipping_incl_tax { get; set; }

        [JsonProperty("base_shipping_invoiced")]
        public int base_shipping_invoiced { get; set; }

        [JsonProperty("base_shipping_refunded")]
        public int base_shipping_refunded { get; set; }

        [JsonProperty("base_shipping_tax_amount")]
        public int base_shipping_tax_amount { get; set; }

        [JsonProperty("base_shipping_tax_refunded")]
        public int base_shipping_tax_refunded { get; set; }

        [JsonProperty("shipping_amount")]
        public int shipping_amount { get; set; }

        [JsonProperty("shipping_canceled")]
        public int shipping_canceled { get; set; }

        [JsonProperty("shipping_discount_amount")]
        public int shipping_discount_amount { get; set; }

        [JsonProperty("shipping_discount_tax_compensation_amount")]
        public int shipping_discount_tax_compensation_amount { get; set; }

        [JsonProperty("shipping_incl_tax")]
        public int shipping_incl_tax { get; set; }

        [JsonProperty("shipping_invoiced")]
        public int shipping_invoiced { get; set; }

        [JsonProperty("shipping_refunded")]
        public int shipping_refunded { get; set; }

        [JsonProperty("shipping_tax_amount")]
        public int shipping_tax_amount { get; set; }

        [JsonProperty("shipping_tax_refunded")]
        public int shipping_tax_refunded { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Shipping
    {
        [JsonProperty("address")]
        public Address address { get; set; }
        [JsonProperty("method")]
        public string method { get; set; }
        [JsonProperty("total")]
        public Total total { get; set; }
        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class ShippingAssignment
    {
        [JsonProperty("shipping")]
        public Shipping shipping { get; set; }

        [JsonProperty("items")]
        public List<Item> items { get; set; }

        [JsonProperty("stock_id")]
        public int stock_id { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class ExtensionAttributes
    {
        [JsonProperty("converting_from_quote")]
        public bool converting_from_quote { get; set; }

        [JsonProperty("applied_taxes")]
        public List<AppliedTax> applied_taxes { get; set; }

        [JsonProperty("item_applied_taxes")]
        public List<ItemAppliedTax> item_applied_taxes { get; set; }

        [JsonProperty("rates")]
        public List<Rate> rates { get; set; }

        [JsonProperty("shipping_assignments")]
        public List<ShippingAssignment> shipping_assignments { get; set; }

        [JsonProperty("file_info")]
        public FileInfo file_info { get; set; }

        [JsonProperty("gift_message")]
        public GiftMessage gift_message { get; set; }

        [JsonProperty("vault_payment_token")]
        public VaultPaymentToken vault_payment_token { get; set; }

        [JsonProperty("entity_id")]
        public string entity_id { get; set; }

        [JsonProperty("entity_type")]
        public string entity_type { get; set; }

        [JsonProperty("custom_options")]
        public IList<CustomOption> custom_options { get; set; }

        [JsonProperty("bundle_options")]
        public IList<BundleOption> bundle_options { get; set; }

        [JsonProperty("downloadable_option")]
        public DownloadableOption downloadable_option { get; set; }

        [JsonProperty("configurable_item_options")]
        public IList<ConfigurableItemOption> configurable_item_options { get; set; }
    }

    public class ProductOption
    {

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Item
    {

        [JsonProperty("additional_data")]
        public string additional_data { get; set; }

        [JsonProperty("amount_refunded")]
        public int amount_refunded { get; set; }

        [JsonProperty("applied_rule_ids")]
        public string applied_rule_ids { get; set; }

        [JsonProperty("base_amount_refunded")]
        public int base_amount_refunded { get; set; }

        [JsonProperty("base_cost")]
        public int base_cost { get; set; }

        [JsonProperty("base_discount_amount")]
        public int base_discount_amount { get; set; }

        [JsonProperty("base_discount_invoiced")]
        public int base_discount_invoiced { get; set; }

        [JsonProperty("base_discount_refunded")]
        public int base_discount_refunded { get; set; }

        [JsonProperty("base_discount_tax_compensation_amount")]
        public int base_discount_tax_compensation_amount { get; set; }

        [JsonProperty("base_discount_tax_compensation_invoiced")]
        public int base_discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("base_discount_tax_compensation_refunded")]
        public int base_discount_tax_compensation_refunded { get; set; }

        [JsonProperty("base_original_price")]
        public int base_original_price { get; set; }

        [JsonProperty("base_price")]
        public int base_price { get; set; }

        [JsonProperty("base_price_incl_tax")]
        public int base_price_incl_tax { get; set; }

        [JsonProperty("base_row_invoiced")]
        public int base_row_invoiced { get; set; }

        [JsonProperty("base_row_total")]
        public int base_row_total { get; set; }

        [JsonProperty("base_row_total_incl_tax")]
        public int base_row_total_incl_tax { get; set; }

        [JsonProperty("base_tax_amount")]
        public int base_tax_amount { get; set; }

        [JsonProperty("base_tax_before_discount")]
        public int base_tax_before_discount { get; set; }

        [JsonProperty("base_tax_invoiced")]
        public int base_tax_invoiced { get; set; }

        [JsonProperty("base_tax_refunded")]
        public int base_tax_refunded { get; set; }

        [JsonProperty("base_weee_tax_applied_amount")]
        public int base_weee_tax_applied_amount { get; set; }

        [JsonProperty("base_weee_tax_applied_row_amnt")]
        public int base_weee_tax_applied_row_amnt { get; set; }

        [JsonProperty("base_weee_tax_disposition")]
        public int base_weee_tax_disposition { get; set; }

        [JsonProperty("base_weee_tax_row_disposition")]
        public int base_weee_tax_row_disposition { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("discount_amount")]
        public int discount_amount { get; set; }

        [JsonProperty("discount_invoiced")]
        public int discount_invoiced { get; set; }

        [JsonProperty("discount_percent")]
        public int discount_percent { get; set; }

        [JsonProperty("discount_refunded")]
        public int discount_refunded { get; set; }

        [JsonProperty("event_id")]
        public int event_id { get; set; }

        [JsonProperty("ext_order_item_id")]
        public string ext_order_item_id { get; set; }

        [JsonProperty("free_shipping")]
        public int free_shipping { get; set; }

        [JsonProperty("gw_base_price")]
        public int gw_base_price { get; set; }

        [JsonProperty("gw_base_price_invoiced")]
        public int gw_base_price_invoiced { get; set; }

        [JsonProperty("gw_base_price_refunded")]
        public int gw_base_price_refunded { get; set; }

        [JsonProperty("gw_base_tax_amount")]
        public int gw_base_tax_amount { get; set; }

        [JsonProperty("gw_base_tax_amount_invoiced")]
        public int gw_base_tax_amount_invoiced { get; set; }

        [JsonProperty("gw_base_tax_amount_refunded")]
        public int gw_base_tax_amount_refunded { get; set; }

        [JsonProperty("gw_id")]
        public int gw_id { get; set; }

        [JsonProperty("gw_price")]
        public int gw_price { get; set; }

        [JsonProperty("gw_price_invoiced")]
        public int gw_price_invoiced { get; set; }

        [JsonProperty("gw_price_refunded")]
        public int gw_price_refunded { get; set; }

        [JsonProperty("gw_tax_amount")]
        public int gw_tax_amount { get; set; }

        [JsonProperty("gw_tax_amount_invoiced")]
        public int gw_tax_amount_invoiced { get; set; }

        [JsonProperty("gw_tax_amount_refunded")]
        public int gw_tax_amount_refunded { get; set; }

        [JsonProperty("discount_tax_compensation_amount")]
        public int discount_tax_compensation_amount { get; set; }

        [JsonProperty("discount_tax_compensation_canceled")]
        public int discount_tax_compensation_canceled { get; set; }

        [JsonProperty("discount_tax_compensation_invoiced")]
        public int discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("discount_tax_compensation_refunded")]
        public int discount_tax_compensation_refunded { get; set; }

        [JsonProperty("is_qty_decimal")]
        public int is_qty_decimal { get; set; }

        [JsonProperty("is_virtual")]
        public int is_virtual { get; set; }

        [JsonProperty("item_id")]
        public int item_id { get; set; }

        [JsonProperty("locked_do_invoice")]
        public int locked_do_invoice { get; set; }

        [JsonProperty("locked_do_ship")]
        public int locked_do_ship { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("no_discount")]
        public int no_discount { get; set; }

        [JsonProperty("order_id")]
        public int order_id { get; set; }

        [JsonProperty("original_price")]
        public int original_price { get; set; }

        [JsonProperty("parent_item_id")]
        public int parent_item_id { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

        [JsonProperty("price_incl_tax")]
        public int price_incl_tax { get; set; }

        [JsonProperty("product_id")]
        public int product_id { get; set; }

        [JsonProperty("product_type")]
        public string product_type { get; set; }

        [JsonProperty("qty_backordered")]
        public int qty_backordered { get; set; }

        [JsonProperty("qty_canceled")]
        public int qty_canceled { get; set; }

        [JsonProperty("qty_invoiced")]
        public int qty_invoiced { get; set; }

        [JsonProperty("qty_ordered")]
        public int qty_ordered { get; set; }

        [JsonProperty("qty_refunded")]
        public int qty_refunded { get; set; }

        [JsonProperty("qty_returned")]
        public int qty_returned { get; set; }

        [JsonProperty("qty_shipped")]
        public int qty_shipped { get; set; }

        [JsonProperty("quote_item_id")]
        public int quote_item_id { get; set; }

        [JsonProperty("row_invoiced")]
        public int row_invoiced { get; set; }

        [JsonProperty("row_total")]
        public int row_total { get; set; }

        [JsonProperty("row_total_incl_tax")]
        public int row_total_incl_tax { get; set; }

        [JsonProperty("row_weight")]
        public int row_weight { get; set; }

        [JsonProperty("sku")]
        public string sku { get; set; }

        [JsonProperty("store_id")]
        public int store_id { get; set; }

        [JsonProperty("tax_amount")]
        public int tax_amount { get; set; }

        [JsonProperty("tax_before_discount")]
        public int tax_before_discount { get; set; }

        [JsonProperty("tax_canceled")]
        public int tax_canceled { get; set; }

        [JsonProperty("tax_invoiced")]
        public int tax_invoiced { get; set; }

        [JsonProperty("tax_percent")]
        public int tax_percent { get; set; }

        [JsonProperty("tax_refunded")]
        public int tax_refunded { get; set; }

        [JsonProperty("updated_at")]
        public string updated_at { get; set; }

        [JsonProperty("weee_tax_applied")]
        public string weee_tax_applied { get; set; }

        [JsonProperty("weee_tax_applied_amount")]
        public int weee_tax_applied_amount { get; set; }

        [JsonProperty("weee_tax_applied_row_amount")]
        public int weee_tax_applied_row_amount { get; set; }

        [JsonProperty("weee_tax_disposition")]
        public int weee_tax_disposition { get; set; }

        [JsonProperty("weee_tax_row_disposition")]
        public int weee_tax_row_disposition { get; set; }

        [JsonProperty("weight")]
        public int weight { get; set; }

        [JsonProperty("parent_item")]
        public ParentItem parent_item { get; set; }

        [JsonProperty("product_option")]
        public ProductOption product_option { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }
    public class ParentItem
    {

    }

    public class BillingAddress
    {

        [JsonProperty("address_type")]
        public string address_type { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("company")]
        public string company { get; set; }

        [JsonProperty("country_id")]
        public string country_id { get; set; }

        [JsonProperty("customer_address_id")]
        public int customer_address_id { get; set; }

        [JsonProperty("customer_id")]
        public int customer_id { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("entity_id")]
        public int entity_id { get; set; }

        [JsonProperty("fax")]
        public string fax { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("middlename")]
        public string middlename { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("postcode")]
        public string postcode { get; set; }

        [JsonProperty("prefix")]
        public string prefix { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }

        [JsonProperty("region_code")]
        public string region_code { get; set; }

        [JsonProperty("region_id")]
        public int region_id { get; set; }

        [JsonProperty("street")]
        public IList<string> street { get; set; }

        [JsonProperty("suffix")]
        public string suffix { get; set; }

        [JsonProperty("telephone")]
        public string telephone { get; set; }

        [JsonProperty("vat_id")]
        public string vat_id { get; set; }

        [JsonProperty("vat_is_valid")]
        public int vat_is_valid { get; set; }

        [JsonProperty("vat_request_date")]
        public string vat_request_date { get; set; }

        [JsonProperty("vat_request_id")]
        public string vat_request_id { get; set; }

        [JsonProperty("vat_request_success")]
        public int vat_request_success { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Payment
    {

        [JsonProperty("account_status")]
        public string account_status { get; set; }

        [JsonProperty("additional_data")]
        public string additional_data { get; set; }

        [JsonProperty("additional_information")]
        public IList<string> additional_information { get; set; }

        [JsonProperty("address_status")]
        public string address_status { get; set; }

        [JsonProperty("amount_authorized")]
        public int amount_authorized { get; set; }

        [JsonProperty("amount_canceled")]
        public int amount_canceled { get; set; }

        [JsonProperty("amount_ordered")]
        public int amount_ordered { get; set; }

        [JsonProperty("amount_paid")]
        public int amount_paid { get; set; }

        [JsonProperty("amount_refunded")]
        public int amount_refunded { get; set; }

        [JsonProperty("anet_trans_method")]
        public string anet_trans_method { get; set; }

        [JsonProperty("base_amount_authorized")]
        public int base_amount_authorized { get; set; }

        [JsonProperty("base_amount_canceled")]
        public int base_amount_canceled { get; set; }

        [JsonProperty("base_amount_ordered")]
        public int base_amount_ordered { get; set; }

        [JsonProperty("base_amount_paid")]
        public int base_amount_paid { get; set; }

        [JsonProperty("base_amount_paid_online")]
        public int base_amount_paid_online { get; set; }

        [JsonProperty("base_amount_refunded")]
        public int base_amount_refunded { get; set; }

        [JsonProperty("base_amount_refunded_online")]
        public int base_amount_refunded_online { get; set; }

        [JsonProperty("base_shipping_amount")]
        public int base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_captured")]
        public int base_shipping_captured { get; set; }

        [JsonProperty("base_shipping_refunded")]
        public int base_shipping_refunded { get; set; }

        [JsonProperty("cc_approval")]
        public string cc_approval { get; set; }

        [JsonProperty("cc_avs_status")]
        public string cc_avs_status { get; set; }

        [JsonProperty("cc_cid_status")]
        public string cc_cid_status { get; set; }

        [JsonProperty("cc_debug_request_body")]
        public string cc_debug_request_body { get; set; }

        [JsonProperty("cc_debug_response_body")]
        public string cc_debug_response_body { get; set; }

        [JsonProperty("cc_debug_response_serialized")]
        public string cc_debug_response_serialized { get; set; }

        [JsonProperty("cc_exp_month")]
        public string cc_exp_month { get; set; }

        [JsonProperty("cc_exp_year")]
        public string cc_exp_year { get; set; }

        [JsonProperty("cc_last4")]
        public string cc_last4 { get; set; }

        [JsonProperty("cc_number_enc")]
        public string cc_number_enc { get; set; }

        [JsonProperty("cc_owner")]
        public string cc_owner { get; set; }

        [JsonProperty("cc_secure_verify")]
        public string cc_secure_verify { get; set; }

        [JsonProperty("cc_ss_issue")]
        public string cc_ss_issue { get; set; }

        [JsonProperty("cc_ss_start_month")]
        public string cc_ss_start_month { get; set; }

        [JsonProperty("cc_ss_start_year")]
        public string cc_ss_start_year { get; set; }

        [JsonProperty("cc_status")]
        public string cc_status { get; set; }

        [JsonProperty("cc_status_description")]
        public string cc_status_description { get; set; }

        [JsonProperty("cc_trans_id")]
        public string cc_trans_id { get; set; }

        [JsonProperty("cc_type")]
        public string cc_type { get; set; }

        [JsonProperty("echeck_account_name")]
        public string echeck_account_name { get; set; }

        [JsonProperty("echeck_account_type")]
        public string echeck_account_type { get; set; }

        [JsonProperty("echeck_bank_name")]
        public string echeck_bank_name { get; set; }

        [JsonProperty("echeck_routing_number")]
        public string echeck_routing_number { get; set; }

        [JsonProperty("echeck_type")]
        public string echeck_type { get; set; }

        [JsonProperty("entity_id")]
        public int entity_id { get; set; }

        [JsonProperty("last_trans_id")]
        public string last_trans_id { get; set; }

        [JsonProperty("method")]
        public string method { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("po_number")]
        public string po_number { get; set; }

        [JsonProperty("protection_eligibility")]
        public string protection_eligibility { get; set; }

        [JsonProperty("quote_payment_id")]
        public int quote_payment_id { get; set; }

        [JsonProperty("shipping_amount")]
        public int shipping_amount { get; set; }

        [JsonProperty("shipping_captured")]
        public int shipping_captured { get; set; }

        [JsonProperty("shipping_refunded")]
        public int shipping_refunded { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class StatusHistory
    {

        [JsonProperty("comment")]
        public string comment { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("entity_id")]
        public int entity_id { get; set; }

        [JsonProperty("entity_name")]
        public string entity_name { get; set; }

        [JsonProperty("is_customer_notified")]
        public int is_customer_notified { get; set; }

        [JsonProperty("is_visible_on_front")]
        public int is_visible_on_front { get; set; }

        [JsonProperty("parent_id")]
        public int parent_id { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }

    public class Order
    {
        [JsonProperty("adjustment_negative")]
        public int adjustment_negative { get; set; }

        [JsonProperty("adjustment_positive")]
        public int adjustment_positive { get; set; }

        [JsonProperty("applied_rule_ids")]
        public string applied_rule_ids { get; set; }

        [JsonProperty("base_adjustment_negative")]
        public int base_adjustment_negative { get; set; }

        [JsonProperty("base_adjustment_positive")]
        public int base_adjustment_positive { get; set; }

        [JsonProperty("base_currency_code")]
        public string base_currency_code { get; set; }

        [JsonProperty("base_discount_amount")]
        public int base_discount_amount { get; set; }

        [JsonProperty("base_discount_canceled")]
        public int base_discount_canceled { get; set; }

        [JsonProperty("base_discount_invoiced")]
        public int base_discount_invoiced { get; set; }

        [JsonProperty("base_discount_refunded")]
        public int base_discount_refunded { get; set; }

        [JsonProperty("base_grand_total")]
        public int base_grand_total { get; set; }

        [JsonProperty("base_discount_tax_compensation_amount")]
        public int base_discount_tax_compensation_amount { get; set; }

        [JsonProperty("base_discount_tax_compensation_invoiced")]
        public int base_discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("base_discount_tax_compensation_refunded")]
        public int base_discount_tax_compensation_refunded { get; set; }

        [JsonProperty("base_shipping_amount")]
        public int base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_canceled")]
        public int base_shipping_canceled { get; set; }

        [JsonProperty("base_shipping_discount_amount")]
        public int base_shipping_discount_amount { get; set; }

        [JsonProperty("base_shipping_discount_tax_compensation_amnt")]
        public int base_shipping_discount_tax_compensation_amnt { get; set; }

        [JsonProperty("base_shipping_incl_tax")]
        public int base_shipping_incl_tax { get; set; }

        [JsonProperty("base_shipping_invoiced")]
        public int base_shipping_invoiced { get; set; }

        [JsonProperty("base_shipping_refunded")]
        public int base_shipping_refunded { get; set; }

        [JsonProperty("base_shipping_tax_amount")]
        public int base_shipping_tax_amount { get; set; }

        [JsonProperty("base_shipping_tax_refunded")]
        public int base_shipping_tax_refunded { get; set; }

        [JsonProperty("base_subtotal")]
        public int base_subtotal { get; set; }

        [JsonProperty("base_subtotal_canceled")]
        public int base_subtotal_canceled { get; set; }

        [JsonProperty("base_subtotal_incl_tax")]
        public int base_subtotal_incl_tax { get; set; }

        [JsonProperty("base_subtotal_invoiced")]
        public int base_subtotal_invoiced { get; set; }

        [JsonProperty("base_subtotal_refunded")]
        public int base_subtotal_refunded { get; set; }

        [JsonProperty("base_tax_amount")]
        public int base_tax_amount { get; set; }

        [JsonProperty("base_tax_canceled")]
        public int base_tax_canceled { get; set; }

        [JsonProperty("base_tax_invoiced")]
        public int base_tax_invoiced { get; set; }

        [JsonProperty("base_tax_refunded")]
        public int base_tax_refunded { get; set; }

        [JsonProperty("base_total_canceled")]
        public int base_total_canceled { get; set; }

        [JsonProperty("base_total_due")]
        public int base_total_due { get; set; }

        [JsonProperty("base_total_invoiced")]
        public int base_total_invoiced { get; set; }

        [JsonProperty("base_total_invoiced_cost")]
        public int base_total_invoiced_cost { get; set; }

        [JsonProperty("base_total_offline_refunded")]
        public int base_total_offline_refunded { get; set; }

        [JsonProperty("base_total_online_refunded")]
        public int base_total_online_refunded { get; set; }

        [JsonProperty("base_total_paid")]
        public int base_total_paid { get; set; }

        [JsonProperty("base_total_qty_ordered")]
        public int base_total_qty_ordered { get; set; }

        [JsonProperty("base_total_refunded")]
        public int base_total_refunded { get; set; }

        [JsonProperty("base_to_global_rate")]
        public int base_to_global_rate { get; set; }

        [JsonProperty("base_to_order_rate")]
        public int base_to_order_rate { get; set; }

        [JsonProperty("billing_address_id")]
        public int billing_address_id { get; set; }

        [JsonProperty("can_ship_partially")]
        public int can_ship_partially { get; set; }

        [JsonProperty("can_ship_partially_item")]
        public int can_ship_partially_item { get; set; }

        [JsonProperty("coupon_code")]
        public string coupon_code { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("customer_dob")]
        public string customer_dob { get; set; }

        [JsonProperty("customer_email")]
        public string customer_email { get; set; }

        [JsonProperty("customer_firstname")]
        public string customer_firstname { get; set; }

        [JsonProperty("customer_gender")]
        public int customer_gender { get; set; }

        [JsonProperty("customer_group_id")]
        public int customer_group_id { get; set; }

        [JsonProperty("customer_id")]
        public int customer_id { get; set; }

        [JsonProperty("customer_is_guest")]
        public int customer_is_guest { get; set; }

        [JsonProperty("customer_lastname")]
        public string customer_lastname { get; set; }

        [JsonProperty("customer_middlename")]
        public string customer_middlename { get; set; }

        [JsonProperty("customer_note")]
        public string customer_note { get; set; }

        [JsonProperty("customer_note_notify")]
        public int customer_note_notify { get; set; }

        [JsonProperty("customer_prefix")]
        public string customer_prefix { get; set; }

        [JsonProperty("customer_suffix")]
        public string customer_suffix { get; set; }

        [JsonProperty("customer_taxvat")]
        public string customer_taxvat { get; set; }

        [JsonProperty("discount_amount")]
        public int discount_amount { get; set; }

        [JsonProperty("discount_canceled")]
        public int discount_canceled { get; set; }

        [JsonProperty("discount_description")]
        public string discount_description { get; set; }

        [JsonProperty("discount_invoiced")]
        public int discount_invoiced { get; set; }

        [JsonProperty("discount_refunded")]
        public int discount_refunded { get; set; }

        [JsonProperty("edit_increment")]
        public int edit_increment { get; set; }

        [JsonProperty("email_sent")]
        public int email_sent { get; set; }

        [JsonProperty("entity_id")]
        public int entity_id { get; set; }

        [JsonProperty("ext_customer_id")]
        public string ext_customer_id { get; set; }

        [JsonProperty("ext_order_id")]
        public string ext_order_id { get; set; }

        [JsonProperty("forced_shipment_with_invoice")]
        public int forced_shipment_with_invoice { get; set; }

        [JsonProperty("global_currency_code")]
        public string global_currency_code { get; set; }

        [JsonProperty("grand_total")]
        public int grand_total { get; set; }

        [JsonProperty("discount_tax_compensation_amount")]
        public int discount_tax_compensation_amount { get; set; }

        [JsonProperty("discount_tax_compensation_invoiced")]
        public int discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("discount_tax_compensation_refunded")]
        public int discount_tax_compensation_refunded { get; set; }

        [JsonProperty("hold_before_state")]
        public string hold_before_state { get; set; }

        [JsonProperty("hold_before_status")]
        public string hold_before_status { get; set; }

        [JsonProperty("increment_id")]
        public string increment_id { get; set; }

        [JsonProperty("is_virtual")]
        public int is_virtual { get; set; }

        [JsonProperty("order_currency_code")]
        public string order_currency_code { get; set; }

        [JsonProperty("original_increment_id")]
        public string original_increment_id { get; set; }

        [JsonProperty("payment_authorization_amount")]
        public int payment_authorization_amount { get; set; }

        [JsonProperty("payment_auth_expiration")]
        public int payment_auth_expiration { get; set; }

        [JsonProperty("protect_code")]
        public string protect_code { get; set; }

        [JsonProperty("quote_address_id")]
        public int quote_address_id { get; set; }

        [JsonProperty("quote_id")]
        public int quote_id { get; set; }

        [JsonProperty("relation_child_id")]
        public string relation_child_id { get; set; }

        [JsonProperty("relation_child_real_id")]
        public string relation_child_real_id { get; set; }

        [JsonProperty("relation_parent_id")]
        public string relation_parent_id { get; set; }

        [JsonProperty("relation_parent_real_id")]
        public string relation_parent_real_id { get; set; }

        [JsonProperty("remote_ip")]
        public string remote_ip { get; set; }

        [JsonProperty("shipping_amount")]
        public int shipping_amount { get; set; }

        [JsonProperty("shipping_canceled")]
        public int shipping_canceled { get; set; }

        [JsonProperty("shipping_description")]
        public string shipping_description { get; set; }

        [JsonProperty("shipping_discount_amount")]
        public int shipping_discount_amount { get; set; }

        [JsonProperty("shipping_discount_tax_compensation_amount")]
        public int shipping_discount_tax_compensation_amount { get; set; }

        [JsonProperty("shipping_incl_tax")]
        public int shipping_incl_tax { get; set; }

        [JsonProperty("shipping_invoiced")]
        public int shipping_invoiced { get; set; }

        [JsonProperty("shipping_refunded")]
        public int shipping_refunded { get; set; }

        [JsonProperty("shipping_tax_amount")]
        public int shipping_tax_amount { get; set; }

        [JsonProperty("shipping_tax_refunded")]
        public int shipping_tax_refunded { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("store_currency_code")]
        public string store_currency_code { get; set; }

        [JsonProperty("store_id")]
        public int store_id { get; set; }

        [JsonProperty("store_name")]
        public string store_name { get; set; }

        [JsonProperty("store_to_base_rate")]
        public int store_to_base_rate { get; set; }

        [JsonProperty("store_to_order_rate")]
        public int store_to_order_rate { get; set; }

        [JsonProperty("subtotal")]
        public int subtotal { get; set; }

        [JsonProperty("subtotal_canceled")]
        public int subtotal_canceled { get; set; }

        [JsonProperty("subtotal_incl_tax")]
        public int subtotal_incl_tax { get; set; }

        [JsonProperty("subtotal_invoiced")]
        public int subtotal_invoiced { get; set; }

        [JsonProperty("subtotal_refunded")]
        public int subtotal_refunded { get; set; }

        [JsonProperty("tax_amount")]
        public int tax_amount { get; set; }

        [JsonProperty("tax_canceled")]
        public int tax_canceled { get; set; }

        [JsonProperty("tax_invoiced")]
        public int tax_invoiced { get; set; }

        [JsonProperty("tax_refunded")]
        public int tax_refunded { get; set; }

        [JsonProperty("total_canceled")]
        public int total_canceled { get; set; }

        [JsonProperty("total_due")]
        public int total_due { get; set; }

        [JsonProperty("total_invoiced")]
        public int total_invoiced { get; set; }

        [JsonProperty("total_item_count")]
        public int total_item_count { get; set; }

        [JsonProperty("total_offline_refunded")]
        public int total_offline_refunded { get; set; }

        [JsonProperty("total_online_refunded")]
        public int total_online_refunded { get; set; }

        [JsonProperty("total_paid")]
        public int total_paid { get; set; }

        [JsonProperty("total_qty_ordered")]
        public int total_qty_ordered { get; set; }

        [JsonProperty("total_refunded")]
        public int total_refunded { get; set; }

        [JsonProperty("updated_at")]
        public string updated_at { get; set; }

        [JsonProperty("weight")]
        public int weight { get; set; }

        [JsonProperty("x_forwarded_for")]
        public string x_forwarded_for { get; set; }

        [JsonProperty("items")]
        public IList<Item> items { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress billing_address { get; set; }

        [JsonProperty("payment")]
        public Payment payment { get; set; }

        [JsonProperty("status_histories")]
        public IList<StatusHistory> status_histories { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes extension_attributes { get; set; }
    }
}
