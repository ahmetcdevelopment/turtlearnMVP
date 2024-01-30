using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class ChatUserValidator : AbstractValidator<ChatUser>
    {
        public ChatUserValidator()
        {
            RuleFor(x => x.IsManager).NotNull().WithMessage("Yönetici mi? alanı boş olmamalıdır.");
            RuleFor(x => x.ChatId).GreaterThan(0).WithMessage("Chat alanı boş olmamalıdır.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Kullanıcı alanı boş olmamalıdır.");
        }
    }
}
