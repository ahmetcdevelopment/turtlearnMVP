using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;

namespace turtlearnMVP.Persistance.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Kullanıcı adı 50 karakterden uzun olmamalıdır.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Kullanıcının ismi boş olmamalıdır.")
                .MaximumLength(100).WithMessage("İsim 100 karakterden uzun olmamalıdır.");

        }
    }
}
