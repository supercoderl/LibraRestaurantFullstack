using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Messages;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Messages.GetAll;

public sealed record GetAllMessagesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    string? Type = null,
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<MessageViewModel>>;