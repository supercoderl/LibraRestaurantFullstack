using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetAll;

public sealed class GetAllPaymentHistoriesQueryHandler :
    IRequestHandler<GetAllPaymentHistoriesQuery, PagedResult<PaymentHistoryViewModel>>
{
    private readonly ISortingExpressionProvider<PaymentHistoryViewModel, PaymentHistory> _sortingExpressionProvider;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository;

    public GetAllPaymentHistoriesQueryHandler(
        IPaymentHistoryRepository paymentHistoryRepository,
        ISortingExpressionProvider<PaymentHistoryViewModel, PaymentHistory> sortingExpressionProvider)
    {
        _paymentHistoryRepository = paymentHistoryRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<PaymentHistoryViewModel>> Handle(
        GetAllPaymentHistoriesQuery request,
        CancellationToken cancellationToken)
    {
        var paymentHistoriesQuery = _paymentHistoryRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        var totalCount = await paymentHistoriesQuery.CountAsync(cancellationToken);

        paymentHistoriesQuery = paymentHistoriesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var paymentHistories = await paymentHistoriesQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(paymentHistory => PaymentHistoryViewModel.FromPaymentHistory(paymentHistory))
            .ToListAsync(cancellationToken);

        return new PagedResult<PaymentHistoryViewModel>(
            totalCount, paymentHistories, request.Query.Page, request.Query.PageSize);
    }
}