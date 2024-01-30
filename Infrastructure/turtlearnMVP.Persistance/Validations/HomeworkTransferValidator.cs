using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Validations
{
    public class HomeworkTransferValidator : AbstractValidator<HomeworkTransfer>
    {
        public HomeworkTransferValidator()
        {
            RuleFor(x => x.HomeworkId).GreaterThan(0).WithMessage("Ödev gönderimi için bir ödev seçilmelidir.");
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Ödev gönderimi bir öğrenciye ait olmalıdır.");
            RuleFor(x => x.Status).GreaterThanOrEqualTo(0).WithMessage("Ödev gönderim durumu geçerli olmalıdır.");
            RuleFor(x => x.Attachment).NotNull().NotEmpty().WithMessage("Ödev gönderim dosyası olmalıdır.");
            RuleFor(x => x.TransferDate).NotNull().WithMessage("Ödev gönderim tarihi boş olmamalıdır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş bırakılmamalıdır.")
                .MaximumLength(350).WithMessage("Açıklama alanı 350");
        }
    }
}
