using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Orders
{
    public class OrderLineFromOrderViewModel
    {
        public int OrderLineId { get; set; }
        public Guid OrderId { get; set; }
        public int ItemId { get; set; }
        public string? FoodName { get; set; }
        public int Quantity { get; set; }
        public double FoodPrice { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledTime { get; set; }
        public string? CanceledReason { get; set; }
        public string? CustomerReview { get; set; }
        public CustomerLikeStatus CustomerLike { get; set; }
    }
}
