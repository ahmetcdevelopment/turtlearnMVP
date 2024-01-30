using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class SessionRollCallValidator : AbstractValidator<SessionRollCall>
    {
        public SessionRollCallValidator()
        {
            RuleFor(x => x.CourseId).NotNull().WithMessage("Kurs alanı seçilmelidir.");
            RuleFor(x => x.SessionId).GreaterThan(0).WithMessage("Yoklama bir oturuma ait olmalıdır.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Yoklama için öğrenci seçilmelidir.");
            RuleFor(x => x.TimeToJoin).NotEmpty().WithMessage("Derse katılma zamanı dolu olmalıdır.");
        }
    }
}
