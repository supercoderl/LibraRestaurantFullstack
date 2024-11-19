using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Payments;
using LibraRestaurant.Application.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;
using LibraNovel.Application.Libraries;
using Stripe.Checkout;
using Stripe;
using Net.payOS;
using Net.payOS.Types;

namespace LibraRestaurant.Application.Services
{
    public class PaypalService : IPaypalService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly PaypalConfig _paypalConfig;
        private readonly IOrderService _orderService;
        public string BaseURL => _paypalConfig.Mode == "Live"
            ? "https://api-m.paypal.com"
            : "https://api-m.sandbox.paypal.com";



        public PaypalService(IHttpContextAccessor httpContextAccessor, PaypalConfig paypalConfig, IOrderService orderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _paypalConfig = paypalConfig;
            _orderService = orderService;
        }

        private async Task<AuthorizationResponseData?> Authenticate()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_paypalConfig.ClientID}:{_paypalConfig.Secret}"));

            var content = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials")
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseURL}/v1/oauth2/token"),
                Method = HttpMethod.Post,
                Headers =
                {
                    { "Authorization", $"Basic {auth}" }
                },
                Content = new FormUrlEncodedContent(content)
            };

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(request);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<AuthorizationResponseData>(jsonResponse);

            return response;
        }

        public async Task<CreateOrderResponse?> CreateOrder(CreateOrderRequest viewModel)
        {
            var auth = await Authenticate();

            var request = new OrderRequest
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>
                {
                    new()
                    {
                        reference_id = viewModel.Reference.ToString(),
                        amount = new Amount
                        {
                            currency_code = viewModel.Currency,
                            value = viewModel.Value.ToString()
                        }
                    }
                }
            };

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth?.access_token}");

            var httpResponse = await httpClient.PostAsJsonAsync($"{BaseURL}/v2/checkout/orders", request);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);

            await _orderService.UpdatePaymentMethodAsync(viewModel.OrderId, viewModel.PaymentMethodId);

            return response;
        }

        public async Task<CaptureOrderResponse?> CaptureOrder(string orderId)
        {
            var auth = await Authenticate();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth?.access_token}");

            var httpContent = new StringContent("", Encoding.Default, "application/json");

            var httpResponse = await httpClient.PostAsync($"{BaseURL}/v2/checkout/orders/{orderId}/capture", httpContent);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse);

            return response;
        }

        public async Task<string> GetTransactions()
        {
            var auth = await Authenticate();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth?.access_token}");

            var httpContent = new StringContent("", Encoding.Default, "application/json");

            var httpResponse = await httpClient.GetAsync($"{BaseURL}/v1/reporting/transactions?fields=transaction_info,payer_info,shipping_info,auction_info,cart_info,incentive_info,store_info&start_date=2024-06-20T23:59:59.999Z&end_date=2024-07-20T00:00:00.000Z");

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse);

            return JsonSerializer.Serialize(response);
        }
    }

    public class VnPayService : IVnPayService
    {
        private readonly VNPayConfig _vNPayConfig;
        private readonly IOrderService _orderService;

        public VnPayService(VNPayConfig vNPayConfig, IOrderService orderService)
        {
            _vNPayConfig = vNPayConfig;
            _orderService = orderService;
        }

        public async Task<string> Pay(CreateVNPayViewModel request)
        {
            await Task.CompletedTask;

            //Get payment input
            OrderInfo order = new OrderInfo();
            order.OrderId = ConvertGuidToLong(request.TransactionId);
            order.Amount = long.Parse((request.Amount).ToString());
            order.Status = request.Status;
            order.CreatedDate = request.CreatedDate ?? DateTime.Now;
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _vNPayConfig.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", order.Amount.ToString());
            if (request.IsQR == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (request.IsVNBank == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (request.IsIntCard == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());

            vnpay.AddRequestData("vnp_Locale", request.Locale);

            vnpay.AddRequestData("vnp_OrderInfo", "Hoa don so_" + request.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", _vNPayConfig.vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(_vNPayConfig.vnp_Url, _vNPayConfig.vnp_HashSecret);
            await _orderService.UpdatePaymentMethodAsync(request.OrderId, request.PaymentMethodId);

            return paymentUrl;
        }

        private long ConvertGuidToLong(Guid value)
        {
            byte[] bytes = value.ToByteArray();
            long convertedLong = BitConverter.ToInt64(bytes, 0);
            return convertedLong;
        }
    }

    public class StripeService : IStripeService
    {
        private readonly SessionService _sessionService;
        private readonly StripeConfig _stripeConfig;
        private readonly IOrderService _orderService;

        public StripeService(StripeConfig stripeConfig, IOrderService orderService)
        {
            StripeConfiguration.ApiKey = stripeConfig.SecretKey;
            _sessionService = new SessionService();
            _stripeConfig = stripeConfig;
            _orderService = orderService;
        }

        public async Task<Session> CreateOrderStripe(SessionStripe request)
        {
            await Task.CompletedTask;
            string param = $"?stripe_order={request.OrderId}&stripe_amount={request.Amount}&stripe_status=00&stripe_transaction={request.TransactionId}&stripe_date={DateTime.Now.ToString("yyyyMMddHHmmss")}";

            var options = new SessionCreateOptions
            {
                SuccessUrl = string.Concat(_stripeConfig.SuccessURL, param),
                CancelUrl = _stripeConfig.CancelURL,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = request.Amount / 10,
                            Currency = request.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = request.Name,
                                Description = request.Description
                            }
                        },
                        Quantity = request.Quantity
                    },
                },
                Mode = request.Mode,
                CustomerEmail = request.CustomerEmail
            };

            var session = _sessionService.Create(options);

            await _orderService.UpdatePaymentMethodAsync(request.OrderId, request.PaymentMethodId);

            return session;
        }

        public async Task<Session> RetrieveSession(string id)
        {
            await Task.CompletedTask;

            var result = _sessionService.Get(id);

            return result;
        }
    }

    public class PayOsService : IPayOsService
    {
        private readonly PayOS _payOS;
        private readonly PayOSConfig _payOSConfig;
        private readonly IOrderService _orderService;

        public PayOsService(PayOSConfig payOSConfig, IOrderService orderService)
        {
            _payOSConfig = payOSConfig;
            _orderService = orderService;
            _payOS = new PayOS(payOSConfig.ClientID, payOSConfig.ApiKey, payOSConfig.ChecksumKey);
        }

        public async Task<CreatePaymentResult> CreateOrderPayOS(CreatePayOSViewModel request)
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            ItemData item = new ItemData(request.ProductName, 1, request.Price);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            PaymentData paymentData = new PaymentData(
                orderCode,
                request.Price,
                request.Description,
                items,
                _payOSConfig.CancelURL,
                _payOSConfig.ReturnURL
            );

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
            await _orderService.UpdatePaymentMethodAsync(request.OrderId, request.PaymentMethodId);

            return createPayment;
        }

        public async Task<PaymentLinkInformation> CancelOrder(long orderID)
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(orderID);
            return paymentLinkInformation;
        }
    }
}


