﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Core;
using WpfExecutor.Converter;
using WpfExecutor.Helpers;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для ESearchParamControl.xaml
    /// </summary>
    public partial class ESearchParamControl : ComboBox, ICustomControl
    {
        public ESearchParamControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var list = typeof(ESearchParam).GeEnumTuple();
            var factory = new FrameworkElementFactory(typeof(ESearchParamControl));
            factory.SetValue(ItemsSourceProperty, list);
            factory.SetBinding(SelectedItemProperty, new Binding(nameof(IPropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new TupleConverter(),
                ConverterParameter = list,
            });
            return factory;
        }
    }
}
