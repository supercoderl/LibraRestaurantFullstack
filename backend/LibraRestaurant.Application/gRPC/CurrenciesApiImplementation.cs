using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Currencies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class CurrenciesApiImplementation : CurrenciesApi.CurrenciesApiBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrenciesApiImplementation(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public override async Task<GetCurrenciesByIdsResult> GetByIds(
            GetCurrenciesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var currencies = await _currencyRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(currency => idsAsIntegers.Contains(currency.CurrencyId))
                .Select(currency => new GrpcCurrency
                {
                    Id = currency.CurrencyId,
                    Name = currency.Name,
                    Description = currency.Description,
                    IsDeleted = currency.Deleted
                })
                .ToListAsync();

            var result = new GetCurrenciesByIdsResult();

            result.Currencies.AddRange(currencies);

            return result;
        }
    }
}
