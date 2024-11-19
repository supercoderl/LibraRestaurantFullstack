using LibraRestaurant.Application.ViewModels.Districts;
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
    public sealed class DistrictViewModelSortProvider : ISortingExpressionProvider<DistrictViewModel, District>
    {
        private static readonly Dictionary<string, Expression<Func<District, object>>> s_expressions = new()
    {
        { "name", district => district.Name },
        { "nameEn", district => district.NameEn },
        { "fullname", district => district.Fullname },
        { "fullnameEn", district => district.FullnameEn },
        { "codeName", district => district.CodeName }
    };

        public Dictionary<string, Expression<Func<District, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
