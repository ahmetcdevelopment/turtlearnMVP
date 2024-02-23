using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs
{
    public class CourseApiDTOValidator : AbstractValidator<CourseApiDTO>
    {
        public CourseApiDTOValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().WithMessage("Kategori alanı boş bırakılmamalıdır.")
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");
            RuleFor(x => x.TeacherId).NotNull().WithMessage("Eğitmen alanı boş bırakılmamalıdır.")
                .GreaterThan(0).WithMessage("Lütfen geçerli bir eğitmen seçiniz.");
            RuleFor(x => x.Status).GreaterThanOrEqualTo(0).WithMessage("Lütfen geçerli bir durum seçiniz.")
                .NotNull().WithMessage("Durum alanı boş bırakılmamalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılmamalıdır")
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olmaldıdır");
            RuleFor(x => x.PricePerHour).NotNull().WithMessage("Saatlik ücret alanı boş bırakılmamalıdır.");
            RuleFor(x => x.Quota).NotNull().WithMessage("Kota alanı boş bırakılmamalıdır.");
            RuleFor(x => x.TotalHour).NotNull().WithMessage("Toplam saat alanı boş bırakılmamalıdır.");
            RuleFor(x => x.TotalPrice).NotNull().WithMessage("Toplam ücret alanı boş bırakılmamalıdır.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç zamanı boş bırakılmamalıdır.");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Bitiş zamanı boş bırakılmamalıdır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş bırakılamamalıdır.")
                .MaximumLength(500).WithMessage("Açıklama alanı en fazla 500 karakter olmalıdır.");
        }
    }
}
