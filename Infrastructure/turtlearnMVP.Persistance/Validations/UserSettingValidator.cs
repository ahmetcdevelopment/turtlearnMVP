using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class UserSettingValidator : AbstractValidator<UserSetting>
    {
        public UserSettingValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Ayar işlemi yapılacak kullanıcı seçilmelidir.");
            RuleFor(x => x.Key).NotNull().WithMessage("Anahtar alanı boş olmamalıdır.")
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir ayar giriniz.");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value alanı boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Value alanı 250 karakterden daha büyük olmamalıdır.");
            RuleFor(x => x.TypeId).GreaterThanOrEqualTo(0).WithMessage("Lütfen geçerli bir ayar tipi seçiniz");
        }
    }
}
