using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class CourseEnrollmentValidator : AbstractValidator<CourseEnrollment>
    {
        public CourseEnrollmentValidator()
        {
            RuleFor(x => x.CourseId).GreaterThan(0).WithMessage("Kayıt olunacak kurs seçilmelidir.");
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Kayıt olacak öğrenci sisteme kayıtlı olmalıdır.");
            RuleFor(x => x.Approved).NotNull().WithMessage("Onay alanı boş bırakılmamalıdır.");
        }
    }
}
