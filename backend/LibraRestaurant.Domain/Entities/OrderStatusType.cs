using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class OrderStatusType : Entity
    {
        public int OrderStatusTypeId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public OrderStatusType(
            int orderStatusTypeId,
            string name,
            string? description
        ) : base (orderStatusTypeId )
        {
            OrderStatusTypeId = orderStatusTypeId;
            Name = name;
            Description = description;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }
    }
}
