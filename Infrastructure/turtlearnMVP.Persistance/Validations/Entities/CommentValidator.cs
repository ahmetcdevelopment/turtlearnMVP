using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations.Entities
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.TableId).GreaterThan(0).WithMessage("Yorumun yapılacağı tablo seçilmelidir.");
            RuleFor(x => x.RecordId).GreaterThan(0).WithMessage("Yorumun yapılacağı kayıt seçilmelidir.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Yorum alanı boş bırakılmamalıdır.")
                .MaximumLength(250).WithMessage("Yorum 250 karakterden daha büyük olmamalıdır.");
            RuleFor(x => x.Rating).NotNull().WithMessage("Yoruma değerlendirme verilmelidir.");
        }
    }
}
