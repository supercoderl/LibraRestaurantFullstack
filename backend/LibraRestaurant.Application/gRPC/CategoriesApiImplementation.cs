using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class CategoriesApiImplementation : CategoriesApi.CategoriesApiBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesApiImplementation(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<GetCategoriesByIdsResult> GetByIds(
            GetCategoriesByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var categories = await _categoryRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(category => idsAsIntegers.Contains(category.CategoryId))
                .Select(category => new GrpcCategory
                {
                    Id = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive,
                    IsDeleted = category.Deleted
                })
                .ToListAsync();

            var result = new GetCategoriesByIdsResult();

            result.Categories.AddRange(categories);

            return result;
        }
    }
}
