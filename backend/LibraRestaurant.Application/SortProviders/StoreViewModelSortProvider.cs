using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class StoreViewModelSortProvider : ISortingExpressionProvider<StoreViewModel, Store>
    {
        private static readonly Dictionary<string, Expression<Func<Store, object>>> s_expressions = new()
    {
        { "name", store => store.Name },
        { "cityId", store => store.CityId },
        { "districtId", store => store.DistrictId },
        { "wardId", store => store.WardId },
        { "taxCode", store => store.TaxCode ?? string.Empty },
        { "address", store => store.Address ?? string.Empty },
        { "gpsLocation", store => store.GpsLocation ?? string.Empty },
        { "postalCode", store => store.PostalCode ?? string.Empty },
        { "phone", store => store.Phone ?? string.Empty },
        { "fax", store => store.Fax ?? string.Empty },
        { "email", store => store.Email ?? string.Empty },
        { "website", store => store.Website ?? string.Empty },
        { "logo", store => store.Logo ?? string.Empty },
        { "bankBranch", store => store.BankBranch ?? string.Empty },
        { "bankCode", store => store.BankCode ?? string.Empty },
        { "bankAccount", store => store.BankAccount ?? string.Empty },
        { "isActive", store => store.IsActive }
    };

        public Dictionary<string, Expression<Func<Store, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
