﻿using System.Windows;
using WpfExecutor.Model.About;

namespace WpfExecutor.Views.About
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow(AboutViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
