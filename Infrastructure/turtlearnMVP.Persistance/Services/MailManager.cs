using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Persistance.Configurations;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace turtlearnMVP.Persistance.Services
{
    public class MailManager : IMailService
    {
        private readonly IOptions<SmtpSettings> _smtpSettings;
        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task<IResult> SendMailAsync(/*string senderMail, string senderDisplayName,*/ string targetMail, string subject, string body, bool isBodyHtml)
        {
            var message = "An error occured at sending mail to " + targetMail;
            try
            {
                using (var smtpClient = new SmtpClient(_smtpSettings.Value.SmtpServer, _smtpSettings.Value.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpSettings.Value.SmtpSenderEmail, _smtpSettings.Value.SmtpPassword);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.Value.SmtpSenderEmail, _smtpSettings.Value.SmtpSenderName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isBodyHtml,
                    };

                    mailMessage.To.Add(targetMail);

                    await smtpClient.SendMailAsync(mailMessage);
                    message = "Mail Successfully Sended";
                    return new Result(ResultStatus.Success, message);
                }
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, message + " Exception:" + ex.Message);
            }
        }

        public async Task<IResult> SendVerificationCodeAsync(/*string senderMail, string senderDisplayName,*/ string targetMail, int verificationCode)
        {
            var message = "An error occured at sending mail to " + targetMail;
            try
            {
                using (var smtpClient = new SmtpClient(_smtpSettings.Value.SmtpServer, _smtpSettings.Value.SmtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpSettings.Value.SmtpSenderEmail, _smtpSettings.Value.SmtpPassword);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpSettings.Value.SmtpSenderEmail, _smtpSettings.Value.SmtpSenderName),
                        Subject = "Turtlearn'e Hoşgeldin",
                        Body = _smtpSettings.Value.SmtpVerifyBodyPart1 + verificationCode.ToString() + _smtpSettings.Value.SmtpVerifyBodyPart2,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(targetMail);

                    await smtpClient.SendMailAsync(mailMessage);
                    message = "Mail Successfully Sended";
                    return new Result(ResultStatus.Success, message);
                }
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, message + " Exception:" + ex.Message);
            }
        }
    }
}
