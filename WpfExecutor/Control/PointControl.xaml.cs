﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfExecutor.Model;

namespace WpfExecutor.Control
{
    /// <summary>
    /// Логика взаимодействия для PointControl.xaml
    /// </summary>
    public partial class PointControl : UserControl, ICustomControl
    {
        public PointControl()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public static FrameworkElementFactory GetFrameworkElementFactory()
        {
            var factory = new FrameworkElementFactory(typeof(PointControl));
            factory.SetBinding(DataContextProperty, new Binding(nameof(PropModel.Value))
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            return factory;
        }
    }
}