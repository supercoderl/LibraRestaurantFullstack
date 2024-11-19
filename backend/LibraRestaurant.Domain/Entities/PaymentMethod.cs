using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class PaymentMethod : Entity
    {
        public int PaymentMethodId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Picture {  get; private set; }
        public bool IsActive { get; private set; }

        [InverseProperty("PaymentMethod")]
        public virtual ICollection<OrderHeader>? OrderHeaders { get; set; } = new List<OrderHeader>();

        [InverseProperty("PaymentMethod")]
        public virtual ICollection<PaymentHistory>? PaymentHistories { get; set; } = new List<PaymentHistory>();

        public PaymentMethod(
            int paymentMethodId,
            string name,
            string? description,
            string? picture,
            bool isActive
        ) : base (paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
            Name = name;
            Description = description;
            Picture = picture;
            IsActive = isActive;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }

        public void SetPicture( string picture )
        {
            Picture = picture;
        }

        public void SetActive( bool isActive )
        {
            IsActive = isActive;
        }
    }
}
