using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Repositories
{
    public sealed class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
