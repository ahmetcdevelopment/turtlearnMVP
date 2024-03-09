using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.ApiDTOs;

namespace turtlearnMVP.Persistance.Validations.DTOs;

public class HomeworkTransferApiDTOValidator : AbstractValidator<HomeworkTransferApiDTO>
{
    public HomeworkTransferApiDTOValidator()
    {
        RuleFor(x => x.StudentId).NotNull().WithMessage($"Ödevi gönderen öğrenci seçilmelidir.")
            .GreaterThan(0).WithMessage($"Lüttfen geçerli bir öğrenci giriniz.");
        RuleFor(x => x.HomeworkId).NotNull().WithMessage($"Lütfen gönderim için ödev seçiniz.")
            .GreaterThan(0).WithMessage($"Lütfen geçerli bir ödev seçiniz.");
        RuleFor(x => x.Status).NotNull().WithMessage($"Ödev gönderiminin durumu olmalıdır.")
            .GreaterThanOrEqualTo(0).WithMessage($"Lütfen geçerli bir ödem gönderim durumu seçiniz.");
        RuleFor(x => x.Attachment).NotEmpty().WithMessage($"Boş bir ödev gönderimi yapılamaz.")
            .MaximumLength(150).WithMessage($"Ödev gönderimi 150 karakterden daha fazla olmamalıdır.");
        RuleFor(x => x.TransferDate).NotEqual(DateTime.MinValue).WithMessage($"Ödev gönderim tarihi geçerli olmalıdır.");
        RuleFor(x => x.Description).MaximumLength(350).WithMessage($"Açıklama alanı 350 karakterden daha fazla olmamalıdır");
    }
}
