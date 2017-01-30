using System;
using System.Collections.ObjectModel;

namespace Core.Plugin
{
   public class PluginMenuItemModel
    {
        private String _title;
        private ObservableCollection<PluginMenuItemModel> _menuItems;
        private Action _command;

        /// <summary>
        /// Название пункта меню
        /// </summary>
        public String Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
            }
        }

        public ObservableCollection<PluginMenuItemModel> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
            }
        }

        public Action Command
        {
            get { return _command; }
            set
            {
                if (Equals(value, _command)) return;
                _command = value;
            }
        }
    }
}
