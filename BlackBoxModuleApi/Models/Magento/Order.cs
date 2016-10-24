using Magento.RestApi.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace BlackBoxModuleApi.Models.Magento
{
    public class InlineModel
    {
        [JsonProperty("entity", Required = Required.Always)]
        public SalesDataOrder entity { get; set; }
    }

    public class SalesDataOrder
    {
        [JsonProperty("adjustment_negative", Required = Required.Default)]
        public decimal? adjustment_negative { get; set; }

        [JsonProperty("adjustment_positive", Required = Required.Default)]
        public decimal? adjustment_positive { get; set; }

        [JsonProperty("applied_rule_ids", Required = Required.Default)]
        public string applied_rule_ids { get; set; }

        [JsonProperty("base_adjustment_negative", Required = Required.Default)]
        public decimal? base_adjustment_negative { get; set; }

        [JsonProperty("base_adjustment_positive", Required = Required.Default)]
        public decimal? base_adjustment_positive { get; set; }

        [JsonProperty("base_currency_code", Required = Required.Default)]
        public string base_currency_code { get; set; }

        [JsonProperty("base_discount_amount", Required = Required.Default)]
        public decimal? base_discount_amount { get; set; }

        [JsonProperty("base_discount_canceled", Required = Required.Default)]
        public decimal? base_discount_canceled { get; set; }

        [JsonProperty("base_discount_invoiced", Required = Required.Default)]
        public decimal? base_discount_invoiced { get; set; }

        [JsonProperty("base_discount_refunded", Required = Required.Default)]
        public decimal? base_discount_refunded { get; set; }

        [JsonProperty("base_grand_total", Required = Required.Always)]
        public decimal base_grand_total { get; set; }

        [JsonProperty("base_discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_amount { get; set; }

        [JsonProperty("base_discount_tax_compensation_invoiced", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("base_discount_tax_compensation_refunded", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_refunded { get; set; }

        [JsonProperty("base_shipping_amount", Required = Required.Default)]
        public decimal? base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_canceled", Required = Required.Default)]
        public decimal? base_shipping_canceled { get; set; }

        [JsonProperty("base_shipping_discount_amount", Required = Required.Default)]
        public decimal? base_shipping_discount_amount { get; set; }

        [JsonProperty("base_shipping_discount_tax_compensation_amnt", Required = Required.Default)]
        public decimal? base_shipping_discount_tax_compensation_amnt { get; set; }

        [JsonProperty("base_shipping_incl_tax", Required = Required.Default)]
        public decimal? base_shipping_incl_tax { get; set; }

        [JsonProperty("base_shipping_invoiced", Required = Required.Default)]
        public decimal? base_shipping_invoiced { get; set; }

        [JsonProperty("base_shipping_refunded", Required = Required.Default)]
        public decimal? base_shipping_refunded { get; set; }

        [JsonProperty("base_shipping_tax_amount", Required = Required.Default)]
        public decimal? base_shipping_tax_amount { get; set; }

        [JsonProperty("base_shipping_tax_refunded", Required = Required.Default)]
        public decimal? base_shipping_tax_refunded { get; set; }

        [JsonProperty("base_subtotal", Required = Required.Default)]
        public decimal? base_subtotal { get; set; }

        [JsonProperty("base_subtotal_canceled", Required = Required.Default)]
        public decimal? base_subtotal_canceled { get; set; }

        [JsonProperty("base_subtotal_incl_tax", Required = Required.Default)]
        public decimal? base_subtotal_incl_tax { get; set; }

        [JsonProperty("base_subtotal_invoiced", Required = Required.Default)]
        public decimal? base_subtotal_invoiced { get; set; }

        [JsonProperty("base_subtotal_refunded", Required = Required.Default)]
        public decimal? base_subtotal_refunded { get; set; }

        [JsonProperty("base_tax_amount", Required = Required.Default)]
        public decimal? base_tax_amount { get; set; }

        [JsonProperty("base_tax_canceled", Required = Required.Default)]
        public decimal? base_tax_canceled { get; set; }

        [JsonProperty("base_tax_invoiced", Required = Required.Default)]
        public decimal? base_tax_invoiced { get; set; }

        [JsonProperty("base_tax_refunded", Required = Required.Default)]
        public decimal? base_tax_refunded { get; set; }

        [JsonProperty("base_total_canceled", Required = Required.Default)]
        public decimal? base_total_canceled { get; set; }

        [JsonProperty("base_total_due", Required = Required.Default)]
        public decimal? base_total_due { get; set; }

        [JsonProperty("base_total_invoiced", Required = Required.Default)]
        public decimal? base_total_invoiced { get; set; }

        [JsonProperty("base_total_invoiced_cost", Required = Required.Default)]
        public decimal? base_total_invoiced_cost { get; set; }

        [JsonProperty("base_total_offline_refunded", Required = Required.Default)]
        public decimal? base_total_offline_refunded { get; set; }

        [JsonProperty("base_total_online_refunded", Required = Required.Default)]
        public decimal? base_total_online_refunded { get; set; }

        [JsonProperty("base_total_paid", Required = Required.Default)]
        public decimal? base_total_paid { get; set; }

        [JsonProperty("base_total_qty_ordered", Required = Required.Default)]
        public decimal? base_total_qty_ordered { get; set; }

        [JsonProperty("base_total_refunded", Required = Required.Default)]
        public decimal? base_total_refunded { get; set; }

        [JsonProperty("base_to_global_rate", Required = Required.Default)]
        public decimal? base_to_global_rate { get; set; }

        [JsonProperty("base_to_order_rate", Required = Required.Default)]
        public decimal? base_to_order_rate { get; set; }

        [JsonProperty("billing_address_id", Required = Required.Default)]
        public int? billing_address_id { get; set; }

        [JsonProperty("can_ship_partially", Required = Required.Default)]
        public int? can_ship_partially { get; set; }

        [JsonProperty("can_ship_partially_item", Required = Required.Default)]
        public int? can_ship_partially_item { get; set; }

        [JsonProperty("coupon_code", Required = Required.Default)]
        public string coupon_code { get; set; }

        [JsonProperty("created_at", Required = Required.Default)]
        public string created_at { get; set; }

        [JsonProperty("customer_dob", Required = Required.Default)]
        public string customer_dob { get; set; }

        [JsonProperty("customer_email", Required = Required.Always)]
        public string customer_email { get; set; }

        [JsonProperty("customer_firstname", Required = Required.Default)]
        public string customer_firstname { get; set; }

        [JsonProperty("customer_gender", Required = Required.Default)]
        public int? customer_gender { get; set; }

        [JsonProperty("customer_group_id", Required = Required.Default)]
        public int? customer_group_id { get; set; }

        [JsonProperty("customer_id", Required = Required.Default)]
        public int? customer_id { get; set; }

        [JsonProperty("customer_is_guest", Required = Required.Default)]
        public int? customer_is_guest { get; set; }

        [JsonProperty("customer_lastname", Required = Required.Default)]
        public string customer_lastname { get; set; }

        [JsonProperty("customer_middlename", Required = Required.Default)]
        public string customer_middlename { get; set; }

        [JsonProperty("customer_note", Required = Required.Default)]
        public string customer_note { get; set; }

        [JsonProperty("customer_note_notify", Required = Required.Default)]
        public int? customer_note_notify { get; set; }

        [JsonProperty("customer_prefix", Required = Required.Default)]
        public string customer_prefix { get; set; }

        [JsonProperty("customer_suffix", Required = Required.Default)]
        public string customer_suffix { get; set; }

        [JsonProperty("customer_taxvat", Required = Required.Default)]
        public string customer_taxvat { get; set; }

        [JsonProperty("discount_amount", Required = Required.Default)]
        public decimal? discount_amount { get; set; }

        [JsonProperty("discount_canceled", Required = Required.Default)]
        public decimal? discount_canceled { get; set; }

        [JsonProperty("discount_description", Required = Required.Default)]
        public string discount_description { get; set; }

        [JsonProperty("discount_invoiced", Required = Required.Default)]
        public decimal? discount_invoiced { get; set; }

        [JsonProperty("discount_refunded", Required = Required.Default)]
        public decimal? discount_refunded { get; set; }

        [JsonProperty("edit_increment", Required = Required.Default)]
        public int? edit_increment { get; set; }

        [JsonProperty("email_sent", Required = Required.Default)]
        public int? email_sent { get; set; }

        [JsonProperty("entity_id", Required = Required.Default)]
        public int? entity_id { get; set; }

        [JsonProperty("ext_customer_id", Required = Required.Default)]
        public string ext_customer_id { get; set; }

        [JsonProperty("ext_order_id", Required = Required.Default)]
        public string ext_order_id { get; set; }

        [JsonProperty("forced_shipment_with_invoice", Required = Required.Default)]
        public int? forced_shipment_with_invoice { get; set; }

        [JsonProperty("global_currency_code", Required = Required.Default)]
        public string global_currency_code { get; set; }

        [JsonProperty("grand_total", Required = Required.Always)]
        public decimal grand_total { get; set; }

        [JsonProperty("discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? discount_tax_compensation_amount { get; set; }

        [JsonProperty("discount_tax_compensation_invoiced", Required = Required.Default)]
        public decimal? discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("discount_tax_compensation_refunded", Required = Required.Default)]
        public decimal? discount_tax_compensation_refunded { get; set; }

        [JsonProperty("hold_before_state", Required = Required.Default)]
        public string hold_before_state { get; set; }

        [JsonProperty("hold_before_status", Required = Required.Default)]
        public string hold_before_status { get; set; }

        [JsonProperty("increment_id", Required = Required.Default)]
        public string increment_id { get; set; }

        [JsonProperty("is_virtual", Required = Required.Default)]
        public int? is_virtual { get; set; }

        [JsonProperty("order_currency_code", Required = Required.Default)]
        public string order_currency_code { get; set; }

        [JsonProperty("original_increment_id", Required = Required.Default)]
        public string original_increment_id { get; set; }

        [JsonProperty("payment_authorization_amount", Required = Required.Default)]
        public decimal? payment_authorization_amount { get; set; }

        [JsonProperty("payment_auth_expiration", Required = Required.Default)]
        public int? payment_auth_expiration { get; set; }

        [JsonProperty("protect_code", Required = Required.Default)]
        public string protect_code { get; set; }

        [JsonProperty("quote_address_id", Required = Required.Default)]
        public int? quote_address_id { get; set; }

        [JsonProperty("quote_id", Required = Required.Default)]
        public int? quote_id { get; set; }

        [JsonProperty("relation_child_id", Required = Required.Default)]
        public string relation_child_id { get; set; }

        [JsonProperty("relation_child_real_id", Required = Required.Default)]
        public string relation_child_real_id { get; set; }

        [JsonProperty("relation_parent_id", Required = Required.Default)]
        public string relation_parent_id { get; set; }

        [JsonProperty("relation_parent_real_id", Required = Required.Default)]
        public string relation_parent_real_id { get; set; }

        [JsonProperty("remote_ip", Required = Required.Default)]
        public string remote_ip { get; set; }

        [JsonProperty("shipping_amount", Required = Required.Default)]
        public decimal? shipping_amount { get; set; }

        [JsonProperty("shipping_canceled", Required = Required.Default)]
        public decimal? shipping_canceled { get; set; }

        [JsonProperty("shipping_description", Required = Required.Default)]
        public string shipping_description { get; set; }

        [JsonProperty("shipping_discount_amount", Required = Required.Default)]
        public decimal? shipping_discount_amount { get; set; }

        [JsonProperty("shipping_discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? shipping_discount_tax_compensation_amount { get; set; }

        [JsonProperty("shipping_incl_tax", Required = Required.Default)]
        public decimal? shipping_incl_tax { get; set; }

        [JsonProperty("shipping_invoiced", Required = Required.Default)]
        public decimal? shipping_invoiced { get; set; }

        [JsonProperty("shipping_refunded", Required = Required.Default)]
        public decimal? shipping_refunded { get; set; }

        [JsonProperty("shipping_tax_amount", Required = Required.Default)]
        public decimal? shipping_tax_amount { get; set; }

        [JsonProperty("shipping_tax_refunded", Required = Required.Default)]
        public decimal? shipping_tax_refunded { get; set; }

        [JsonProperty("state", Required = Required.Default)]
        public string state { get; set; }

        [JsonProperty("status", Required = Required.Default)]
        public string status { get; set; }

        [JsonProperty("store_currency_code", Required = Required.Default)]
        public string store_currency_code { get; set; }

        [JsonProperty("store_id", Required = Required.Default)]
        public int? store_id { get; set; }

        [JsonProperty("store_name", Required = Required.Default)]
        public string store_name { get; set; }

        [JsonProperty("store_to_base_rate", Required = Required.Default)]
        public decimal? store_to_base_rate { get; set; }

        [JsonProperty("store_to_order_rate", Required = Required.Default)]
        public decimal? store_to_order_rate { get; set; }

        [JsonProperty("subtotal", Required = Required.Default)]
        public decimal? subtotal { get; set; }

        [JsonProperty("subtotal_canceled", Required = Required.Default)]
        public decimal? subtotal_canceled { get; set; }

        [JsonProperty("subtotal_incl_tax", Required = Required.Default)]
        public decimal? subtotal_incl_tax { get; set; }

        [JsonProperty("subtotal_invoiced", Required = Required.Default)]
        public decimal? subtotal_invoiced { get; set; }

        [JsonProperty("subtotal_refunded", Required = Required.Default)]
        public decimal? subtotal_refunded { get; set; }

        [JsonProperty("tax_amount", Required = Required.Default)]
        public decimal? tax_amount { get; set; }

        [JsonProperty("tax_canceled", Required = Required.Default)]
        public decimal? tax_canceled { get; set; }

        [JsonProperty("tax_invoiced", Required = Required.Default)]
        public decimal? tax_invoiced { get; set; }

        [JsonProperty("tax_refunded", Required = Required.Default)]
        public decimal? tax_refunded { get; set; }

        [JsonProperty("total_canceled", Required = Required.Default)]
        public decimal? total_canceled { get; set; }

        [JsonProperty("total_due", Required = Required.Default)]
        public decimal? total_due { get; set; }

        [JsonProperty("total_invoiced", Required = Required.Default)]
        public decimal? total_invoiced { get; set; }

        [JsonProperty("total_item_count", Required = Required.Default)]
        public int? total_item_count { get; set; }

        [JsonProperty("total_offline_refunded", Required = Required.Default)]
        public decimal? total_offline_refunded { get; set; }

        [JsonProperty("total_online_refunded", Required = Required.Default)]
        public decimal? total_online_refunded { get; set; }

        [JsonProperty("total_paid", Required = Required.Default)]
        public decimal? total_paid { get; set; }

        [JsonProperty("total_qty_ordered", Required = Required.Default)]
        public decimal? total_qty_ordered { get; set; }

        [JsonProperty("total_refunded", Required = Required.Default)]
        public decimal? total_refunded { get; set; }

        [JsonProperty("updated_at", Required = Required.Default)]
        public string updated_at { get; set; }

        [JsonProperty("weight", Required = Required.Default)]
        public decimal? weight { get; set; }

        [JsonProperty("x_forwarded_for", Required = Required.Default)]
        public string x_forwarded_for { get; set; }

        [JsonProperty("items", Required = Required.Always)]
        public List<SalesDataOrderItem> items { get; set; }

        [JsonProperty("billing_address", Required = Required.Default)]
        public SalesDataOrderAddress billing_address { get; set; }

        [JsonProperty("payment", Required = Required.Default)]
        public SalesDataOrderPayment payment { get; set; }

        [JsonProperty("status_histories", Required = Required.Default)]
        public List<SalesDataOrderStatusHistory> status_histories { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataOrderExtension extension_attributes { get; set; }
    }

    public class SalesDataOrderItem
    {
        [JsonProperty("additional_data", Required = Required.Default)]
        public string additional_data { get; set; }

        [JsonProperty("amount_refunded", Required = Required.Default)]
        public decimal? amount_refunded { get; set; }

        [JsonProperty("applied_rule_ids", Required = Required.Default)]
        public string applied_rule_ids { get; set; }

        [JsonProperty("base_amount_refunded", Required = Required.Default)]
        public decimal? base_amount_refunded { get; set; }

        [JsonProperty("base_cost", Required = Required.Default)]
        public decimal? base_cost { get; set; }

        [JsonProperty("base_discount_amount", Required = Required.Default)]
        public decimal? base_discount_amount { get; set; }

        [JsonProperty("base_discount_invoiced", Required = Required.Default)]
        public decimal? base_discount_invoiced { get; set; }

        [JsonProperty("base_discount_refunded", Required = Required.Default)]
        public decimal? base_discount_refunded { get; set; }

        [JsonProperty("base_discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_amount { get; set; }

        [JsonProperty("base_discount_tax_compensation_invoiced", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("base_discount_tax_compensation_refunded", Required = Required.Default)]
        public decimal? base_discount_tax_compensation_refunded { get; set; }

        [JsonProperty("base_original_price", Required = Required.Default)]
        public decimal? base_original_price { get; set; }

        [JsonProperty("base_price", Required = Required.Default)]
        public decimal? base_price { get; set; }

        [JsonProperty("base_price_incl_tax", Required = Required.Default)]
        public decimal? base_price_incl_tax { get; set; }

        [JsonProperty("base_row_invoiced", Required = Required.Default)]
        public decimal? base_row_invoiced { get; set; }

        [JsonProperty("base_row_total", Required = Required.Default)]
        public decimal? base_row_total { get; set; }

        [JsonProperty("base_row_total_incl_tax", Required = Required.Default)]
        public decimal? base_row_total_incl_tax { get; set; }

        [JsonProperty("base_tax_amount", Required = Required.Default)]
        public decimal? base_tax_amount { get; set; }

        [JsonProperty("base_tax_before_discount", Required = Required.Default)]
        public decimal? base_tax_before_discount { get; set; }

        [JsonProperty("base_tax_invoiced", Required = Required.Default)]
        public decimal? base_tax_invoiced { get; set; }

        [JsonProperty("base_tax_refunded", Required = Required.Default)]
        public decimal? base_tax_refunded { get; set; }

        [JsonProperty("base_weee_tax_applied_amount", Required = Required.Default)]
        public decimal? base_weee_tax_applied_amount { get; set; }

        [JsonProperty("base_weee_tax_applied_row_amnt", Required = Required.Default)]
        public decimal? base_weee_tax_applied_row_amnt { get; set; }

        [JsonProperty("base_weee_tax_disposition", Required = Required.Default)]
        public decimal? base_weee_tax_disposition { get; set; }

        [JsonProperty("base_weee_tax_row_disposition", Required = Required.Default)]
        public decimal? base_weee_tax_row_disposition { get; set; }

        [JsonProperty("created_at", Required = Required.Default)]
        public string created_at { get; set; }

        [JsonProperty("description", Required = Required.Default)]
        public string description { get; set; }

        [JsonProperty("discount_amount", Required = Required.Default)]
        public decimal? discount_amount { get; set; }

        [JsonProperty("discount_invoiced", Required = Required.Default)]
        public decimal? discount_invoiced { get; set; }

        [JsonProperty("discount_percent", Required = Required.Default)]
        public decimal? discount_percent { get; set; }

        [JsonProperty("discount_refunded", Required = Required.Default)]
        public decimal? discount_refunded { get; set; }

        [JsonProperty("event_id", Required = Required.Default)]
        public int? event_id { get; set; }

        [JsonProperty("ext_order_item_id", Required = Required.Default)]
        public string ext_order_item_id { get; set; }

        [JsonProperty("free_shipping", Required = Required.Default)]
        public int? free_shipping { get; set; }

        [JsonProperty("gw_base_price", Required = Required.Default)]
        public decimal? gw_base_price { get; set; }

        [JsonProperty("gw_base_price_invoiced", Required = Required.Default)]
        public decimal? gw_base_price_invoiced { get; set; }

        [JsonProperty("gw_base_price_refunded", Required = Required.Default)]
        public decimal? gw_base_price_refunded { get; set; }

        [JsonProperty("gw_base_tax_amount", Required = Required.Default)]
        public decimal? gw_base_tax_amount { get; set; }

        [JsonProperty("gw_base_tax_amount_invoiced", Required = Required.Default)]
        public decimal? gw_base_tax_amount_invoiced { get; set; }

        [JsonProperty("gw_base_tax_amount_refunded", Required = Required.Default)]
        public decimal? gw_base_tax_amount_refunded { get; set; }

        [JsonProperty("gw_id", Required = Required.Default)]
        public int? gw_id { get; set; }

        [JsonProperty("gw_price", Required = Required.Default)]
        public decimal? gw_price { get; set; }

        [JsonProperty("gw_price_invoiced", Required = Required.Default)]
        public decimal? gw_price_invoiced { get; set; }

        [JsonProperty("gw_price_refunded", Required = Required.Default)]
        public decimal? gw_price_refunded { get; set; }

        [JsonProperty("gw_tax_amount", Required = Required.Default)]
        public decimal? gw_tax_amount { get; set; }

        [JsonProperty("gw_tax_amount_invoiced", Required = Required.Default)]
        public decimal? gw_tax_amount_invoiced { get; set; }

        [JsonProperty("gw_tax_amount_refunded", Required = Required.Default)]
        public decimal? gw_tax_amount_refunded { get; set; }

        [JsonProperty("discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? discount_tax_compensation_amount { get; set; }

        [JsonProperty("discount_tax_compensation_canceled", Required = Required.Default)]
        public decimal? discount_tax_compensation_canceled { get; set; }

        [JsonProperty("discount_tax_compensation_invoiced", Required = Required.Default)]
        public decimal? discount_tax_compensation_invoiced { get; set; }

        [JsonProperty("discount_tax_compensation_refunded", Required = Required.Default)]
        public decimal? discount_tax_compensation_refunded { get; set; }

        [JsonProperty("is_qty_decimal", Required = Required.Default)]
        public int? is_qty_decimal { get; set; }

        [JsonProperty("is_virtual", Required = Required.Default)]
        public int? is_virtual { get; set; }

        [JsonProperty("item_id", Required = Required.Default)]
        public int? item_id { get; set; }

        [JsonProperty("locked_do_invoice", Required = Required.Default)]
        public int? locked_do_invoice { get; set; }

        [JsonProperty("locked_do_ship", Required = Required.Default)]
        public int? locked_do_ship { get; set; }

        [JsonProperty("name", Required = Required.Default)]
        public string name { get; set; }

        [JsonProperty("no_discount", Required = Required.Default)]
        public int? no_discount { get; set; }

        [JsonProperty("order_id", Required = Required.Default)]
        public int? order_id { get; set; }

        [JsonProperty("original_price", Required = Required.Default)]
        public decimal? original_price { get; set; }

        [JsonProperty("parent_item_id", Required = Required.Default)]
        public int? parent_item_id { get; set; }

        [JsonProperty("price", Required = Required.Default)]
        public decimal? price { get; set; }

        [JsonProperty("price_incl_tax", Required = Required.Default)]
        public decimal? price_incl_tax { get; set; }

        [JsonProperty("product_id", Required = Required.Default)]
        public int? product_id { get; set; }

        [JsonProperty("product_type", Required = Required.Default)]
        public string product_type { get; set; }

        [JsonProperty("qty_backordered", Required = Required.Default)]
        public decimal? qty_backordered { get; set; }

        [JsonProperty("qty_canceled", Required = Required.Default)]
        public decimal? qty_canceled { get; set; }

        [JsonProperty("qty_invoiced", Required = Required.Default)]
        public decimal? qty_invoiced { get; set; }

        [JsonProperty("qty_ordered", Required = Required.Default)]
        public decimal? qty_ordered { get; set; }

        [JsonProperty("qty_refunded", Required = Required.Default)]
        public decimal? qty_refunded { get; set; }

        [JsonProperty("qty_returned", Required = Required.Default)]
        public decimal? qty_returned { get; set; }

        [JsonProperty("qty_shipped", Required = Required.Default)]
        public decimal? qty_shipped { get; set; }

        [JsonProperty("quote_item_id", Required = Required.Default)]
        public int? quote_item_id { get; set; }

        [JsonProperty("row_invoiced", Required = Required.Default)]
        public decimal? row_invoiced { get; set; }

        [JsonProperty("row_total", Required = Required.Default)]
        public decimal? row_total { get; set; }

        [JsonProperty("row_total_incl_tax", Required = Required.Default)]
        public decimal? row_total_incl_tax { get; set; }

        [JsonProperty("row_weight", Required = Required.Default)]
        public decimal? row_weight { get; set; }

        [JsonProperty("sku", Required = Required.Default)]
        public string sku { get; set; }

        [JsonProperty("store_id", Required = Required.Default)]
        public int? store_id { get; set; }

        [JsonProperty("tax_amount", Required = Required.Default)]
        public decimal? tax_amount { get; set; }

        [JsonProperty("tax_before_discount", Required = Required.Default)]
        public decimal? tax_before_discount { get; set; }

        [JsonProperty("tax_canceled", Required = Required.Default)]
        public decimal? tax_canceled { get; set; }

        [JsonProperty("tax_invoiced", Required = Required.Default)]
        public decimal? tax_invoiced { get; set; }

        [JsonProperty("tax_percent", Required = Required.Default)]
        public decimal? tax_percent { get; set; }

        [JsonProperty("tax_refunded", Required = Required.Default)]
        public decimal? tax_refunded { get; set; }

        [JsonProperty("updated_at", Required = Required.Default)]
        public string updated_at { get; set; }

        [JsonProperty("weee_tax_applied", Required = Required.Default)]
        public string weee_tax_applied { get; set; }

        [JsonProperty("weee_tax_applied_amount", Required = Required.Default)]
        public decimal? weee_tax_applied_amount { get; set; }

        [JsonProperty("weee_tax_applied_row_amount", Required = Required.Default)]
        public decimal? weee_tax_applied_row_amount { get; set; }

        [JsonProperty("weee_tax_disposition", Required = Required.Default)]
        public decimal? weee_tax_disposition { get; set; }

        [JsonProperty("weee_tax_row_disposition", Required = Required.Default)]
        public decimal? weee_tax_row_disposition { get; set; }

        [JsonProperty("weight", Required = Required.Default)]
        public decimal? weight { get; set; }

        [JsonProperty("parent_item", Required = Required.Default)]
        public SalesDataOrderItem parent_item { get; set; }

        [JsonProperty("product_option", Required = Required.Default)]
        public CatalogDataProductOption product_option { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataOrderItemExtension extension_attributes { get; set; }
    }

    public class SalesDataOrderAddress
    {
        [JsonProperty("address_type", Required = Required.Always)]
        public string address_type { get; set; }

        [JsonProperty("city", Required = Required.Always)]
        public string city { get; set; }

        [JsonProperty("company", Required = Required.Default)]
        public string company { get; set; }

        [JsonProperty("country_id", Required = Required.Always)]
        public string country_id { get; set; }

        [JsonProperty("customer_address_id", Required = Required.Default)]
        public int? customer_address_id { get; set; }

        [JsonProperty("customer_id", Required = Required.Default)]
        public int? customer_id { get; set; }

        [JsonProperty("email", Required = Required.Default)]
        public string email { get; set; }

        [JsonProperty("entity_id", Required = Required.Default)]
        public int? entity_id { get; set; }

        [JsonProperty("fax", Required = Required.Default)]
        public string fax { get; set; }

        [JsonProperty("firstname", Required = Required.Always)]
        public string firstname { get; set; }

        [JsonProperty("lastname", Required = Required.Always)]
        public string lastname { get; set; }

        [JsonProperty("middlename", Required = Required.Default)]
        public string middlename { get; set; }

        [JsonProperty("parent_id", Required = Required.Default)]
        public int? parent_id { get; set; }

        [JsonProperty("postcode", Required = Required.Always)]
        public string postcode { get; set; }

        [JsonProperty("prefix", Required = Required.Default)]
        public string prefix { get; set; }

        [JsonProperty("region", Required = Required.Default)]
        public string region { get; set; }

        [JsonProperty("region_code", Required = Required.Default)]
        public string region_code { get; set; }

        [JsonProperty("region_id", Required = Required.Default)]
        public int? region_id { get; set; }

        [JsonProperty("street", Required = Required.Default)]
        public List<string> street { get; set; }

        [JsonProperty("suffix", Required = Required.Default)]
        public string suffix { get; set; }

        [JsonProperty("telephone", Required = Required.Always)]
        public string telephone { get; set; }

        [JsonProperty("vat_id", Required = Required.Default)]
        public string vat_id { get; set; }

        [JsonProperty("vat_is_valid", Required = Required.Default)]
        public int? vat_is_valid { get; set; }

        [JsonProperty("vat_request_date", Required = Required.Default)]
        public string vat_request_date { get; set; }

        [JsonProperty("vat_request_id", Required = Required.Default)]
        public string vat_request_id { get; set; }

        [JsonProperty("vat_request_success", Required = Required.Default)]
        public int? vat_request_success { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesdataOrderAddressExtension extension_attributes { get; set; }
    }

    public class SalesDataOrderPayment
    {

        [JsonProperty("account_status", Required = Required.Always)]
        public string account_status { get; set; }

        [JsonProperty("additional_data", Required = Required.Default)]
        public string additional_data { get; set; }

        [JsonProperty("additional_information", Required = Required.Always)]
        public List<string> additional_information { get; }

        [JsonProperty("address_status", Required = Required.Default)]
        public string address_status { get; set; }

        [JsonProperty("amount_authorized", Required = Required.Default)]
        public decimal? amount_authorized { get; set; }

        [JsonProperty("amount_canceled", Required = Required.Default)]
        public decimal? amount_canceled { get; set; }

        [JsonProperty("amount_ordered", Required = Required.Default)]
        public decimal? amount_ordered { get; set; }

        [JsonProperty("amount_paid", Required = Required.Default)]
        public decimal? amount_paid { get; set; }

        [JsonProperty("amount_refunded", Required = Required.Default)]
        public decimal? amount_refunded { get; set; }

        [JsonProperty("anet_trans_method", Required = Required.Default)]
        public string anet_trans_method { get; set; }

        [JsonProperty("base_amount_authorized", Required = Required.Default)]
        public decimal? base_amount_authorized { get; set; }

        [JsonProperty("base_amount_canceled", Required = Required.Default)]
        public decimal? base_amount_canceled { get; set; }

        [JsonProperty("base_amount_ordered", Required = Required.Default)]
        public decimal? base_amount_ordered { get; set; }

        [JsonProperty("base_amount_paid", Required = Required.Default)]
        public decimal? base_amount_paid { get; set; }

        [JsonProperty("base_amount_paid_online", Required = Required.Default)]
        public decimal? base_amount_paid_online { get; set; }

        [JsonProperty("base_amount_refunded", Required = Required.Default)]
        public decimal? base_amount_refunded { get; set; }

        [JsonProperty("base_amount_refunded_online", Required = Required.Default)]
        public decimal? base_amount_refunded_online { get; set; }

        [JsonProperty("base_shipping_amount", Required = Required.Default)]
        public decimal? base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_captured", Required = Required.Default)]
        public decimal? base_shipping_captured { get; set; }

        [JsonProperty("base_shipping_refunded", Required = Required.Default)]
        public decimal? base_shipping_refunded { get; set; }

        [JsonProperty("cc_approval", Required = Required.Default)]
        public string cc_approval { get; set; }

        [JsonProperty("cc_avs_status", Required = Required.Default)]
        public string cc_avs_status { get; set; }

        [JsonProperty("cc_cid_status", Required = Required.Default)]
        public string cc_cid_status { get; set; }

        [JsonProperty("cc_debug_request_body", Required = Required.Default)]
        public string cc_debug_request_body { get; set; }

        [JsonProperty("cc_debug_response_body", Required = Required.Default)]
        public string cc_debug_response_body { get; set; }

        [JsonProperty("cc_debug_response_serialized", Required = Required.Default)]
        public string cc_debug_response_serialized { get; set; }

        [JsonProperty("cc_exp_month", Required = Required.Default)]
        public string cc_exp_month { get; set; }

        [JsonProperty("cc_exp_year", Required = Required.Default)]
        public string cc_exp_year { get; set; }

        [JsonProperty("cc_last4", Required = Required.Always)]
        public string cc_last4 { get; set; }

        [JsonProperty("cc_number_enc", Required = Required.Default)]
        public string cc_number_enc { get; set; }

        [JsonProperty("cc_owner", Required = Required.Default)]
        public string cc_owner { get; set; }

        [JsonProperty("cc_secure_verify", Required = Required.Default)]
        public string cc_secure_verify { get; set; }

        [JsonProperty("cc_ss_issue", Required = Required.Default)]
        public string cc_ss_issue { get; set; }

        [JsonProperty("cc_ss_start_month", Required = Required.Default)]
        public string cc_ss_start_month { get; set; }

        [JsonProperty("cc_ss_start_year", Required = Required.Default)]
        public string cc_ss_start_year { get; set; }

        [JsonProperty("cc_status", Required = Required.Default)]
        public string cc_status { get; set; }

        [JsonProperty("cc_status_description", Required = Required.Default)]
        public string cc_status_description { get; set; }

        [JsonProperty("cc_trans_id", Required = Required.Default)]
        public string cc_trans_id { get; set; }

        [JsonProperty("cc_type", Required = Required.Default)]
        public string cc_type { get; set; }

        [JsonProperty("echeck_account_name", Required = Required.Default)]
        public string echeck_account_name { get; set; }

        [JsonProperty("echeck_account_type", Required = Required.Default)]
        public string echeck_account_type { get; set; }

        [JsonProperty("echeck_bank_name", Required = Required.Default)]
        public string echeck_bank_name { get; set; }

        [JsonProperty("echeck_routing_number", Required = Required.Default)]
        public string echeck_routing_number { get; set; }

        [JsonProperty("echeck_type", Required = Required.Default)]
        public string echeck_type { get; set; }

        [JsonProperty("entity_id", Required = Required.Default)]
        public int? entity_id { get; set; }

        [JsonProperty("last_trans_id", Required = Required.Default)]
        public string last_trans_id { get; set; }

        [JsonProperty("method", Required = Required.Always)]
        public string method { get; set; }

        [JsonProperty("parent_id", Required = Required.Default)]
        public int? parent_id { get; set; }

        [JsonProperty("po_number", Required = Required.Default)]
        public string po_number { get; set; }

        [JsonProperty("protection_eligibility", Required = Required.Default)]
        public string protection_eligibility { get; set; }

        [JsonProperty("quote_payment_id", Required = Required.Default)]
        public int? quote_payment_id { get; set; }

        [JsonProperty("shipping_amount", Required = Required.Default)]
        public decimal? shipping_amount { get; set; }

        [JsonProperty("shipping_captured", Required = Required.Default)]
        public decimal? shipping_captured { get; set; }

        [JsonProperty("shipping_refunded", Required = Required.Default)]
        public decimal? shipping_refunded { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataOrderPaymentExtension extension_attributes { get; set; }

    }

    public class SalesDataOrderStatusHistory
    {
        [JsonProperty("shipping_assignments", Required = Required.Default)]
        public string comment { get; set; }

        [JsonProperty("created_at", Required = Required.Default)]
        public string created_at { get; set; }

        [JsonProperty("entity_id", Required = Required.Default)]
        public int? entity_id { get; set; }

        [JsonProperty("entity_name", Required = Required.Default)]
        public string entity_name { get; set; }

        [JsonProperty("is_customer_notified", Required = Required.Always)]
        public int? is_customer_notified { get; set; }

        [JsonProperty("is_visible_on_front", Required = Required.Always)]
        public int is_visible_on_front { get; set; }

        [JsonProperty("parent_id", Required = Required.Always)]
        public int parent_id { get; set; }

        [JsonProperty("status", Required = Required.Default)]
        public string status { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataOrderStatusHistoryExtension extension_attributes { get; set; }
    }

    public class SalesDataOrderExtension
    {

        [JsonProperty("shipping_assignments", Required = Required.Default)]
        public List<SalesDataShippingAssignment> shipping_assignments { get; set; }

        [JsonProperty("gift_message", Required = Required.Default)]
        public GiftMessageDataMessage gift_message { get; set; }

        [JsonProperty("applied_taxes", Required = Required.Default)]
        public List<TaxDataOrderTaxDetailsAppliedTax> applied_taxes { get; set; }

        [JsonProperty("item_applied_taxes", Required = Required.Default)]
        public List<TaxDataOrderTaxDetailsItem> item_applied_taxes { get; set; }

        [JsonProperty("converting_from_quote", Required = Required.Default)]
        public bool? converting_from_quote { get; set; }
    }

    public class CatalogDataProductOption
    {
        [JsonProperty("extension_attributes", Required = Required.Default)]
        public CatalogDataProductOptionExtension extension_attributes { get; set; }
    }

    public class SalesDataOrderItemExtension
    {
        [JsonProperty("gift_message", Required = Required.Default)]
        public GiftMessageDataMessage gift_message { get; set; }
    }

    public class SalesdataOrderAddressExtension { }

    public class SalesDataOrderPaymentExtension
    {
        [JsonProperty("vault_payment_token", Required = Required.Default)]
        public VaultDataPaymentToken vault_payment_token { get; set; }
    }

    public class SalesDataOrderStatusHistoryExtension { }

    public class SalesDataShippingAssignment
    {
        [JsonProperty("shipping", Required = Required.Always)]
        public SalesDataShipping shipping { get; set; }

        [JsonProperty("items", Required = Required.Always)]
        public List<SalesDataOrderItem> items { get; set; }

        [JsonProperty("stock_id", Required = Required.Default)]
        public int? stock_id { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataShippingAssignmentExtension extension_attributes { get; set; }
    }

    public class GiftMessageDataMessage
    {

        [JsonProperty("gift_message_id", Required = Required.Default)]
        public int? gift_message_id { get; set; }

        [JsonProperty("customer_id", Required = Required.Default)]
        public int? customer_id { get; set; }

        [JsonProperty("sender", Required = Required.Always)]
        public string sender { get; set; }

        [JsonProperty("recipient", Required = Required.Always)]
        public string recipient { get; set; }

        [JsonProperty("message", Required = Required.Always)]
        public string message { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public GiftMessageDataMessageExtension extension_attributes { get; set; }

    }

    public class TaxDataOrderTaxDetailsAppliedTax
    {

        [JsonProperty("code", Required = Required.Default)]
        public string code { get; set; }

        [JsonProperty("title", Required = Required.Default)]
        public string title { get; set; }

        [JsonProperty("percent", Required = Required.Default)]
        public decimal? percent { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public decimal amount { get; set; }

        [JsonProperty("base_amount", Required = Required.Always)]
        public decimal base_amount { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public TaxDataOrderTaxDetailsAppliedTaxExtension extension_attributes { get; set; }
    }

    public class TaxDataOrderTaxDetailsItem
    {

        [JsonProperty("type", Required = Required.Default)]
        public string type { get; set; }

        [JsonProperty("item_id", Required = Required.Default)]
        public int? item_id { get; set; }

        [JsonProperty("associated_item_id", Required = Required.Default)]
        public int? associated_item_id { get; set; }

        [JsonProperty("applied_taxes", Required = Required.Default)]
        public List<TaxDataOrderTaxDetailsAppliedTax> applied_taxes { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public TaxDataOrderTaxDetailsItemExtension extension_attributes { get; set; }

    }

    public class CatalogDataProductOptionExtension
    {

        [JsonProperty("custom_options", Required = Required.Default)]
        public List<CatalogDataCustomOption> custom_options { get; set; }

        [JsonProperty("bundle_options", Required = Required.Default)]
        public List<BundleDataBundleOption> bundle_options { get; set; }

        [JsonProperty("downloadable_option", Required = Required.Default)]
        public DownloadableDataDownloadableOption downloadable_option { get; set; }

        [JsonProperty("configurable_item_options", Required = Required.Default)]
        public List<ConfigurableProductDataConfigurableItemOptionValue> configurable_item_options { get; set; }

    }

    public class VaultDataPaymentToken
    {

        [JsonProperty("entity_id", Required = Required.Default)]
        public int? entity_id { get; set; }

        [JsonProperty("customer_id", Required = Required.Default)]
        public int? customer_id { get; set; }

        [JsonProperty("public_hash", Required = Required.Always)]
        public string public_hash { get; set; }

        [JsonProperty("payment_method_code", Required = Required.Always)]
        public string payment_method_code { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string type { get; set; }

        [JsonProperty("created_at", Required = Required.Default)]
        public string created_at { get; set; }

        [JsonProperty("expires_at", Required = Required.Default)]
        public string expires_at { get; set; }

        [JsonProperty("gateway_token", Required = Required.Always)]
        public string gateway_token { get; set; }

        [JsonProperty("token_details", Required = Required.Always)]
        public string token_details { get; set; }

        [JsonProperty("is_active", Required = Required.Always)]
        public bool is_active { get; set; }

        [JsonProperty("is_visible", Required = Required.Always)]
        public bool is_visible { get; set; }
    }

    public class SalesDataShipping
    {

        [JsonProperty("address", Required = Required.Default)]
        public SalesDataOrderAddress address { get; set; }

        [JsonProperty("method", Required = Required.Default)]
        public string method { get; set; }

        [JsonProperty("total", Required = Required.Default)]
        public SalesDataTotal total { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataShippingExtension extension_attributes { get; set; }
    }

    public class SalesDataShippingAssignmentExtension { }

    public class GiftMessageDataMessageExtension
    {
        [JsonProperty("entity_id", Required = Required.Default)]
        public string entity_id { get; set; }

        [JsonProperty("entity_type", Required = Required.Default)]
        public string entity_type { get; set; }
    }

    public class TaxDataOrderTaxDetailsAppliedTaxExtension
    {
        [JsonProperty("rates", Required = Required.Default)]
        public List<TaxDataAppliedTaxRate> rates { get; set; }
    }

    public class TaxDataOrderTaxDetailsItemExtension { }

    public class CatalogDataCustomOption
    {

        [JsonProperty("option_id", Required = Required.Always)]
        public string option_id { get; set; }

        [JsonProperty("option_value", Required = Required.Always)]
        public string option_value { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public CatalogDataCustomOptionExtension extension_attributes { get; set; }
    }

    public class BundleDataBundleOption
    {

        [JsonProperty("option_id", Required = Required.Always)]
        public int option_id { get; set; }

        [JsonProperty("option_qty", Required = Required.Always)]
        public int option_qty { get; set; }

        [JsonProperty("option_selections", Required = Required.Always)]
        public List<int> option_selections { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public BundleDataBundleOptionExtension extension_attributes { get; set; }
    }

    public class DownloadableDataDownloadableOption
    {

        [JsonProperty("downloadable_links", Required = Required.Always)]
        public List<int> downloadable_links { get; set; }
    }

    public class ConfigurableProductDataConfigurableItemOptionValue
    {
        /// <summary>
        /// Option SKU
        /// </summary>
        [JsonProperty("option_id")]
        public string option_id { get; set; }

        [JsonProperty("option_value", Required = Required.Default)]
        public int? option_value { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public ConfigurableProductDataConfigurableItemOptionValueExtension extension_attributes { get; set; }
    }

    public class SalesDataTotal
    {
        [JsonProperty("base_shipping_amount", Required = Required.Default)]
        public decimal? base_shipping_amount { get; set; }

        [JsonProperty("base_shipping_canceled", Required = Required.Default)]
        public decimal? base_shipping_canceled { get; set; }

        [JsonProperty("base_shipping_discount_amount", Required = Required.Default)]
        public decimal? base_shipping_discount_amount { get; set; }

        [JsonProperty("base_shipping_discount_tax_compensation_amnt", Required = Required.Default)]
        public decimal? base_shipping_discount_tax_compensation_amnt { get; set; }

        [JsonProperty("base_shipping_incl_tax", Required = Required.Default)]
        public decimal? base_shipping_incl_tax { get; set; }

        [JsonProperty("base_shipping_invoiced", Required = Required.Default)]
        public decimal? base_shipping_invoiced { get; set; }

        [JsonProperty("base_shipping_refunded", Required = Required.Default)]
        public decimal? base_shipping_refunded { get; set; }

        [JsonProperty("base_shipping_tax_amount", Required = Required.Default)]
        public decimal? base_shipping_tax_amount { get; set; }

        [JsonProperty("base_shipping_tax_refunded", Required = Required.Default)]
        public decimal? base_shipping_tax_refunded { get; set; }

        [JsonProperty("shipping_amount", Required = Required.Default)]
        public decimal? shipping_amount { get; set; }

        [JsonProperty("shipping_canceled", Required = Required.Default)]
        public decimal? shipping_canceled { get; set; }

        [JsonProperty("shipping_discount_amount", Required = Required.Default)]
        public int? shipping_discount_amount { get; set; }

        [JsonProperty("shipping_discount_tax_compensation_amount", Required = Required.Default)]
        public decimal? shipping_discount_tax_compensation_amount { get; set; }

        [JsonProperty("shipping_incl_tax", Required = Required.Default)]
        public decimal? shipping_incl_tax { get; set; }

        [JsonProperty("shipping_invoiced", Required = Required.Default)]
        public decimal? shipping_invoiced { get; set; }

        [JsonProperty("shipping_refunded", Required = Required.Default)]
        public decimal? shipping_refunded { get; set; }

        [JsonProperty("shipping_tax_amount", Required = Required.Default)]
        public decimal? shipping_tax_amount { get; set; }

        [JsonProperty("shipping_tax_refunded", Required = Required.Default)]
        public decimal? shipping_tax_refunded { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public SalesDataTotalExtension extension_attributes { get; set; }

    }

    public class SalesDataShippingExtension { }

    public class TaxDataAppliedTaxRate
    {

        [JsonProperty("code", Required = Required.Default)]
        public string code { get; set; }

        [JsonProperty("title", Required = Required.Default)]
        public string title { get; set; }

        [JsonProperty("percent", Required = Required.Default)]
        public decimal? percent { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public TaxDataAppliedTaxRateExtension extension_attributes { get; set; }
    }

    public class CatalogDataCustomOptionExtension
    {

        [JsonProperty("file_info", Required = Required.Default)]
        public FrameworkDataImageContent file_info { get; set; }
    }

    public class BundleDataBundleOptionExtension { }

    public class ConfigurableProductDataConfigurableItemOptionValueExtension { }

    public class SalesDataTotalExtension { }

    public class TaxDataAppliedTaxRateExtension { }

    public class FrameworkDataImageContent
    {
        [JsonProperty("base64_encoded_data", Required = Required.Always)]
        public string base64_encoded_data { get; set; }

        [JsonProperty("type ", Required = Required.Always)]
        public string type { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string name { get; set; }
    }

    public class CatalogDataProduct
    {
        [JsonProperty("id", Required = Required.Default)]
        public int? id { get; set; }

        [JsonProperty("sku", Required = Required.Always)]
        public string sku { get; set; }

        [JsonProperty("name", Required = Required.Default)]
        public string name { get; set; }

        [JsonProperty("attribute_set_id", Required = Required.Default)]
        public int? attribute_set_id { get; set; }

        [JsonProperty("price", Required = Required.Default)]
        public decimal? price { get; set; }

        [JsonProperty("status", Required = Required.Default)]
        public int? status { get; set; }

        [JsonProperty("visibility", Required = Required.Default)]
        public int? visibility { get; set; }

        [JsonProperty("file_info", Required = Required.Default)]
        public string type_id { get; set; }

        [JsonProperty("created_at", Required = Required.Default)]
        public string created_at { get; set; }

        [JsonProperty("updated_at", Required = Required.Default)]
        public string updated_at { get; set; }

        [JsonProperty("weight", Required = Required.Default)]
        public decimal? weight { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public CatalogDataProductExtension extension_attributes { get; set; }

        [JsonProperty("product_links", Required = Required.Default)]
        public List<CatalogDataProductLink> product_links { get; set; }

        [JsonProperty("options", Required = Required.Default)]
        public List<CatalogDataProductCustomOption> options { get; set; }

        [JsonProperty("media_gallery_entries", Required = Required.Default)]
        public List<CatalogDataProductAttributeMediaGalleryEntry> media_gallery_entries { get; set; }

        [JsonProperty("tier_prices", Required = Required.Default)]
        public List<CatalogDataProductTierPrice> tier_prices { get; set; }

        [JsonProperty("custom_attributes", Required = Required.Default)]
        public List<FrameworkAttribute> custom_attributes { get; set; }
    }

    public class CatalogDataProductExtension
    {
        [JsonProperty("bundle_product_options", Required = Required.Default)]
        public List<BundleDataOption> bundle_product_options { get; set; }

        [JsonProperty("downloadable_products_links", Required = Required.Default)]
        public List<DownloadableDataLink> downloadable_products_links { get; set; }

        [JsonProperty("downloadable_product_samples", Required = Required.Default)]
        public List<DownloadableDataSample> downloadable_product_samples { get; set; }

        [JsonProperty("stock_item", Required = Required.Default)]
        public CatalogInventoryDataStockItem stock_item { get; set; }

        [JsonProperty("configurable_product_options", Required = Required.Default)]
        public List<ConfigurableProductDataOption> configurable_product_options { get; set; }

        [JsonProperty("configurable_product_links", Required = Required.Default)]
        public List<int> configurable_product_links { get; set; }
    }

    public class CatalogDataProductLink
    {
        [JsonProperty("sku", Required = Required.Always)]
        public string sku { get; set; }

        [JsonProperty("link_type", Required = Required.Always)]
        public string link_type { get; set; }

        [JsonProperty("linked_product_sku", Required = Required.Always)]
        public string linked_product_sku { get; set; }

        [JsonProperty("linked_product_type", Required = Required.Always)]
        public string linked_product_type { get; set; }

        [JsonProperty("position", Required = Required.Always)]
        public int position { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public CatalogDataProductLinkExtension extension_attributes { get; set; }
    }

    public class CatalogDataProductCustomOption
    {
        [JsonProperty("product_sku", Required = Required.Always)]
        public string product_sku { get; set; }

        [JsonProperty("option_id", Required = Required.Default)]
        public int? option_id { get; set; }

        [JsonProperty("title", Required = Required.Always)]
        public string title { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string type { get; set; }

        [JsonProperty("sort_order", Required = Required.Always)]
        public int sort_order { get; set; }

        [JsonProperty("is_require", Required = Required.Always)]
        public bool is_require { get; set; }

        [JsonProperty("price", Required = Required.Default)]
        public decimal? price { get; set; }

        [JsonProperty("price_type", Required = Required.Default)]
        public string price_type { get; set; }

        [JsonProperty("sku", Required = Required.Default)]
        public string sku { get; set; }

        [JsonProperty("file_extension", Required = Required.Default)]
        public string file_extension { get; set; }

        [JsonProperty("max_characters", Required = Required.Default)]
        public int? max_characters { get; set; }

        [JsonProperty("image_size_x", Required = Required.Default)]
        public int? image_size_x { get; set; }

        [JsonProperty("image_size_y", Required = Required.Default)]
        public int? image_size_y { get; set; }

        [JsonProperty("values", Required = Required.Default)]
        public List<CatalogDataProductCustomOptionValues> values { get; set; }

        [JsonProperty("extension_values", Required = Required.Default)]
        public CatalogDataProductCustomOptionExtension extension_values { get; set; }

    }

    public class CatalogDataProductAttributeMediaGalleryEntry
    {
        [JsonProperty("id", Required = Required.Default)]
        public int? id { get; set; }

        [JsonProperty("media_type", Required = Required.Always)]
        public string media_type { get; set; }

        [JsonProperty("label", Required = Required.Always)]
        public string label { get; set; }

        [JsonProperty("position", Required = Required.Always)]
        public int position { get; set; }

        [JsonProperty("disabled", Required = Required.Always)]
        public bool disabled { get; set; }

        [JsonProperty("types", Required = Required.Always)]
        public List<string> types { get; set; }

        [JsonProperty("file", Required = Required.Default)]
        public string file { get; set; }

        [JsonProperty("content", Required = Required.Default)]
        public FrameworkDataImageContent content { get; set; }

        [JsonProperty("extension_attributes", Required = Required.Default)]
        public CatalogDataProductAttributeMediaGalleryEntryExtension extension_attributes { get; set; }
    }

    public class CatalogDataProductTierPrice
    {
        public int customer_group_id { get; set; }
        public decimal qty { get; set; }
        public decimal value { get; set; }
        public CatalogDataProductTierPriceExtension extension_attributes { get; set; }
    }

    public class FrameworkAttribute
    {
        public string attribute_code { get; set; }
        public object value { get; set; }
    }

    public class BundleDataOption
    {
        public int option_id { get; set; }
        public string title { get; set; }
        public bool required { get; set; }
        public int position { get; set; }
        public string sku { get; set; }
        public List<BundleDataLink> product_links { get; set; }
        public BundleDataOptionExtension extension_attributes { get; set; }
    }

    public class DownloadableDataLink
    {
        public int id { get; set; }
        public string title { get; set; }
        public int sort_order { get; set; }
        public int is_shareable { get; set; }
        public decimal price { get; set; }
        public int number_of_downloads { get; set; }
        public string link_type { get; set; }
        public string link_file { get; set; }
        public DownloadableDataFileContent link_file_content { get; set; }
        public string link_url { get; set; }
        public string sample_type { get; set; }
        public string sample_file { get; set; }
        public string sample_file_content { get; set; }
        public string sample_url { get; set; }
        public DownloadableDataLinkExtension extension_attributes { get; set; }
    }

    public class DownloadableDataSample
    {
        public int id { get; set; }
        public string title { get; set; }
        public int sort_order { get; set; }
        public string sample_type { get; set; }
        public string sample_file { get; set; }
        public DownloadableDataFileContent sample_file_content { get; set; }
        public string sample_url { get; set; }
        public DownloadableDataSampleExtension extension_attributes { get; set; }
    }

    public class CatalogInventoryDataStockItem
    {
        public int item_id { get; set; }
        public int product_id { get; set; }
        public int stock_id { get; set; }
        public decimal qty { get; set; }
        public bool is_in_stock { get; set; }
        public bool is_qty_decimal { get; set; }
        public bool show_default_notification_message { get; set; }
        public bool use_config_min_qty { get; set; }
        public decimal min_qty { get; set; }
        public int use_config_min_sale_qty { get; set; }
        public decimal min_sale_qty { get; set; }
        public bool use_config_max_sale_qty { get; set; }
        public decimal max_sale_qty { get; set; }
        public bool use_config_backorders { get; set; }
        public int backorders { get; set; }
        public bool use_config_notify_stock_qty { get; set; }
        public decimal notify_stock_qty { get; set; }
        public bool use_config_qty_increments { get; set; }
        public bool use_config_enable_qty_inc { get; set; }
        public bool enable_qty_increments { get; set; }
        public bool use_config_manage_stock { get; set; }
        public string low_stock_date { get; set; }
        public bool is_decimal_divided { get; set; }
        public int stock_status_changed_auto { get; set; }
        public CatalogInventoryDataStockItemExtension extension_attributes
        {
            get; set;
        }
    }

    public class ConfigurableProductDataOption
    {
        public int id { get; set; }
        public string attribute_id { get; set; }
        public string label { get; set; }
        public int position { get; set; }
        public bool is_use_default { get; set; }
        public List<ConfigurableProductDataOptionValue> values { get; set; }
        public ConfigurableProductDataOptionExtension extension_attributes { get; set; }
        public int product_id { get; set; }
    }

    public class CatalogDataProductLinkExtension
    {
        public decimal qty { get; set; }
    }
    public class CatalogDataProductCustomOptionValues
    {
        public string title { get; set; }
        public int sort_order { get; set; }
        public decimal price { get; set; }
        public string price_type { get; set; }
        public string sku { get; set; }
        public int option_type_id { get; set; }
    }

    public class CatalogDataProductCustomOptionExtension
    {

    }
    public class CatalogDataProductAttributeMediaGalleryEntryExtension
    {
        public FrameworkDataVideoContent video_content { get; set; }
    }

    public class CatalogDataProductTierPriceExtension { }

    public class BundleDataLink
    {
        public string id { get; set; }
        public string sku { get; set; }
        public int option_id { get; set; }
        public decimal qty { get; set; }
        public int position { get; set; }
        public bool is_default { get; set; }
        public decimal price { get; set; }
        public int price_type { get; set; }
        public int can_change_quantity { get; set; }
        public BundleDataLinkExtension extension_attributes { get; set; }
    }

    public class BundleDataOptionExtension { }

    public class DownloadableDataFileContent
    {
        public string file_data { get; set; }
        public string name { get; set; }

        public DownloadableDataFileContentExtension extension_attributes { get; set; }
    }

    public class DownloadableDataLinkExtension { }
    public class DownloadableDataSampleExtension { }
    public class CatalogInventoryDataStockItemExtension { }

    public class ConfigurableProductDataOptionValue
    {
        public int value_index { get; set; }
        public ConfigurableProductDataOptionValueExtension extension_attributes { get; set; }
    }
    public class ConfigurableProductDataOptionExtension { }
    public class FrameworkDataVideoContent
    {
        public string media_type { get; set; }
        public string video_provider { get; set; }
        public string video_url { get; set; }
        public string video_title { get; set; }
        public string video_description { get; set; }
        public string video_metadata { get; set; }

    }

    public class BundleDataLinkExtension { }
    public class DownloadableDataFileContentExtension { }
    public class ConfigurableProductDataOptionValueExtension { }

}