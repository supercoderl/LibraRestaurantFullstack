using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Wards;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class WardViewModelSortProvider : ISortingExpressionProvider<WardViewModel, Ward>
    {
        private static readonly Dictionary<string, Expression<Func<Ward, object>>> s_expressions = new()
    {
        { "name", ward => ward.Name },
        { "nameEn", ward => ward.NameEn },
        { "fullname", ward => ward.Fullname },
        { "fullnameEn", ward => ward.FullnameEn },
        { "codeName", ward => ward.CodeName }
    };

        public Dictionary<string, Expression<Func<Ward, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
