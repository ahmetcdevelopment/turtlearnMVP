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
                        Subject = "This Is Your Code",
                        Body = "<!doctype html><html lang=\"en-US\"><head>    <meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />    <title>New Account Email Template</title>    <meta name=\"description\" content=\"New Account Email Template.\">    <style type=\"text/css\">        a:hover {text-decoration: underline !important;}    </style></head><body marginheight=\"0\" topmargin=\"0\" marginwidth=\"0\" style=\"margin: 0px; background-color: #1d1e22;\" leftmargin=\"0\">    <!-- 100% body table -->    <table cellspacing=\"0\" border=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#1d1e22\"        style=\"@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;\">        <tr>            <td>                <table style=\"background-color: #1d1e22; max-width:670px; margin:0 auto;\" width=\"100%\" border=\"0\"                    align=\"center\" cellpadding=\"0\" cellspacing=\"0\">                    <tr>                        <td style=\"height:80px;\">&nbsp;</td>                    </tr>                    <tr>                        <td style=\"text-align:center;\">                            <a href=\"https://rakeshmandal.com\" title=\"logo\" target=\"_blank\">                            <img width=\"70%\" src=\"https://www.zurafworks.com/assets/img/logo.png\" title=\"logo\" alt=\"logo\">                          </a>                        </td>                    </tr>                    <tr>                        <td style=\"height:20px;\">&nbsp;</td>                    </tr>                    <tr>                        <td>                            <table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"                                style=\"max-width:670px; background:#1d1e22; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);\">                                <tr>                                    <td style=\"height:40px;\">&nbsp;</td>                                </tr>                                <tr>                                    <td style=\"padding:0 35px;\">                                        <h1 style=\"color:#ffffff; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;\">Aramıza Hoşgeldin!                                        </h1>                                        <p style=\"font-size:15px; color:#cccccc; margin:8px 0 0; line-height:24px;\">                                            Turtlearn hesabın oluşturuldu. aşağıdaki kod ile mail adresini doğrulayabilir <br> ve eğitim almaya hemen başlayabilirsin  <br>                                           <span                                            style=\"display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;\"></span><br><strong style=\"font-size:40px; color:#538662; margin:8px 0 0; line-height:40px;\">578456</strong></p>                                        <span                                            style=\"display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;\"></span>                                        <p                                            style=\"color:#455056; font-size:18px;line-height:20px; margin:0; font-weight: 500;\">                                                                                                                       </td>                                </tr>                                <tr>                                    <td style=\"height:40px;\">&nbsp;</td>                                </tr>                            </table>                        </td>                    </tr>                    <tr>                        <td style=\"height:20px;\">&nbsp;</td>                    </tr>                    <tr>                        <td style=\"text-align:center; display:flex;\">                          <p style=\"font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;margin-left:20px;\">&copy; <strong>www.turtlearn.com</strong> </p>                            <p style=\"font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0; margin-left:300px;\">&copy; <strong>www.zurafworks.com</strong> </p>                        </td>                    </tr>                    <tr>                        <td style=\"height:80px;\">&nbsp;</td>                    </tr>                </table>            </td>        </tr>    </table>    <!--/100% body table--></body></html>",
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
