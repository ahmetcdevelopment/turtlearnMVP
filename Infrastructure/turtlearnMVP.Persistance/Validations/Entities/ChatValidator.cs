using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations.Entities
{
    public class ChatValidator : AbstractValidator<Chat>
    {
        public ChatValidator()
        {
            RuleFor(x => x.Picture).MaximumLength(255).WithMessage("Resim karakteri 255 karakterden daha büyük olmamalıdır.");
            RuleFor(x => x.Privacy).GreaterThanOrEqualTo(0).WithMessage("Gizlilik için geçerli bir değer seçmelisiniz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Chat adı boş bırakılmamalıdır.")
                .MaximumLength(100).WithMessage("Chat adı 100 karakterden daha büyük olmamalıdır.");
        }
    }
}
