using LibraRestaurant.Application.ViewModels.Messages;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class MessageViewModelSortProvider : ISortingExpressionProvider<MessageViewModel, Message>
    {
        private static readonly Dictionary<string, Expression<Func<Message, object>>> s_expressions = new()
    {
        { "content", message => message.Content },
    };

        public Dictionary<string, Expression<Func<Message, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
