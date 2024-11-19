using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class PaypalConfig
    {
        public string Mode { get; set; }
        public string ClientID { get; set; }
        public string Secret { get; set; }

        public PaypalConfig(string mode, string clientID, string secret)
        {
            Mode = mode;
            ClientID = clientID;
            Secret = secret;
        }
    }
}
