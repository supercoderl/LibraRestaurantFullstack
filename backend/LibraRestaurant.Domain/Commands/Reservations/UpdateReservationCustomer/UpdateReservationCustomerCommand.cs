using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using LibraRestaurant.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.UpdateReservationCustomer
{
    public sealed class UpdateReservationCustomerCommand : CommandBase, IRequest<int>
    {
        private static readonly UpdateReservationCustomerCommandValidation s_validation = new();

        public int ReservationId { get; }
        public ReservationStatus Status { get; }
        public string CustomerName { get; }
        public string CustomerPhone { get; }

        public UpdateReservationCustomerCommand(
            int reservationId,
            ReservationStatus status,
            string customerName,
            string customerPhone) : base(reservationId)
        {
            ReservationId = reservationId;
            Status = status;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
