using System.Windows;
using WpfExecutor.Model.ConditionalEditor;

namespace WpfExecutor.Views.ConditionalEditor
{
    /// <summary>
    /// Логика взаимодействия для AddAction.xaml
    /// </summary>
    public partial class ConditionalEditorWindow : Window
    {
        public ConditionalEditorWindow(ConditionalEditorViewModel model)
        {
            InitializeComponent();
            model.Close += (sender, b) =>
             {
                 DialogResult = b;
                 Close();
             };
            DataContext = model;
        }
    }
}
