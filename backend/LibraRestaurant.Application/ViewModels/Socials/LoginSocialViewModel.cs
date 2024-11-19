using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Socials
{
    public class LoginGoogleViewModel
    {
        public string? Provider { get; set; }
        public string? IdToken { get; set; }
    }
}
