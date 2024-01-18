using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface IMailService
    {
        Task<IResult> SendVerificationCodeAsync(/*string senderMail, string senderDisplayName,*/ string targetMail, int verificationCode);
        Task<IResult> SendMailAsync(/*string senderMail, string senderDisplayName,*/ string targetMail, string subject, string body, bool isBodyHtml);
    }
}
