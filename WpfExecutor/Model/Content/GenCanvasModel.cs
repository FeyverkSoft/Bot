using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Core.ConfigEntity;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Model.Content
{
    public sealed class GenCanvasModel : BaseViewModel
    {
        public object SelectedObject { get; set; }

        /// <summary>
        /// Команда добавить
        /// </summary>
        private ICommand _addCommand;

        /// <summary>
        /// Список комманд бота
        /// </summary>
        private ICollectionView _commandConfig;

        /// <summary>
        /// Отфильтрованное предстваление
        /// </summary>
        public ICollectionView CommandConfig
        {
            get { return _commandConfig; }
            set
            {
                if (Equals(value, _commandConfig)) return;
                _commandConfig = value;
                /*if (_commandConfig != null)
                    _commandConfig.Filter =;*/
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Версия файла конфигурации
        /// </summary>
        public String ConfigVersion => $"{LocalizationManager.GetString("ConfigVersion")}: {Document.Instance.DocumentItems.BotVer}";

        public GenCanvasModel()
        {
            StaticPropertyChanged += OnPropertyChanged;
            CommandConfig = CollectionViewSource.GetDefaultView(Document.Instance.DocumentItems.Actions);
        }

        /// <summary>
        /// Подписываемся на статику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(Document.Instance))
            {
                CommandConfig = CollectionViewSource.GetDefaultView(Document.Instance.DocumentItems.Actions);
                OnPropertyChanged(nameof(ConfigVersion));
            }
        }

        /// <summary>
        /// Комманда добавить
        /// </summary>
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand<Object>(AddCommandMethod));
        /// <summary>
        /// Имплементация метода добавления комманды боту
        /// </summary>
        void AddCommandMethod(Object o)
        {
            var temp = o ?? SelectedObject;
            if (temp != null)
            {
                
            }
        }
    }
}
