using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IRealTimeService
    {
        Task Push<THubMethodType>(THubMethodType method, JsonObject data, string hubUrl) where THubMethodType : Enum;

        Task Ping<THubMethodType>(THubMethodType method, string hubUrl) where THubMethodType : Enum;
    }
}
