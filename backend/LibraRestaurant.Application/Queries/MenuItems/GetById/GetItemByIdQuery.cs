﻿using LibraRestaurant.Application.ViewModels.MenuItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Queries.MenuItems.GetById
{
    public sealed record GetItemByIdQuery(int Id) : IRequest<ItemViewModel?>;
}