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
    public sealed class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Token?> GetByOldTokenAsync(string refreshToken)
        {
            return await DbSet.SingleOrDefaultAsync(token => token.OldToken == refreshToken);
        }
    }
}
