using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.MenuItems;

namespace LibraRestaurant.Application.Queries.MenuItems.GetAll
{
    public sealed record GetAllItemsQuery(
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        SortQuery? SortQuery = null,
        int CategoryId = -1) :
        IRequest<PagedResult<ItemViewModel>>;
}
