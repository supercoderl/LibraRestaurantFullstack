using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.Wards;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class AddressController : ApiController
    {
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;

        public AddressController(
            INotificationHandler<DomainNotification> notifications,
            ICityService cityService,
            IDistrictService districtService,
            IWardService wardService) : base(notifications)
        {
            _cityService = cityService;
            _districtService = districtService;
            _wardService = wardService;
        }

        [HttpGet("city")]
        [SwaggerOperation("Get a list of all cities")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CityViewModel>>))]
        public async Task<IActionResult> GetAllCitiesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool isAll = false,
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<CityViewModelSortProvider, CityViewModel, City>]
        SortQuery? sortQuery = null)
        {
            var cities = await _cityService.GetAllCitiesAsync(
                query,
                includeDeleted,
                isAll,
                searchTerm,
                sortQuery);
            return Response(cities);
        }

        [HttpGet("{id}/city")]
        [SwaggerOperation("Get a city by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CityViewModel>))]
        public async Task<IActionResult> GetCityByIdAsync([FromRoute] int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            return Response(city);
        }

        [HttpGet("district")]
        [SwaggerOperation("Get a list of all districts")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DistrictViewModel>>))]
        public async Task<IActionResult> GetAllDistrictsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] bool isAll = false,
            [FromQuery] [SortableFieldsAttribute<DistrictViewModelSortProvider, DistrictViewModel, District>]
        SortQuery? sortQuery = null)
        {
            var districts = await _districtService.GetAllDistrictsAsync(
                query,
                includeDeleted,
                isAll,
                searchTerm,
                sortQuery);
            return Response(districts);
        }

        [HttpGet("{id}/district")]
        [SwaggerOperation("Get a district by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DistrictViewModel>))]
        public async Task<IActionResult> GetDistrictByIdAsync([FromRoute] int id)
        {
            var district = await _districtService.GetDistrictByIdAsync(id);
            return Response(district);
        }

        [HttpGet("ward")]
        [SwaggerOperation("Get a list of all wards")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<WardViewModel>>))]
        public async Task<IActionResult> GetAllWardsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] bool isAll = false,
            [FromQuery] [SortableFieldsAttribute<WardViewModelSortProvider, WardViewModel, Ward>]
        SortQuery? sortQuery = null)
        {
            var wards = await _wardService.GetAllWardsAsync(
                query,
                includeDeleted,
                isAll,
                searchTerm,
                sortQuery);
            return Response(wards);
        }

        [HttpGet("{id}/ward")]
        [SwaggerOperation("Get a ward by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<WardViewModel>))]
        public async Task<IActionResult> GetWardByIdAsync([FromRoute] int id)
        {
            var ward = await _wardService.GetWardByIdAsync(id);
            return Response(ward);
        }
    }
}
