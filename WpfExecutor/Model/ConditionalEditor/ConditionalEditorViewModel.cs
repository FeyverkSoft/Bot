using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ActionExecutors.ExecutorResult;

namespace WpfExecutor.Model.ConditionalEditor
{
    public class ConditionalEditorViewModel : BaseViewModel
    {
        Tuple<String, Type> _selectedItem;
        public Tuple<String, Type> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                Refresh();
            }
        }

        public List<Tuple<String, Type>> ExecutorResultList { get; private set; }
        
        public ConditionalEditorViewModel()
        {
        }
    }
}
