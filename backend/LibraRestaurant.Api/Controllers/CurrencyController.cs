using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class CurrencyController : ApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(
            INotificationHandler<DomainNotification> notifications,
            ICurrencyService currencyService) : base(notifications)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all currencies")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CurrencyViewModel>>))]
        public async Task<IActionResult> GetAllCurrenciesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<CurrencyViewModelSortProvider, CurrencyViewModel, Currency>]
        SortQuery? sortQuery = null)
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(currencies);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a currency by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CurrencyViewModel>))]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            return Response(currency);
        }

        [HttpPost]
        [SwaggerOperation("Create a new currency")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateCurrencyViewModel viewModel)
        {
            var currencyId = await _currencyService.CreateCurrencyAsync(viewModel);
            return Response(currencyId);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a currency")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteCurrencyAsync([FromRoute] int id)
        {
            await _currencyService.DeleteCurrencyAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a currency")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCurrencyViewModel>))]
        public async Task<IActionResult> UpdateCurrencyAsync([FromBody] UpdateCurrencyViewModel viewModel)
        {
            await _currencyService.UpdateCurrencyAsync(viewModel);
            return Response(viewModel);
        }
    }
}
