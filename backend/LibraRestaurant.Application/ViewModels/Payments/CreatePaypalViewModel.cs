using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class CreatePlanRequest
    {
        public string product_id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public List<BillingCycle> billing_cycles { get; set; } = new List<BillingCycle>();
        public PaymentPreferences payment_preferences { get; set; } = new PaymentPreferences();
        public Taxes taxes { get; set; } = new Taxes();
    }

    public class CreateSubscriptionRequest
    {
        public string plan_id { get; set; } = string.Empty;
        public string quantity { get; set; } = string.Empty;
        public Amount shipping_amount { get; set; } = new Amount();
        public Subscriber subscriber { get; set; } = new Subscriber();
        public ApplicationContext application_context { get; set; } = new ApplicationContext();
    }

    public class RefundRequest
    {
        public Amount amount { get; set; } = new Amount();
        public string invoice_number { get; set; } = string.Empty;
    }

    public class Amount
    {
        public string currency_code { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
    }

    public class SubscriptionStatusChangeRequest
    {
        public string reason { get; set; } = string.Empty;
    }

    public class OrderRequest
    {
        public string intent { get; set; } = string.Empty;
        public List<PurchaseUnit> purchase_units { get; set; } = new List<PurchaseUnit>();
    }

    public sealed record CreateOrderRequest(
        Guid Reference,
        string Currency,
        double Value,
        int PaymentMethodId,
        Guid OrderId
    );
}
