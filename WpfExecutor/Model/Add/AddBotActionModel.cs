using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Core.Attributes;
using Core.ConfigEntity;
using Core.Core;
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
        /// Комманда добавить
        /// </summary>
        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandMethod));
        /// <summary>
        /// Реализация комманды добавить
        /// </summary>
        private void AddCommandMethod()
        {
           CloseCommand?.Execute(true);
        }
    }
}
