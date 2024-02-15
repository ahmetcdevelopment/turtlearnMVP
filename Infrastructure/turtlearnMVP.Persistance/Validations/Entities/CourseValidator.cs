using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations.Entities
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThan(0).WithMessage("Öğretmen alanı boş bırakılmamalıdır.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Kategori seçimi boş bırakılmamalıdır.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Başlangıç tarihi boş bırakılmamalıdır.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("Bitiş tarihi boş bırakılmamalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kurs Adı boş bırakılmamalıdır.")
                .MaximumLength(100).WithMessage("Kurs Adı 100 karakterden daha büyük olmamalıdır");
            RuleFor(x => x.PricePerHour).GreaterThanOrEqualTo(0).WithMessage("Geçerli bir saatlik ücret giriniz.");
            RuleFor(x => x.TotalHour).GreaterThan(0).WithMessage("Geçerli bir saat giriniz");
            //Burada GreaterThanOrEqualTo kullanmamın sebebi ücretsiz kurs yayımlanabilecek olmasıdır.
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kurs ücreti giriniz");
            RuleFor(x => x.Status).GreaterThan(0).WithMessage("Geçerli bir kurs durumu seçiniz");
        }
    }
}
