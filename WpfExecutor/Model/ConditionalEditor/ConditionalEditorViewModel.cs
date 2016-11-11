using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Helpers;
using Core.ActionExecutors.ExecutorResult;
using Core.Core;

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
                //Refresh();
            }
        }

        public List<Tuple<String, Type>> ExecutorResultList { get; } = new List<Tuple<string, Type>>();

        public ConditionalEditorViewModel()
        {
            foreach (var typ in ActionFactory.ExResultTypes)
            {
                ExecutorResultList.Add(new Tuple<String, Type>(typ.GetLocalName(), typ));
            }
        }
    }
}
