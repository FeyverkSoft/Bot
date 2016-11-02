using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Extensions.Localization;
using WpfExecutor.Factories;
using WpfExecutor.Model.Add;

namespace WpfExecutor.Model.Content
{
    public sealed class GenCanvasModel : BaseViewModel
    {
        public Object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                MenuRefresh();
            }
        }
        /// <summary>
        /// Показывает на активность пункта меню, редактирование
        /// </summary>
        public Boolean EditCommandEnabled
        {
            get { return _editCommandEnabled; }
            private set
            {
                _editCommandEnabled = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Показывает на активность пункта меню, добавить
        /// </summary>
        public Boolean AddCommandEnabled
        {
            get { return _addCommandEnabled; }
            private set
            {
                _addCommandEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда добавить
        /// </summary>
        private ICommand _addCommand;
        /// <summary>
        /// Комманда удалить
        /// </summary>
        private ICommand _deleteCommand;
        /// <summary>
        /// Комманда редактировать
        /// </summary>
        private ICommand _editCommand;

        private object _selectedObject;
        private bool _editCommandEnabled;
        private bool _addCommandEnabled;
        public Config[] CommandConfig => new[] { Document.Instance.DocumentItems };

        public GenCanvasModel()
        {
            StaticPropertyChanged += OnPropertyChanged;
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
                OnPropertyChanged(nameof(CommandConfig));
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
                if (temp.GetType().GetInterfaces().Contains(typeof(IBotActionContainer)))
                {
                    //отображаем окно для добавления IBotAction в корень, последним элементом
                    //потом надо будет придумать как сделать выбор места
                    var winF = WindowFactory.CreateAddBotActionWindow();
                    if (winF.ShowDialog() == true)
                    {
                        var mod = winF.DataContext as AddBotActionModel;
                        if (mod != null)
                        {
                            ((IBotActionContainer)temp).Actions.Add(mod.BotAction);
                        }
                    }
                }
                if (temp.GetType().GetInterfaces().Contains(typeof(IActionsContainer)))
                {  //Добавляем поддействие IAction в действие
                    var winF = WindowFactory.CreateAddActionWindow(((BotAction)temp).ActionType);
                    if (!((IActionsContainer)temp).IsMultiAct && ((IActionsContainer)temp).SubActions.Count > 0)
                    {
                        MessageBox.Show(
                            LocalizationManager.GetString("NotSupportedMessage"),
                            LocalizationManager.GetString("NotSupportedMessageTitle"), MessageBoxButton.OK);
                        return;
                    }
                    if (winF.ShowDialog() == true)
                    {
                        var mod = winF.DataContext as AddActionViewModel;
                        if (mod != null)
                        {
                            ((IActionsContainer)temp).SubActions.Add(mod.Action);
                        }
                    }
                }
                Document.OnChanged();
            }
        }

        /// <summary>
        /// Комманда удалить
        /// </summary>
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand<Object>(DeleteCommandMethod));
        /// <summary>
        /// Имплементация метода удаления комманды у бота
        /// </summary>
        void DeleteCommandMethod(Object o)
        {
            var temp = o ?? SelectedObject;
            if (temp != null)
            {
                if (MessageBox.Show(
                        LocalizationManager.GetString("DeleteMessage"),
                        LocalizationManager.GetString("DeleteMessageTitle"), MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {
                    RecursRemove(temp, Document.Instance.DocumentItems.Actions);
                    Document.OnChanged();
                }
            }
        }

        /// <summary>
        /// Рекурсивная функция обхода дерева и удаления элементов
        /// </summary>
        /// <param name="o"></param>
        /// <param name="list"></param>
        void RecursRemove(Object o, ListBotAction list)
        {
            foreach (var item in list)
            {
                if (item == o)
                {
                    list.Remove(item);
                    break;
                }
                foreach (var it in item.SubActions)
                {
                    if (it == o)
                    {
                        item.SubActions.Remove(it);
                        break;
                    }
                    if (it is IBotActionContainer)
                    {
                        RecursRemove(o, ((IBotActionContainer)it).Actions);
                    }
                }
            }
        }


        /// <summary>
        /// Комманда редактировать
        /// </summary>
        public ICommand EditCommand => _editCommand ?? (_editCommand = new DelegateCommand<Object>(EditCommandMethod));
        /// <summary>
        /// Имплементация метода редактирования комманды боту
        /// </summary>
        void EditCommandMethod(Object o)
        {
            var temp = o ?? SelectedObject;
            if (temp != null)
            {
                if (temp.GetType().GetInterfaces().Contains(typeof(IAction)))
                {  //редактируем поддействие IAction в действии
                    var winF = WindowFactory.CreateAddActionWindow(ActionFactory.GetType((IAction)temp), (IAction)temp);
                    if (winF.ShowDialog() == true)
                    {
                        SelectedObject = temp;
                        Document.OnChanged();
                        //хак, что бы обновлять без рефреша дерева
                        LocalizationManager.Instance.ChangeCultureCommand.Execute(CultureInfo.CurrentCulture);
                    }
                }
            }
        }

        private void MenuRefresh()
        {
            EditCommandEnabled = SelectedObject is IAction;
            var add = SelectedObject is IBotActionContainer || SelectedObject is IActionsContainer;
            if (SelectedObject is IActionsContainer &&
                !((IActionsContainer) SelectedObject).IsMultiAct
                && ((IActionsContainer) SelectedObject).SubActions.Count > 0)
                AddCommandEnabled = false;
            else
                AddCommandEnabled = add;

        }
    }
}
