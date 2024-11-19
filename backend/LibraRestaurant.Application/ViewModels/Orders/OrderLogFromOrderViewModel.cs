using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Orders
{
    public class OrderLogFromOrderViewModel
    {
        public int ItemId { get; set; }
        public string? QuantityChanges { get; set; }
        public string? TimeChanges { get; set; }
    }
}
