using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs;

public class SessionApiDTOValidator : AbstractValidator<SessionApiDTO>
{
    public SessionApiDTOValidator()
    {
        RuleFor(x => x.CourseId).NotNull().WithMessage("Kurs alanı boş bırakılmamalıdır.")
            .GreaterThan(0).WithMessage("Lütfen geçerli bir kurs seçiniz.");
        RuleFor(x => x.SessionName).NotEmpty().WithMessage("Ad alanı boş bırakılmamalıdır.")
            .MaximumLength(150).WithMessage("Ad alanı en fazla 150 karakter olmalıdır.");
        RuleFor(x => x.Queue).NotNull().WithMessage("Sıralama belirtmeniz gerekmektedir.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Oturum başlangıç zamanı boş bırakılmamalıdır.");
        RuleFor(x => x.Link).MaximumLength(100).WithMessage("Link alanı en fazla 100 karakter olmalıdır.")
            .NotEmpty().WithMessage("Link alanı boş bırakılmamalıdır.");
        RuleFor(x => x.Description).MaximumLength(350).WithMessage("Açıklama alanı en fazla 350 karakter olmalıdır.");

    }
}
