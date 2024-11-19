using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Dashboards
{
    public sealed class DashboardViewModel
    {
        public int OrderCount { get; set; }
        public double PaymentAmount { get; set; }
        public DashboardCustomer Customer { get; set; } = new DashboardCustomer();
        public List<Top5Items> Top5Items { get; set; } = new List<Top5Items>();
    }

    public sealed class DashboardCustomer
    {
        public int CustomerCountInThisMonth { get; set; }
        public int CustomerCountInLastMonth { get; set; }
        public double Percentage { get; set; }
        public List<Top5Customers> Top5Customers { get; set; } = new List<Top5Customers>();
    }

    public sealed class Top5Customers
    {
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
    }

    public sealed class Top5Items
    {
        public string? Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
