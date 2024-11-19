using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Repositories
{
    public sealed class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
