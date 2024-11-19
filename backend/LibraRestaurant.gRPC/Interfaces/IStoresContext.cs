using LibraRestaurant.Shared.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Interfaces
{
    public interface IStoresContext
    {
        Task<IEnumerable<StoreViewModel>> GetStoresByIds(IEnumerable<Guid> ids);
    }
}
