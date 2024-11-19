using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Currency : Entity
    {
        public int CurrencyId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Currency(
            int currencyId,
            string name,
            string? description
        ) : base(currencyId)
        {
            CurrencyId = currencyId;
            Name = name;
            Description = description;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }
    }
}
