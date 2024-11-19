using LibraRestaurant.Application.ViewModels.Socials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ISocialService
    {
        public Task<Object> LoginByGoogle(LoginGoogleViewModel viewModel);
    }
}
