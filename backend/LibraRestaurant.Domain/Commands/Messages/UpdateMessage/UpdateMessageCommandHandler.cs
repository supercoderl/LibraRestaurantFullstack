
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.OrderLine;
using LibraRestaurant.Shared.Events.Messages;

namespace LibraRestaurant.Domain.Commands.Messages.UpdateMessage
{
    public sealed class UpdateMessageCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;

        public UpdateMessageCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMessageRepository messageRepository) : base(bus, unitOfWork, notifications)
        {
            _messageRepository = messageRepository;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var message = await _messageRepository.GetByIdAsync(request.MessageId);

            if (message is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no message with Id {request.MessageId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            message.SetSender(request.SenderId);
            message.SetReceiver(request.ReceiverId);
            message.SetContent(request.Content);
            message.SetTime(request.Time);
            message.SetIsRead(request.IsRead);
            message.SetConversation(request.ConversationId);
            message.SetMessageType(request.MessageType);
            message.SetAttachmentUrl(request.AttachmentUrl);

            _messageRepository.Update(message);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MessageUpdatedEvent(message.MessageId));
            }
        }
    }
}
