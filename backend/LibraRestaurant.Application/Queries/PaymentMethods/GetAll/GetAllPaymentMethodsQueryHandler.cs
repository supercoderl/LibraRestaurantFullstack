using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.PaymentMethods.GetAll;

public sealed class GetAllPaymentMethodsQueryHandler :
    IRequestHandler<GetAllPaymentMethodsQuery, PagedResult<PaymentMethodViewModel>>
{
    private readonly ISortingExpressionProvider<PaymentMethodViewModel, PaymentMethod> _sortingExpressionProvider;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public GetAllPaymentMethodsQueryHandler(
        IPaymentMethodRepository paymentMethodRepository,
        ISortingExpressionProvider<PaymentMethodViewModel, PaymentMethod> sortingExpressionProvider)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<PaymentMethodViewModel>> Handle(
        GetAllPaymentMethodsQuery request,
        CancellationToken cancellationToken)
    {
        var paymentMethodsQuery = _paymentMethodRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted && x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            paymentMethodsQuery = paymentMethodsQuery.Where(paymentMethod =>
                paymentMethod.Name.Contains(request.SearchTerm) ||
                (paymentMethod.Description != null && paymentMethod.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await paymentMethodsQuery.CountAsync(cancellationToken);

        paymentMethodsQuery = paymentMethodsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var paymentMethods = await paymentMethodsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(paymentMethod => PaymentMethodViewModel.FromPaymentMethod(paymentMethod))
            .ToListAsync(cancellationToken);

        return new PagedResult<PaymentMethodViewModel>(
            totalCount, paymentMethods, request.Query.Page, request.Query.PageSize);
    }
}