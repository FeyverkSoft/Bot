using System;
using System.Windows;
using System.Windows.Input;
using WpfConverters.Extensions;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Extensions.Localization;
using WpfExecutor.Factories;

namespace WpfExecutor.Model
{
    public class MainWindowModel : BaseViewModel
    {
        /// <summary>
        /// Комманда открытия
        /// </summary>
        private ICommand _closeCommand;
        /// <summary>
        /// Комманда зупуска
        /// </summary>
        private ICommand _runCommand;
        /// <summary>
        /// Комманда открытия документа
        /// </summary>
        private ICommand _openCommand;
        /// <summary>
        /// комманда сохранения документа
        /// </summary>
        private ICommand _saveCommand;
        /// <summary>
        /// комманды создания нового документа
        /// </summary>
        private ICommand _newCommand;
        /// <summary>
        /// Отобразить справку
        /// </summary>
        private ICommand _helpCommand;
        /// <summary>
        /// Отобрахить инфу о программе
        /// </summary>
        private ICommand _aboutCommand;
        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand = new DelegateCommand(About));

        private void About()
        {
            var window = WindowFactory.CreateAboutWindow();
            window.SetOwner();
            window.ShowDialog();
        }
        /// <summary>
        /// Команда закрытия
        /// </summary>
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseCommandMethod));

        private void CloseCommandMethod()
        {
            if (MessageBox.Show(
                LocalizationManager.GetString("CloseMessage"),
                LocalizationManager.GetString("CloseMessageTitle"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        /// <summary>
        /// Команда открытия нового документа
        /// </summary>
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(OpenCommandMethod));

        private void OpenCommandMethod()
        {
            throw new NotImplementedException();
        }
    }
}
