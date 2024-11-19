using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Customer : Entity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Customer")]
        public virtual Reservation? Reservation { get; set; }

        [InverseProperty("Customer")]
        public virtual OrderHeader? OrderHeader { get; set; }

        public Customer(
            int customerId,
            string name,
            string phone,
            string? email,
            string? address,
            DateTime createdAt,
            DateTime? updatedAt
        ) : base(customerId)
        {
            CustomerId = customerId;     
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetPhone( string phone )
        {
            Phone = phone;
        }

        public void SetEmail( string? email )
        {
            Email = email;
        }

        public void SetAddress( string? address )
        {
            Address = address;
        }

        public void SetCreatedAt( DateTime createdAt )
        {
            CreatedAt = createdAt;
        }

        public void SetUpdatedAt( DateTime? updatedAt )
        {
            UpdatedAt = updatedAt;
        }
    }
}
