using System;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace WpfExecutor.Model.Error
{
    public class ExceptionViewModel : BaseViewModel
    {
        private readonly Exception _exception;
        private readonly String _message;

        public ExceptionViewModel(Exception exception, String message)
        {
            _exception = exception;
            _message = message;

            var builder = new StringBuilder();

            builder.AppendLine($"Version: {(Application.ResourceAssembly.Location != null ? FileVersionInfo.GetVersionInfo(Application.ResourceAssembly.Location).ProductVersion : null)}");
            builder.AppendLine($"Date: {DateTime.Now:dd.MM.yyyy H:mm:ss}");
            builder.AppendLine($"User name: {Environment.UserName}");
            builder.AppendLine($"Machine name: {Environment.MachineName}");
            builder.AppendLine($"OS Version: {Environment.OSVersion}");
            builder.AppendLine($"Processor Count: {Environment.ProcessorCount}");
            builder.AppendLine($"Uptime: {TimeSpan.FromMilliseconds(Environment.TickCount)}");
            builder.AppendLine($"CLR Version: {Environment.Version}");
            builder.AppendLine();
            builder.AppendLine($"Exception source: {exception.Source}");
            builder.AppendLine($"Exception: {exception}");
            Info = builder.ToString();
        }

        public String Info { get; }

        public String Message
        {
            get
            {
                if (String.IsNullOrEmpty(_message) || String.IsNullOrWhiteSpace(_message))
                    return _exception.Message;
                return _message;
            }
        }
    }
}
