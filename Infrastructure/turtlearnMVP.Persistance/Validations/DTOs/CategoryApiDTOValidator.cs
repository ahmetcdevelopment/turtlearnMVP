using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs
{
    public class CategoryApiDTOValidator : AbstractValidator<CategoryApiDTO>
    {
        public CategoryApiDTOValidator()
        {
            RuleFor(x => x.SinifDuzeyiId).GreaterThan(0).WithMessage("Lütfen geçerli bir sınıf düzeyi seçiniz.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("İçerik alanı en fazla 50 karakter olmalıdır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olmamalıdır.")
                .MaximumLength(250).WithMessage("Açıklama alanı en fazla 250 karakter olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılmamalıdır.")
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olmalıdır.");
        }
    }
}
