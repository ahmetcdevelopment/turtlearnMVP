using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class HomeworkValidator : AbstractValidator<Homework>
    {
        public HomeworkValidator()
        {
            RuleFor(x => x.SessionId).GreaterThan(0).WithMessage("Ödev verilecek oturum seçilmelidir.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Ödev başlığı boş bırakılmamalıdır.")
                .MaximumLength(200).WithMessage("Ödev başlığı 200 karakterden büyük olmamalıdır.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Ödevin başlangıç saati boş olmamalıdır.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("Ödevin bitiş saati boş olmamalıdır.");
            RuleFor(x => x.Description).MaximumLength(350).WithMessage("Ödevin açıklaması 350 karakterden büyük olamamalıdır.");
        }
    }
}
