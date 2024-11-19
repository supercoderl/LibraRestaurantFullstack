using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class OrderStatusHistory : Entity
    {
        public int OrderStatusHistoryId { get; private set; }   
        public Guid OrderId { get; private set; }
        public int OrderStatusTypeId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public OrderStatusHistory(
            int orderStatusHistoryId,
            Guid orderId,
            int orderStatusTypeId,
            DateTime startTime,
            DateTime? endTime
        ) : base (orderStatusHistoryId)
        {
            OrderStatusHistoryId = orderStatusHistoryId;
            OrderId = orderId;
            OrderStatusTypeId = orderStatusTypeId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void SetOrderId( Guid orderId )
        {
            OrderId = orderId;
        }

        public void SetOrderStatusTypeId( int orderStatusTypeId )
        {
            OrderStatusTypeId = orderStatusTypeId;
        }

        public void SetStartTime( DateTime startTime )
        {
            StartTime = startTime;
        }

        public void SetEndTime( DateTime? endTime )
        {
            EndTime = endTime;
        }
    }
}
