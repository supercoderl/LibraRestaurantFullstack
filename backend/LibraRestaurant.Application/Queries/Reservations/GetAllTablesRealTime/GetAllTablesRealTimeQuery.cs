
using LibraRestaurant.Application.ViewModels.Reservations;
using MediatR;
using System.Collections.Generic;

namespace LibraRestaurant.Application.Queries.Reservations.GetAllTablesRealTime;

public sealed record GetAllTablesRealTimeQuery(bool IncludeDeleted) : IRequest<List<TableRealTimeViewModel>>;