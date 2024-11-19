using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using LibraRestaurant.Domain.Commands.PaymentMethods.DeletePaymentMethod;
using LibraRestaurant.Domain.Commands.PaymentMethods.UpdatePaymentMethod;
using LibraRestaurant.Domain.Commands.PaymentMethods.CreatePaymentMethod;
using LibraRestaurant.Application.Queries.PaymentMethods.GetAll;
using LibraRestaurant.Application.Queries.PaymentMethods.GetPaymentMethodById;

namespace LibraRestaurant.Application.Services
{
    public sealed class PaymentMethodService : IPaymentMethodService
    {
        private readonly IMediatorHandler _bus;

        public PaymentMethodService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<PaymentMethodViewModel?> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            return await _bus.QueryAsync(new GetPaymentMethodByIdQuery(paymentMethodId));
        }

        public async Task<PagedResult<PaymentMethodViewModel>> GetAllPaymentMethodsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPaymentMethodsQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreatePaymentMethodAsync(CreatePaymentMethodViewModel paymentMethod)
        {
            await _bus.SendCommandAsync(new CreatePaymentMethodCommand(
                0,
                paymentMethod.Name,
                paymentMethod.Description,
                paymentMethod.Picture,
                true));

            return 0;
        }

        public async Task UpdatePaymentMethodAsync(UpdatePaymentMethodViewModel paymentMethod)
        {
            await _bus.SendCommandAsync(new UpdatePaymentMethodCommand(
                paymentMethod.PaymentMethodId,
                paymentMethod.Name,
                paymentMethod.Description,
                paymentMethod.Picture,
                paymentMethod.IsActive));
        }

        public async Task DeletePaymentMethodAsync(int paymentMethodId)
        {
            await _bus.SendCommandAsync(new DeletePaymentMethodCommand(paymentMethodId));
        }
    }
}
