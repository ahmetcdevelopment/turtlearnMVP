using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.SenderId).GreaterThan(0).WithMessage("Mesajı ileten olmalıdır.");
            RuleFor(x => x.ChatId).GreaterThan(0).WithMessage("Mesaj bir Chat'e ait olmalıdır.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Boş mesaj gönderilemez.")
                .MaximumLength(250).WithMessage("Mesaj içeriği 250 karakterden fazla olmamalıdır.");
            RuleFor(x => x.Media).MaximumLength(2000).WithMessage("Medya içeriği 2000 karakterden daha büyük olmamalıdır.");
        }
    }
}
