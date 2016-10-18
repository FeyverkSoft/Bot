using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfConverters.Extensions.Commands;

namespace WpfExecutor.Model
{
    public class MainWindowModel : BaseViewModel, IDisposable
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseMethod));

        private void CloseMethod()
        {
            Application.Current.Shutdown();
        }

        /// <summary>Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.</summary>
        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }
}
