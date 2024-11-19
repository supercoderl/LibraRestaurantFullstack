using LibraRestaurant.Shared.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IReviewsContext
    {
        Task<IEnumerable<ReviewViewModel>> GetReviewsByIds(IEnumerable<int> ids);
    }
}
