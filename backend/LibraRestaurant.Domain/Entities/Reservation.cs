using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Reservation : Entity
    {
        public int ReservationId { get; private set; }
        public int TableNumber { get; private set; }
        public int Capacity { get; private set; }
        public ReservationStatus Status { get; private set; }
        public Guid StoreId { get; private set; }
        public string? Description { get; private set; }
        public DateTime? ReservationTime { get; private set; }
        public int? CustomerId { get; private set; }
        public string? Code { get; private set; }
        public DateTime? CleaningTime { get; private set; }

        [ForeignKey("StoreId")]
        [InverseProperty("Reservations")]
        public virtual Store? Store { get; set; }

        [InverseProperty("Reservation")]
        public virtual ICollection<OrderHeader>? OrderHeaders { get; set; } = new List<OrderHeader>();

        [ForeignKey("CustomerId")]
        [InverseProperty("Reservation")]
        public virtual Customer? Customer { get; set; }

        public Reservation(
            int reservationId,
            int tableNumber,
            int capacity,
            ReservationStatus status,
            Guid storeId,
            string? description,
            DateTime? reservationTime,
            int? customerId,
            string? code,
            DateTime? cleaningTime
        ) : base(reservationId)
        {
            ReservationId = reservationId;
            TableNumber = tableNumber;
            Capacity = capacity;
            Status = status;
            StoreId = storeId;
            Description = description;
            ReservationTime = reservationTime;
            CustomerId = customerId;
            Code = code;
            CleaningTime = cleaningTime;
        }

        public void SetTableNumber( int tableNumber )
        {
            TableNumber = tableNumber;
        }

        public void SetCapacity( int capacity ) 
        { 
            Capacity = capacity; 
        } 
        
        public void SetStatus( ReservationStatus status ) 
        {
            Status = status;
        }

        public void SetStoreId( Guid storeId )
        {
            StoreId = storeId;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }

        public void SetReservationTime( DateTime? reservationTime )
        {
            ReservationTime = reservationTime;
        }

        public void SetCustomer( int? customerId )
        {
            CustomerId = customerId;
        }

        public void SetCode(string? code )
        {
            Code = code;
        }

        public void SetCleaningTime( DateTime? cleaningTime )
        {
            CleaningTime = cleaningTime;
        }
    }
}
