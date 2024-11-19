using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.Payments
{
    public class OrderInfo
    {
        public long OrderId { get; set; }
        public long Amount { get; set; }
        public string OrderDesc { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public long PaymentTranId { get; set; }
        public string BankCode { get; set; } = string.Empty;
        public string PayStatus { get; set; } = string.Empty;
    }

    public sealed record CreateVNPayViewModel(
        bool IsQR,
        bool IsVNBank,
        bool IsIntCard,
        string? Locale,
        double Amount,
        Guid OrderId,
        Guid TransactionId,
        string Status,
        int PaymentMethodId,
        DateTime? CreatedDate
    );
}
