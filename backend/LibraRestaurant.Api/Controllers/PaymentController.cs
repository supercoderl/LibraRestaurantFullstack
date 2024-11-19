using LibraRestaurant.Api.Models;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Services;
using LibraRestaurant.Application.ViewModels.Payments;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Stripe.Checkout;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class PaymentController : ApiController
    {
        private readonly IPaypalService _paypalService;
        private readonly IVnPayService _vnPayService;
        private readonly IStripeService _stripeService;
        private readonly IPayOsService _payOsService;

        public PaymentController(
            INotificationHandler<DomainNotification> notifications,
            IPaypalService paypalService,
            IVnPayService vnPayService,
            IStripeService stripeService,
            IPayOsService payOsService) : base(notifications)
        {
            _paypalService = paypalService;
            _vnPayService = vnPayService;
            _stripeService = stripeService;
            _payOsService = payOsService;
        }

        [HttpPost("Paypal")]
        [SwaggerOperation("Create order")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CreateOrderResponse>))]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest viewModel)
        {
            return Response(await _paypalService.CreateOrder(viewModel));
        }

        [HttpGet("Paypal/capture/{id}")]
        [SwaggerOperation("Get capture order")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CaptureOrderResponse>))]
        public async Task<IActionResult> GetPlanDetails([FromRoute] string id)
        {
            return Response(await _paypalService.CaptureOrder(id));
        }

        [HttpGet("Paypal/transactions")]
        [SwaggerOperation("Get transactions")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> GetTransactions()
        {
            return Response(await _paypalService.GetTransactions());
        }

        [HttpPost("VNPay")]
        [SwaggerOperation("Pay")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> PayVnPay([FromBody] CreateVNPayViewModel viewModel)
        {
            return Response(await _vnPayService.Pay(viewModel));
        }

        [HttpPost("Stripe")]
        [SwaggerOperation("Pay")]
        [SwaggerResponse(200, "Request successful")]
        public async Task<IActionResult> PayStripe(SessionStripe request)
        {
            return Response(await _stripeService.CreateOrderStripe(request));
        }

/*        [HttpGet("Stripe/{id}")]
        [SwaggerOperation("Retrieve session")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Session>))]
        public async Task<IActionResult> RetrieveStripe([FromRoute] string id)
        {
            return Response(await _stripeService.RetrieveSession(id));
        }*/

        [HttpPost("PayOS")]
        [SwaggerOperation("Pay")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CreatePaymentResult>))]
        public async Task<IActionResult> PayOs(CreatePayOSViewModel request)
        {
            return Response(await _payOsService.CreateOrderPayOS(request));
        }

        [HttpPost("PayOS/{id}")]
        [SwaggerOperation("Cancel")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PaymentLinkInformation>))]
        public async Task<IActionResult> RetrieveStripe([FromRoute] long id)
        {
            return Response(await _payOsService.CancelOrder(id));
        }
    }
}
