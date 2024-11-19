using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class PayOSConfig
    {
        public string ClientID { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
        public string ReturnURL { get; set; }
        public string CancelURL { get; set; }

        public PayOSConfig(string clientID, string apiKey, string checksumKey, string returnURL, string cancelURL)
        {
            ClientID = clientID;
            ApiKey = apiKey;
            ChecksumKey = checksumKey;
            ReturnURL = returnURL;
            CancelURL = cancelURL;

        }
    }
}
