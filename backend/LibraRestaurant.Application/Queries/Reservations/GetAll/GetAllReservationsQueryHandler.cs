using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LibraRestaurant.Domain.Errors.DomainErrorCodes;
using Reservation = LibraRestaurant.Domain.Entities.Reservation;

namespace LibraRestaurant.Application.Queries.Reservations.GetAll;

public sealed class GetAllReservationsQueryHandler :
    IRequestHandler<GetAllReservationsQuery, PagedResult<ReservationViewModel>>
{
    private readonly ISortingExpressionProvider<ReservationViewModel, Reservation> _sortingExpressionProvider;
    private readonly IReservationRepository _reservationRepository;

    public GetAllReservationsQueryHandler(
        IReservationRepository reservationRepository,
        ISortingExpressionProvider<ReservationViewModel, Reservation> sortingExpressionProvider)
    {
        _reservationRepository = reservationRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ReservationViewModel>> Handle(
        GetAllReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var reservationsQuery = _reservationRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            reservationsQuery = reservationsQuery.Where(reservation =>
                (reservation.Description != null && reservation.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await reservationsQuery.CountAsync(cancellationToken);

        reservationsQuery = reservationsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var reservations = await reservationsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(reservation => ReservationViewModel.FromReservation(reservation, null))
            .ToListAsync(cancellationToken);

        return new PagedResult<ReservationViewModel>(
            totalCount, reservations, request.Query.Page, request.Query.PageSize);
    }
}