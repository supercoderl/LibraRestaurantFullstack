using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Employees
{
    public sealed record RefreshTokenViewModel(
      string RefreshToken
    );
}
