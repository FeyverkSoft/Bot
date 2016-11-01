using System;
using System.ComponentModel;
using System.Windows.Input;
using Core.ConfigEntity;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Factories;
using WpfExecutor.Model.Add;

namespace WpfExecutor.Model.Content
{
    public sealed class GenCanvasModel : BaseViewModel
    {
        public object SelectedObject { get; set; }

        /// <summary>
        /// Команда добавить
        /// </summary>
        private ICommand _addCommand;

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
                switch (temp.GetType().Name)
                {
                    case nameof(Config):
                        {
                            //отображаем окно для добавления IBotAction в корень, последним элементом
                            //потом надо будет придумать как сделать выбор места
                            var winF = WindowFactory.CreateAddBotActionWindow();
                            if (winF.ShowDialog() == true)
                            {
                                var mod = winF.DataContext as AddBotActionModel;
                                mod = mod;
                            }
                        }
                        break;
                    case nameof(BotAction):
                        {
                            //Добавляем поддействие IAction в действие
                            var winF = WindowFactory.CreateAddActionWindow(((BotAction)temp).ActionType);
                            if (winF.ShowDialog() == true)
                            {
                                var mod = winF.DataContext as AddActionViewModel;
                                if (mod != null)
                                {
                                    ((IBotAction) temp).SubActions.Add(mod.Action);
                                   // OnPropertyChanged(nameof(CommandConfig));
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
