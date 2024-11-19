using System;
using LibraRestaurant.Application.ViewModels.Stores;
using MediatR;

namespace LibraRestaurant.Application.Queries.Stores.GetStoreById;

public sealed record GetStoreByIdQuery(Guid Id) : IRequest<StoreViewModel?>;