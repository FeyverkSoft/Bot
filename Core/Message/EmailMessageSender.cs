using System;

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
            throw new NotImplementedException();
        }
    }
}