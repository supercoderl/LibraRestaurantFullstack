using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Invoice : Entity
    {
        public int InvoiceId { get; private set; }
        public string InvoiceNo { get; private set; }
        public Guid PaymentId { get; private set; }
        public double Subtotal { get; private set; }
        public double Tax { get; private set; }
        public double Total { get; private set; }
        public DateTime InvoiceTime { get; private set; }
        public bool IsCanceled { get; private set; }
        public DateTime? CanceledTime { get; private set; }
        public string? CanceledReason { get; private set; }

        [InverseProperty("Invoice")]
        public virtual ICollection<Discount>? Discounts { get; set; } = new List<Discount>();

        public Invoice(
            int invoiceId,
            string invoiceNo,
            Guid paymentId,
            double subtotal,
            double tax,
            double total,
            DateTime invoiceTime,
            bool isCanceled,
            DateTime? canceledTime,
            string? canceledReason
        ) : base (invoiceId)
        {
            InvoiceId = invoiceId;
            InvoiceNo = invoiceNo;
            PaymentId = paymentId;
            Subtotal = subtotal;
            Tax = tax;
            Total = total;
            InvoiceTime = invoiceTime;
            IsCanceled = isCanceled;
            CanceledTime = canceledTime;
            CanceledReason = canceledReason;
        }

        public void SetInvoiceNo( string invoiceNo )
        {
            InvoiceNo = invoiceNo;
        }

        public void SetPaymentId( Guid paymentId )
        {
            PaymentId = paymentId;
        }

        public void SetSubtotal( double subtotal )
        {
            Subtotal = subtotal;
        }

        public void SetTax( double tax )
        {
            Tax = tax;
        }

        public void SetTotal( double total )
        {
            Total = total;
        }

        public void SetInvoiceTime( DateTime invoiceTime )
        {
            InvoiceTime = invoiceTime;
        }

        public void SetIsCanceled( bool isCanceled )
        {
            IsCanceled = isCanceled;
        }

        public void SetCanceledTime( DateTime? canceledTime )
        {
            CanceledTime = canceledTime;
        }

        public void SetCanceledReason( string? reason )
        {
            CanceledReason = reason;
        }
    }
}
