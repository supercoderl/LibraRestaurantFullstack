using LibraRestaurant.Application.ViewModels.Reviews;
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
    public sealed class ReviewViewModelSortProvider : ISortingExpressionProvider<ReviewViewModel, Review>
    {
        private static readonly Dictionary<string, Expression<Func<Review, object>>> s_expressions = new()
    {
        { "name", review => review.CustomerName },
        { "comment", review => review.Comment },
    };

        public Dictionary<string, Expression<Func<Review, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
