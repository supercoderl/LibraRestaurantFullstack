using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Dashboards;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class DashboardController : ApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            INotificationHandler<DomainNotification> notifications,
            IDashboardService dashboardService) : base(notifications)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [SwaggerOperation("Get a dashboard data")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DashboardViewModel>))]
        public async Task<IActionResult> Analytic()
        {
            var result = await _dashboardService.Analytic();
            return Response(result);
        }
    }
}
