using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class VNPayConfig
    {
        public string vnp_Returnurl {  get; set; }
        public string vnp_Url { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_HashSecret { get; set; }

        public VNPayConfig(string returnURL, string url, string tmnCode, string hashSecret)
        {
            vnp_Returnurl = returnURL;
            vnp_Url = url;
            vnp_TmnCode = tmnCode;
            vnp_HashSecret = hashSecret;
        }
    }
}
