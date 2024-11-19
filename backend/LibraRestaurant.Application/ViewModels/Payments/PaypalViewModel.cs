using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class AuthorizationResponseData
    {
        public string scope { get; set; } = string.Empty;
        public string access_token { get; set; } = string.Empty;
        public string token_type { get; set; } = string.Empty;
        public string app_id { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public List<string> supported_authn_schemes { get; set; } = new List<string>();
        public string nonce { get; set; } = string.Empty;
        public ClientMetaData client_metadata { get; set; } = new ClientMetaData();
    }

    public class CreatePlanResponse
    {
        public string id { get; set; } = string.Empty;
        public string product_id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<BillingCycle> billing_cycles { get; set; } = new List<BillingCycle>();
        public PaymentPreferences payment_preferences { get; set; } = new PaymentPreferences();
        public Taxes taxes { get; set; } = new Taxes();
        public DateTime create_time { get; set; } 
        public DateTime update_time { get; set; }
        public List<Link> links { get; set; } = new List<Link>();
    }

    public class CreateSubscriptionResponse
    {
        public string id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateTime status_update_time { get; set; }
        public string plan_id { get; set; } = string.Empty;
        public bool plan_overridden { get; set; }
        public DateTime start_time { get; set; }
        public string quantity { get; set; } = string.Empty;
        public Amount shipping_amount { get; set; } = new Amount();
        public Subscriber subscriber { get; set; } = new Subscriber();
        public DateTime create_time { get; set; }
        public List<Link> links { get; set; } = new List<Link>();
    }

    public class WebhookVerificationResponse
    {
        public string verification_status { get; set; } = string.Empty;
    }

    public sealed class CreateOrderResponse
    {
        public string id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<Link> links { get; set; } = new List<Link>();
    }

    public sealed class CaptureOrderResponse
    {
        public string id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public PaymentSource payment_source { get; set; } = new PaymentSource();
        public List<PurchaseUnit> purchase_units { get; set; } = new List<PurchaseUnit>();
        public Payer payer { get; set; } = new Payer();
        public List<Link> links { get; set; } = new List<Link>();
    }

    public class ClientMetaData
    {
        public string name { get; set; } = string.Empty;
        public string display_name { get; set; } = string.Empty;
        public string logo_uri { get; set; } = string.Empty;
        public List<string> scopes { get; set; } = new List<string>();
        public string ui_type { get; set; } = string.Empty;
    }

    public class BillingCycle
    {
        public Frequency frequency { get; set; } = new Frequency();
        public string tenure_type { get; set; } = string.Empty;
        public int sequence { get; set; }
        public int total_cycles { get; set; }
        public PricingScheme pricing_scheme { get; set; } = new PricingScheme();
    }

    public class Frequency
    {
        public string interval_unit { get; set; } = string.Empty;
        public int interval_count { get; set; }
    }

    public class PricingScheme
    {
        public FixedPrice fixed_price { get; set; } = new FixedPrice();
    }

    public class FixedPrice
    {
        public string value { get; set; } = string.Empty;
        public string currency_code { get; set; } = string.Empty;
    }

    public class PaymentPreferences
    {
        public bool auto_bill_outstanding { get; set; }
        public Amount setup_fee { get; set; } = new Amount();
        public string setup_fee_failure_action { get; set; } = string.Empty;
        public int payment_failure_threshold { get; set; } 
    }

    public class Taxes
    {
        public string percentage { get; set; } = string.Empty;
        public bool inclusive { get; set; }
    }

    public class Link
    {
        public string href { get; set; } = string.Empty;
        public string rel { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
    }

    public class Subscriber
    {
        public Name name { get; set; } = new Name();
        public string email_address { get; set; } = string.Empty;
        public ShippingAddress shipping_address { get; set; } = new ShippingAddress();
    }

    public class Name
    {
        public string given_name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
    }

    public class ShippingAddress
    {
        public Name name { get; set; } = new Name();
        public AddressLine address { get; set; } = new AddressLine();
    }

    public class AddressLine
    {
        public string line1 { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country_code { get; set; } = string.Empty;
        public string postal_code { get; set; } = string.Empty;
    }

    public class PaymentSource
    {
        public Paypal paypal { get; set; } = new Paypal();
    }

    public class Paypal
    {
        public Name name { get; set; } = new Name();
        public string email_address { get; set; } = string.Empty;
        public string account_id { get; set; } = string.Empty;
    }

    public class Payer
    {
        public Name name { get; set; } = new Name();
        public string email_address { get; set; } = string.Empty;
        public string payer_id { get; set; } = string.Empty;
    }

    public class PurchaseUnit
    {
        public Amount amount { get; set; } = new Amount();
        public string reference_id { get; set; } = string.Empty;
        public ShippingMethod? shipping { get; set; }
        public Payments? payments { get; set; }
    }

    public class ShippingMethod
    {
        public AddressLine address { get; set; } = new AddressLine();
        public string name { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
    }

    public class Payments
    {
        public List<Capture> captures { get; set; } = new List<Capture>();
    }

    public class ApplicationContext
    {
        public string brand_name { get; set; } = string.Empty;
        public string locale { get; set; } = string.Empty;
        public string shipping_preference { get; set; } = string.Empty;
        public string user_action { get; set; } = string.Empty;
        public PaymentMethod payment_method { get; set; } = new PaymentMethod();
        public string return_url { get; set; } = string.Empty;
        public string cancel_url { get; set; } = string.Empty;
    }

    public class PaymentMethod
    {
        public string payer_selected { get; set; } = string.Empty;
        public string payee_preferred { get; set; } = string.Empty;
    }

    public class Capture
    {
        public string id { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public Amount amount { get; set; } = new Amount();
        public SellerProtection seller_protection { get; set; } = new SellerProtection();
        public bool final_capture { get; set; }
        public string disbursement_mode { get; set; } = string.Empty;
        public SellerReceivableBreakdown seller_receivable_breakdown { get; set; } = new SellerReceivableBreakdown();
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<Link> links { get; set; } = new List<Link>();
    }

    public class SellerProtection
    {
        public string status { get; set; } = string.Empty;
        public List<string> dispute_categories { get; set; } = new List<string>();
    }

    public class SellerReceivableBreakdown
    {
        public Amount gross_amount { get; set; } = new Amount();
        public PaypalFee paypal_fee { get; set; } = new PaypalFee();
        public Amount net_amount { get; set; } = new Amount();
    }

    public class PaypalFee
    {
        public string currency_code { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
    }
}
