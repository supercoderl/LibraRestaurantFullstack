using LibraRestaurant.Shared.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IDiscountsContext
    {
        Task<IEnumerable<DiscountViewModel>> GetDiscountsByIds(IEnumerable<int> ids);
    }
}
