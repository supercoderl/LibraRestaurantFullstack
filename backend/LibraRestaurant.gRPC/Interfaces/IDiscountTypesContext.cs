using LibraRestaurant.Shared.DiscountTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IDiscountTypesContext
    {
        Task<IEnumerable<DiscountTypeViewModel>> GetDiscountTypesByIds(IEnumerable<int> ids);
    }
}
