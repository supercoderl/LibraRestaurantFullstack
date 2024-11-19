using LibraRestaurant.Application.ViewModels.MenuItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Queries.MenuItems.GetBySlug
{
    public sealed record GetItemBySlugQuery(string Slug) : IRequest<ItemViewModel?>;
}
