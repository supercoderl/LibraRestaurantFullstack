using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Messages;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Messages.GetAll;

public sealed class GetAllMessagesQueryHandler :
    IRequestHandler<GetAllMessagesQuery, PagedResult<MessageViewModel>>
{
    private readonly ISortingExpressionProvider<MessageViewModel, Message> _sortingExpressionProvider;
    private readonly IMessageRepository _messageRepository;

    public GetAllMessagesQueryHandler(
        IMessageRepository messageRepository,
        ISortingExpressionProvider<MessageViewModel, Message> sortingExpressionProvider)
    {
        _messageRepository = messageRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<MessageViewModel>> Handle(
        GetAllMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messagesQuery = _messageRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            messagesQuery = messagesQuery.Where(message =>
                message.Content.Contains(request.SearchTerm));
        }

        var totalCount = await messagesQuery.CountAsync(cancellationToken);

        messagesQuery = messagesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var messages = await messagesQuery
            .OrderByDescending(message => message.Time)
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(message => MessageViewModel.FromMessage(message))
            .ToListAsync(cancellationToken);

        return new PagedResult<MessageViewModel>(
            totalCount, messages, request.Query.Page, request.Query.PageSize);
    }
}