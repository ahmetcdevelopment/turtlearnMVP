using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations.Entities
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.SinifDuzeyiId).NotNull().WithMessage("Sınıf düzeyi seçmek zorunludur.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("İçerik alanı 100 karakterden daha büyük olamaz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ad alanı 100 karakterden büyük olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz")
                .MaximumLength(250).WithMessage("Açıklama alanı 250 karakterden daha büyük olamaz");
            RuleFor(x => x.Picture).MaximumLength(250).WithMessage("Resmin boyutu daha küçük olmalıdır.");
        }
    }
}
