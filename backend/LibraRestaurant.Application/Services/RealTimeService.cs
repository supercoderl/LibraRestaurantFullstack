using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Settings;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Services
{
    public class RealTimeService : IRealTimeService
    {
        private readonly IOptions<CoreSettings> _options;

        public RealTimeService(IOptions<CoreSettings> options)
        {
            _options = options;
        }

        public async Task Push<THubMethodType>(THubMethodType method, JsonObject data, string hubUrl)
            where THubMethodType : Enum
        {
            var hubConnectionBuilder = new HubConnectionBuilder()
                .WithUrl($"{_options.Value.ServerUrl}/{hubUrl}")
                .Build();

            try
            {
                if (hubConnectionBuilder.State != HubConnectionState.Connected)
                {
                    // Yöntemleri çağırmadan önce hub bağlantısının başlatıldığından emin olun
                    await hubConnectionBuilder.StartAsync();
                }

                await hubConnectionBuilder.InvokeAsync(method.ToString(), data);
            }
            catch (Exception ex)
            {
                // Hataları uygun şekilde günlüğe kaydedin veya işleyin
                Console.WriteLine($"An error occurred in the push method: {ex.Message}");
                throw;
            }
        }

        public async Task Ping<THubMethodType>(THubMethodType method, string hubUrl)
            where THubMethodType : Enum
        {
            var hubConnectionBuilder = new HubConnectionBuilder()
                .WithUrl($"{_options.Value.ServerUrl}/{hubUrl}")
                .Build();

            try
            {
                if (hubConnectionBuilder.State != HubConnectionState.Connected)
                {
                    // Yöntemleri çağırmadan önce hub bağlantısının başlatıldığından emin olun
                    await hubConnectionBuilder.StartAsync();
                }

                await hubConnectionBuilder.InvokeAsync(method.ToString());
            }
            catch (Exception ex)
            {
                // Hataları uygun şekilde günlüğe kaydedin veya işleyin
                Console.WriteLine($"An error occurred in the ping method: {ex.Message}");
                throw;
            }
        }
    }
}
