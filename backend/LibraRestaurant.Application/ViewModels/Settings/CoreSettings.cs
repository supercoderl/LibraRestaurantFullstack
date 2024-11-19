using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Settings
{
    public class CoreSettings
    {
        public string ServerUrl { get; set; }

        public CoreSettings(string serverUrl)
        {
            ServerUrl = serverUrl;
        }
    }
}
