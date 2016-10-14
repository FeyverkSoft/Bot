using System;

namespace Core.Message
{
    /// <summary>
    /// ��������� �������� ���������
    /// </summary>
    public class MessageResult
    {
        /// <summary>
        /// ������ ��������
        /// </summary>
        public EMessageStatus Status { get; set; }
        /// <summary>
        /// ������� ������ ��� ������, ���� ������ �� �������
        /// </summary>
        public String Reason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">������ ��������</param>
        /// <param name="reason"> ������� ������ ��� ������, ���� ������ �� �������</param>
        public MessageResult(EMessageStatus status = EMessageStatus.Success, String reason = "" )
        {
            Status = status;
            Reason = reason;
        }
        
    }
}