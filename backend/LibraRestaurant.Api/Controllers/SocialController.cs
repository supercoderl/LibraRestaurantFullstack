
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using LibraRestaurant.Application.ViewModels.Socials;
using LibraRestaurant.Api.Models;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class SocialController : ApiController
    {
        private readonly ISocialService _socialService;

        public SocialController(
            ISocialService socialService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _socialService = socialService;
        }

        [HttpPost("google")]
        [AllowAnonymous]
        [SwaggerOperation("Login by google")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Object>))]
        public async Task<IActionResult> GoogleLogin([FromBody] LoginGoogleViewModel viewModel)
        {
            var token = await _socialService.LoginByGoogle(viewModel);
            return Response(token);
        }
    }
}
