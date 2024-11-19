using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Messages.SendMessage
{
    public sealed class SendMessageCommandValidation : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidation()
        {
            AddRuleForContent();
            AddRuleForTime();
            AddRuleForMessageType();
        }

        private void AddRuleForContent()
        {
            RuleFor(cmd => cmd.Content)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Message.EmptyContent)
                .WithMessage("Content may not be empty");
        }

        private void AddRuleForTime()
        {
            RuleFor(cmd => cmd.Time)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Message.EmptyTime)
                .WithMessage("Time may not be empty");
        }

        private void AddRuleForMessageType()
        {
            RuleFor(cmd => cmd.MessageType)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Message.EmptyMessageType)
                .WithMessage("Message type may not be empty");
        }
    }
}
