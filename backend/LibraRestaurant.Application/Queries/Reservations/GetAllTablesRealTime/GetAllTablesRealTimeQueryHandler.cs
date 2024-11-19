using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Reservations.GetAllTablesRealTime;

public sealed class GetAllTablesRealTimeQueryHandler :
    IRequestHandler<GetAllTablesRealTimeQuery, List<TableRealTimeViewModel>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetAllTablesRealTimeQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<List<TableRealTimeViewModel>> Handle(
        GetAllTablesRealTimeQuery request,
        CancellationToken cancellationToken)
    {
        var reservationsQuery = _reservationRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        var reservations = await reservationsQuery
            .Select(reservation => TableRealTimeViewModel.FromTables(reservation))
            .ToListAsync(cancellationToken);

        return reservations;
    }
}