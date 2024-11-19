using LibraRestaurant.Application.ViewModels.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IDashboardService
    {
        public Task<DashboardViewModel?> Analytic();
    }
}
