using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations.Entities
{
    public class SessionValidator : AbstractValidator<Session>
    {
        public SessionValidator()
        {
            RuleFor(x => x.CourseId).NotNull().WithMessage("Oturum bir kus'a ait olmalıdır.");
            RuleFor(x => x.Queue).GreaterThanOrEqualTo(0).WithMessage("Her oturumun bir sıralaması olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Oturum ismi boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Oturum ismi 150 karakterden daha büyük olamamalıdır.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Oturum başlangıç tarihi boş bırakılmamalıdır.");
            RuleFor(x => x.Link).NotEmpty().WithMessage("Ders linki boş bırakılmamalıdır.");
            RuleFor(x => x.Description).MaximumLength(350).WithMessage("Açıklama alanı 350 karakterden daha büyük olmamalıdır.");
        }
    }
}
