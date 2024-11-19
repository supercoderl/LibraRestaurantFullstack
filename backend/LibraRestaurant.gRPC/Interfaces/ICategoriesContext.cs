using LibraRestaurant.Shared.Categories;
using LibraRestaurant.Shared.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface ICategoriesContext
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesByIds(IEnumerable<int> ids);
    }
}
