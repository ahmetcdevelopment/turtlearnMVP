using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Dtos;

namespace turtlearnMVP.Persistance.Validations.DTOs;

public class UserLoginDTOValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDTOValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş bırakılmamalıdır.")
            .MaximumLength(100).WithMessage("Email alanı 100 karakterden daha büyük olmamalıdır.");
    }
}
