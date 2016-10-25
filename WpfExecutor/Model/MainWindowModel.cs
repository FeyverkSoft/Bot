using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Core;
using Core.ConfigEntity;
using Core.Core;
using LogWrapper;
using Microsoft.Win32;
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
        /// Отобразить инфу о программе
        /// </summary>
        private ICommand _aboutCommand;

        /// <summary>
        /// Прервать
        /// </summary>
        private ICommand _abortCommand;
        /// <summary>
        /// Вызвать Настройки ядра
        /// </summary>
        private ICommand _settingsCommand;

        /// <summary>
        /// Ядро бота
        /// </summary>
        private readonly IExecutiveCore _core;

        private string _textLog;

        public CoreStatus Status => _core.Status;

        public String TextLog
        {
            get { return _textLog; }
            private set
            {
                _textLog = value;
                OnPropertyChanged();
            }
        }

        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand = new DelegateCommand(About));

        public MainWindowModel(String[] args)
        {
            Document.CreateInstance(new Config());
            _core = AppContext.Get<IExecutiveCore>();
            _core.OnPrintMessageEvent += (message) => TextLog += $"{Environment.NewLine}{message}";
            _core.PropertyChanged +=(sender, e) => OnPropertyChanged(e?.PropertyName);
            if (args?.Length == 1 && File.Exists(args[0]))
            {
                Document.CreateInstance(ConfigReaderFactory.Get<Config>().Load(args[0]), args[0]);
            }
        }

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
            {
                _core?.Abort();
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Команда открытия нового документа
        /// </summary>
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(OpenCommandMethod));

        private void OpenCommandMethod()
        {
            var od = new OpenFileDialog { Filter = "bot command file|*.jsn" };
            if (od.ShowDialog() == true)
            {
                TextLog = String.Empty;
                var conf = ConfigReaderFactory.Get<Config>().Load(new StreamReader(od.OpenFile()));
                _core.Abort();
                Document.CreateInstance(conf, od.FileName);
            }
        }

        /// <summary>
        /// комманды создания нового документа
        /// </summary>
        public ICommand NewCommand => _newCommand ?? (_newCommand = new DelegateCommand(NewCommandMethod));

        private void NewCommandMethod()
        {
            if (Document.Instance.DocumentItems.Actions.Count > 0)
                if (MessageBox.Show(LocalizationManager.GetString("NewDocMessage"), LocalizationManager.GetString("NewDocMessageTitle"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TextLog = String.Empty;
                    _core.Abort();
                    Document.CreateInstance(new Config());
                }
        }

        /// <summary>
        /// комманда исполнения документа
        /// </summary>
        public ICommand RunCommand => _runCommand ?? (_runCommand = new DelegateCommand(RunCommandMethod));

        private void RunCommandMethod()
        {
            _core.Run(Document.Instance.DocumentItems);
            CoreFactory.GetCore();
        }

        /// <summary>
        /// Комманда прирывания
        /// </summary>
        public ICommand AbortCommand => _abortCommand ?? (_abortCommand = new DelegateCommand(AbortCommandMethod));

        private void AbortCommandMethod()
        {
            _core?.Abort();
        }

        /// <summary>
        /// комманда сохранения документа
        /// </summary>
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveCommandMethod));

        private void SaveCommandMethod()
        {
            if (!String.IsNullOrEmpty(Document.Instance.Path))
            {
                ConfigReaderFactory.Get<Config>().Save(Document.Instance.DocumentItems, Document.Instance.Path);
                return;
            }
            var sd = new SaveFileDialog { Filter = "bot command file|*.jsn" };
            if (sd.ShowDialog() == true)
            {
                var conf = ConfigReaderFactory.Get<Config>().Save(Document.Instance.DocumentItems, sd.FileName);
                Document.CreateInstance(conf, sd.FileName);
            }
        }

        /// <summary>
        /// Отобразить настройки ядра
        /// </summary>
        public ICommand SettingsCommand => _settingsCommand ?? (_settingsCommand = new DelegateCommand(SettingsCommandMethod));

        private void SettingsCommandMethod()
        {
            var window = WindowFactory.CreateCoreSettingsWindow();
            window.SetOwner();
            window.ShowDialog();
        }

        public ICommand HelpCommand => _helpCommand ?? (_helpCommand = new DelegateCommand(()=> {throw new NotImplementedException("Справка еще не реализована");}));
    }
}
