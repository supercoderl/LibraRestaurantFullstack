using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Messages.UpdateMessage
{
    public sealed class UpdateMessageCommandValidation : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidation()
        {
            AddRuleForId();
            AddRuleForContent();
            AddRuleForTime();
            AddRuleForMessageType();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.MessageId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Message.EmptyId)
                .WithMessage("Id may not be empty");
        }

        private void AddRuleForContent()
        {
            RuleFor(cmd => cmd.Content)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Message.EmptyContent)
                .WithMessage("Content id may not be empty");
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
