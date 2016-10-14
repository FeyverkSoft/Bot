using System;
using System.Net;
using System.Net.Mail;

namespace Core.Message
{
    /// <summary>
    /// ���������� �������� ��������� �� ��������� email ���������
    /// </summary>
    internal class EmailMessageSender : IMessageSender
    {
        /// <summary>
        /// ��������� ��������� ���������� ��������
        /// </summary>
        /// <param name="body">���� ���������</param>
        /// <param name="recipient">���������� ���������</param>
        /// <returns></returns>
        public MessageResult SendMessage(String body, String recipient)
        {
            try
            {
                var mailClient = new SmtpClient(AppConfig.SmtpHost, AppConfig.SmtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(AppConfig.SmtpLogin, AppConfig.SmtpPassword)
                };
                var from = new MailAddress(AppConfig.SmtpLogin, "Bot message");
                var to = new MailAddress(recipient, recipient);
                var message = new MailMessage(from, to)
                {
                    Sender = from,
                    IsBodyHtml = true,
                    Body = body,
                    Subject = $"Bot message {DateTime.UtcNow:G}"
                };
                mailClient.Send(message);
                return new MessageResult();
            }
            catch (Exception ex)
            {
                return new MessageResult(EMessageStatus.Fail, ex.Message);
            }

        }
    }
}