using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class InvoiceLine : Entity
    {
        public int InvoiceLineId { get; private set; }
        public int InvoiceId { get; private set; }
        public Guid OrderLineId { get; private set; }
        public int LineNumber { get; private set; }
        public bool IsCanceled { get; private set; }
        public string? CanceledReason { get; private set; }
        public DateTime? CanceledTime { get; private set; }

        public InvoiceLine(
            int invoiceLineId,
            int invoiceId,
            Guid orderLineId,
            int lineNumber,
            bool isCanceled,
            string? canceledReason,
            DateTime? canceledTime
        ) : base(invoiceLineId)
        {
            InvoiceLineId = invoiceLineId;
            InvoiceId = invoiceId;
            OrderLineId = orderLineId;
            LineNumber = lineNumber;
            IsCanceled = isCanceled;
            CanceledReason = canceledReason;
            CanceledTime = canceledTime;
        }

        public void SetInvoiceId( int invoiceId )
        {
            InvoiceId = invoiceId;
        }

        public void SetOrderLineId( Guid orderLineId )
        {
            OrderLineId = orderLineId;
        }

        public void SetLineNumber( int lineNumber )
        {
            LineNumber = lineNumber;
        }

        public void SetIsCanceled( bool isCanceled )
        {
            IsCanceled = isCanceled;
        }

        public void SetCanceledReason( string? canceledReason )
        {
            CanceledReason = canceledReason;
        }

        public void SetCanceledTime( DateTime? canceledTime )
        {
            CanceledTime = canceledTime;
        }
    }
}
