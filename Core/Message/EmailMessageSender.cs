using System;
using System.Net;
using System.Net.Mail;
using LogWrapper;

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
                var mailClient = new SmtpClient(AppConfig.Instance.SmtpHost, AppConfig.Instance.SmtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(AppConfig.Instance.SmtpLogin, AppConfig.Instance.SmtpPassword)
                };
                var from = new MailAddress(AppConfig.Instance.SmtpLogin, "Bot message");
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
                Log.WriteLine(ex.Message);
                return new MessageResult(EMessageStatus.Fail, ex.Message);
            }

        }
    }
}