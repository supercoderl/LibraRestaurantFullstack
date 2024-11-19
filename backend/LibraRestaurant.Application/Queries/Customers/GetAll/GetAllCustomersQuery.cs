using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Customers.GetAll;

public sealed record GetAllCustomersQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CustomerViewModel>>;