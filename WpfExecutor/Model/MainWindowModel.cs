﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core;
using Core.ConfigEntity;
using Core.Core;
using Core.Plugin;
using LogWrapper.Helpers;
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
        /// Комманда закрытия
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
        /// комманда сохранения документа
        /// </summary>
        private ICommand _saveAsCommand;
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

        private String _textLog;

        public CoreStatus Status => _core.Status;
        /// <summary>
        /// 
        /// </summary>
        public String Path => Document.Instance.Path;

        /// <summary>
        /// Меню предоставляемые плагинами
        /// </summary>
        public List<MenuItem> PluginMenu { get; }

        /// <summary>
        /// Логи бота
        /// </summary>
        public String TextLog
        {
            get { return _textLog; }
            private set
            {
                _textLog = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Json представление, пока что readonly
        /// </summary>
        public String Json => Document.Instance.DocumentItems.ToJson(typeName: true);
        /// <summary>
        /// Список ошибок в документе
        /// </summary>
        public List<String> ErrList => _core.ConfigValidator.GetErrorList(Document.Instance.DocumentItems.Actions);

        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand = new DelegateCommand(About));

        public MainWindowModel(String[] args)
        {
            Document.CreateInstance(new Config());
            _core = AppContext.Get<IExecutiveCore>();
            _core.PropertyChanged += (sender, e) => OnPropertyChanged(e?.PropertyName);
            _core.OnPrintMessageEvent += (message) => TextLog += $"{Environment.NewLine}{message}";
            if (args?.Length == 1 && File.Exists(args[0]))
            {
                Document.CreateInstance(ConfigReaderFactory.Get<Config>().Load(args[0]), args[0]);
            }

            StaticPropertyChanged += OnStaticPropertyChanged;
            PluginMenu = new List<MenuItem>();
            foreach (var plugin in Assemblys.PluginsList.Distinct())
            {

                if (plugin.ShowMenue)
                {
                    var item = new MenuItem
                    {
                        Header = plugin.Name
                    };
                    item.Items.Add(new MenuItem
                    {
                        Header = plugin.Menu.Title,
                        Command = new DelegateCommand(plugin.Menu.Command),
                        ItemsSource = FullPMenu(plugin.Menu.MenuItems)
                    });
                    PluginMenu.Add(item);
                }
            }
        }

        private ObservableCollection<MenuItem> FullPMenu(IReadOnlyCollection<PluginMenuItemModel> menu)
        {
            var list = new ObservableCollection<MenuItem>();
            if (menu == null)
                return list;
            foreach (var pluginMenuItemModel in menu)
            {
                list.Add(new MenuItem
                {
                    Header = pluginMenuItemModel.Title,
                    Command = new DelegateCommand(pluginMenuItemModel.Command),
                    ItemsSource = (pluginMenuItemModel.MenuItems?.Count > 0) ? FullPMenu(pluginMenuItemModel.MenuItems) : null
                });
            }
            return list;
        }

        /// <summary>
        /// Событие изменения какого либо свойства в документе.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OnStaticPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == nameof(Document.DocumentItems))
            {
                OnPropertyChanged(nameof(Json));
                OnPropertyChanged(nameof(ErrList));
                OnPropertyChanged(nameof(Path));
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
        public new ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseCommandMethod));

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
                OnPropertyChanged(nameof(Path));
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
                    OnPropertyChanged(nameof(Path));
                }
        }

        /// <summary>
        /// комманда исполнения документа
        /// </summary>
        public ICommand RunCommand => _runCommand ?? (_runCommand = new DelegateCommand(RunCommandMethod));

        private void RunCommandMethod()
        {
            _core.Run(Document.Instance.DocumentItems);
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
                Document.Instance.DocumentItems.UpdateBotVer();
                ConfigReaderFactory.Get<Config>().Save(Document.Instance.DocumentItems, Document.Instance.Path);
                return;
            }
            SaveAsCommandMethod();
        }


        /// <summary>
        /// комманда сохранить как
        /// </summary>
        public ICommand SaveAsCommand => _saveAsCommand ?? (_saveAsCommand = new DelegateCommand(SaveAsCommandMethod));

        private void SaveAsCommandMethod()
        {
            var sd = new SaveFileDialog { Filter = "bot command file|*.jsn" };
            if (sd.ShowDialog() == true)
            {
                Document.Instance.DocumentItems.UpdateBotVer();
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

        public ICommand HelpCommand => _helpCommand ?? (_helpCommand = new DelegateCommand(() => { throw new NotImplementedException("Справка еще не реализована"); }));
    }
}
