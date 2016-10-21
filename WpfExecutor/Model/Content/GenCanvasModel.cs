using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Core.ConfigEntity;
using WpfExecutor.Extensions.Localization;

namespace WpfExecutor.Model.Content
{
    public sealed class GenCanvasModel : BaseViewModel
    {
        /// <summary>
        /// Список комманд бота
        /// </summary>
        private ICollectionView _CommandConfig;

        private ListBotAction lba;

        /// <summary>
        /// Отфильтрованное предстваление
        /// </summary>
        public ICollectionView CommandConfig
        {
            get { return _CommandConfig; }
            set
            {
                if (Equals(value, _CommandConfig)) return;
                _CommandConfig = value;
                /*if (_CommandConfig != null)
                    _CommandConfig.Filter =;*/
                OnPropertyChanged();
            }
        }

        public String ConfigVersion => $"{LocalizationManager.GetString("ConfigVersion")}: {Document.Instance.DocumentItems.BotVer}";

        public GenCanvasModel()
        {
            StaticPropertyChanged += OnPropertyChanged;
            CommandConfig = CollectionViewSource.GetDefaultView(Document.Instance.DocumentItems.Actions);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(Document.Instance))
            {
                CommandConfig = CollectionViewSource.GetDefaultView(Document.Instance.DocumentItems.Actions);
            }
        }
    }
}
