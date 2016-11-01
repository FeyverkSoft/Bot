using System;
using System.Windows.Input;
using Core.ConfigEntity;
using WpfConverters.Extensions.Commands;

namespace WpfExecutor.Model.Add
{
    /// <summary>
    /// Настройка и генерация элементов типа IBotAction
    /// </summary>
    public sealed class AddBotActionModel : BaseViewModel
    {
        private Tuple<String, Object> _currentType;

        /// <summary>
        /// Текущий выбранный тип действия
        /// </summary>
        public Tuple<String, Object> CurrentType
        {
            get { return _currentType; }
            set
            {
                _currentType = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Действие бота
        /// </summary>
        public IBotAction BotAction { get; private set; }

        /// <summary>
        /// Комманда добавить
        /// </summary>
        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandMethod));
        /// <summary>
        /// Реализация комманды добавить
        /// </summary>
        private void AddCommandMethod()
        {
            if (CurrentType != null)
            {
                BotAction = new BotAction((ActionType) CurrentType.Item2);
                CloseCommand?.Execute(true);
                return;
            }
            CloseCommand?.Execute(false);
        }
    }
}
