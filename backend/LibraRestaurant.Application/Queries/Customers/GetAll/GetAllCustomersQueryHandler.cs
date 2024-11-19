using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Customers.GetAll;

public sealed class GetAllCustomersQueryHandler :
    IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerViewModel>>
{
    private readonly ISortingExpressionProvider<CustomerViewModel, Customer> _sortingExpressionProvider;
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository,
        ISortingExpressionProvider<CustomerViewModel, Customer> sortingExpressionProvider)
    {
        _customerRepository = customerRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CustomerViewModel>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customersQuery = _customerRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            customersQuery = customersQuery.Where(menu =>
                menu.Name.Contains(request.SearchTerm) ||
                menu.Phone.Contains(request.SearchTerm));
        }

        var totalCount = await customersQuery.CountAsync(cancellationToken);

        customersQuery = customersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var customers = await customersQuery
            .OrderByDescending(customer => customer.CreatedAt)
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(customer => CustomerViewModel.FromCustomer(customer))
            .ToListAsync(cancellationToken);

        return new PagedResult<CustomerViewModel>(
            totalCount, customers, request.Query.Page, request.Query.PageSize);
    }
}