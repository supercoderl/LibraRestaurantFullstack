using LibraRestaurant.Domain.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Infrastructure.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasKey(x => x.MessageId);

            builder
                .Property(item => item.SenderId);

            builder
                .Property(item => item.ReceiverId);

            builder
                .Property(item => item.Content)
                .IsRequired();

            builder
                .Property(item => item.Time)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .Property(item => item.IsRead)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(item => item.ConversationId);

            builder
                .Property(item => item.MessageType)
                .IsRequired();

            builder
                .Property(item => item.AttachmentUrl);
        }
    }
}
