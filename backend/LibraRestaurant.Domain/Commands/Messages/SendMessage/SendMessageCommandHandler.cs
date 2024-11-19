
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Messages;

namespace LibraRestaurant.Domain.Commands.Messages.SendMessage
{
    public sealed class SendMessageCommandHandler : CommandHandlerBase,
        IRequestHandler<SendMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IMessageRepository messageRepository) : base(bus, unitOfWork, notifications)
        {
            _messageRepository = messageRepository;
        }

        public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var message = new Entities.Message(
                request.MessageId,
                request.SenderId,
                request.ReceiverId,
                request.Content,
                request.Time,
                request.IsRead,
                request.ConversationId,
                request.MessageType,
                request.AttachmentUrl);

            _messageRepository.Add(message);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new MessageSentEvent(message.MessageId));
            }
        }
    }
}
