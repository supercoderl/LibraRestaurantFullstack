using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Socials;
using LibraRestaurant.Domain.Commands.Socials.LoginSocial;
using LibraRestaurant.Domain.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Services
{
    public class SocialService : ISocialService
    {
        private readonly IMediatorHandler _bus;

        public SocialService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Object> LoginByGoogle(LoginGoogleViewModel viewModel)
        {
            if(viewModel.Provider is null || viewModel.IdToken is null)
            {
                return string.Empty;
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={viewModel.IdToken}");
                if (response.IsSuccessStatusCode)
                {
                    var info = ExtractEmailFromJson(await response.Content.ReadAsStringAsync());
                    if(!string.IsNullOrEmpty(info))
                    {
                        return await _bus.QueryAsync(new LoginSocialCommand(NormalizeEmail(info)));
                    }
                }
            }

            return string.Empty;
        }

        public string? ExtractEmailFromJson(string? jsonResponse)
        {
            if (jsonResponse is null) return string.Empty;
            return JObject.Parse(jsonResponse)["email"]?.ToString();
        }

        private string NormalizeEmail(string email)
        {
            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                return email;
            }

            var localPart = parts[0].Replace(".", "");
            var domain = parts[1];

            return $"{localPart}@{domain}";
        }
    }
}
