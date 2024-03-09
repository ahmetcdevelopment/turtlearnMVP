using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs;

public class MessageApiDTOValidator : AbstractValidator<MessageApiDTO>
{
    public MessageApiDTOValidator()
    {
        RuleFor(x => x.ChatId).GreaterThan(0).WithMessage($"Mesaj göndereceğiniz sohbet geçerli olmalıdır.");
        RuleFor(x => x.SenderId).GreaterThan(0).WithMessage($"Lütfen tekrardan giriş yapınız.");
        RuleFor(x => x.Content).NotEmpty().WithMessage($"Boş mesaj gönderilemez.")
            .MaximumLength(250).WithMessage($"Mesaj 250 karakterden daha büyük olmamalıdır.");
        RuleFor(x => x.Media).MaximumLength(2000).WithMessage($"Medya ögesi sınırına ulaşıldı.");
    }
}
