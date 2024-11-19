using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class OrderLog : Entity
    {
        public int LogId { get; set; }
        public Guid OrderId { get; set; }
        public int ItemId { get; set; }
        public string ChangeType { get; set; }
        public int PreviousQuantity { get; set; }
        public int NewQuantity { get; set; }
        public DateTime Time {  get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderLogs")]
        public virtual OrderHeader? OrderHeader { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("OrderLogs")]
        public virtual MenuItem? Item { get; set; }

        public OrderLog(
            int logId,
            Guid orderId,
            int itemId,
            string changeType,
            int previousQuantity,
            int newQuantity,
            DateTime time
        ) : base(logId)
        {
            LogId = logId;
            OrderId = orderId;
            ItemId = itemId;
            ChangeType = changeType;
            PreviousQuantity = previousQuantity;
            NewQuantity = newQuantity;
            Time = time;
        }

        public void SetOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public void SetItem(int itemId) 
        { 
            ItemId = itemId; 
        }

        public void SetChangeType(string changeType)
        {
            ChangeType = changeType;
        }

        public void SetPreviousQuantity(int quantity)
        {
            PreviousQuantity = quantity;
        }

        public void SetNewQuantity(int quantity)
        {
            NewQuantity = quantity;
        }

        public void SetTime(DateTime time)
        {
            Time = time;
        }
    }
}
