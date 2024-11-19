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
    public sealed class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Message?> GetByMessageType(string messageType)
        {
            return await DbSet.SingleOrDefaultAsync(item => item.MessageType == messageType);
        }
    }
}
