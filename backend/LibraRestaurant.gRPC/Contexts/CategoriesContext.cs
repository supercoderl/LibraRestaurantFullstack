using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Categories;
using LibraRestaurant.Shared.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class CategoriesContext : ICategoriesContext
    {
        private readonly CategoriesApi.CategoriesApiClient _client;

        public CategoriesContext(CategoriesApi.CategoriesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesByIds(IEnumerable<int> ids)
        {
            var request = new GetCategoriesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Categories.Select(category => new CategoryViewModel(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.Picture,
                category.IsDeleted));
        }
    }
}
