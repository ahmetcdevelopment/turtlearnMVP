using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs;

public class ChatApiDTOValidator : AbstractValidator<ChatApiDTO>
{
    public ChatApiDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage($"Sohbetin bir adı olmalıdır.")
            .MaximumLength(100).WithMessage($"Sohbet adı 100 karakterden büyük olmamalıdır.");
        RuleFor(x => x.Privacy).GreaterThanOrEqualTo(0).WithMessage($"Lütfen geçerli bir gizlilik seçiniz");
        RuleFor(x => x.Picture).MaximumLength(255).WithMessage($"Resim 255 karakter boyutundan daha büyük olmamalıdır.");
    }
}
