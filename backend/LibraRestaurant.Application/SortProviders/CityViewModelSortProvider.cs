using LibraRestaurant.Application.ViewModels.Cities;
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
    public sealed class CityViewModelSortProvider : ISortingExpressionProvider<CityViewModel, City>
    {
        private static readonly Dictionary<string, Expression<Func<City, object>>> s_expressions = new()
    {
        { "name", city => city.Name },
        { "nameEn", city => city.NameEn },
        { "fullname", city => city.Fullname },
        { "fullnameEn", city => city.FullnameEn },
        { "codeName", city => city.CodeName }
    };

        public Dictionary<string, Expression<Func<City, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
