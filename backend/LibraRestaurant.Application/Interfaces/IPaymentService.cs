using LibraRestaurant.Application.ViewModels.Payments;
using Net.payOS.Types;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IVnPayService
    {
        Task<string> Pay(CreateVNPayViewModel request);
    }

    public interface IPaypalService
    {
        Task<CreateOrderResponse?> CreateOrder(CreateOrderRequest request);
        Task<CaptureOrderResponse?> CaptureOrder(string orderId);
        Task<string> GetTransactions();
    }

    public interface IPayOsService
    {
        Task<CreatePaymentResult> CreateOrderPayOS(CreatePayOSViewModel request);
        Task<PaymentLinkInformation> CancelOrder(long orderID);
    }

    public interface IStripeService
    {
        Task<Session> CreateOrderStripe(SessionStripe request);
        Task<Session> RetrieveSession(string id);
    }
}
