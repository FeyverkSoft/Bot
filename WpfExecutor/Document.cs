using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;
using WpfExecutor.Model;

namespace WpfExecutor
{
    /// <summary>
    /// Текущий исполняемый документ
    /// Сингелтон
    /// </summary>
    public class Document : BaseViewModel
    {
        private Config _documentItems;
        private String _path;
        public static Document Instance { get; private set; }

        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public Config DocumentItems
        {
            private set
            {
                _documentItems = value;
                OnPropertyChanged();
            }
            get { return _documentItems; }
        }

        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public String Path
        {
            private set
            {
                _path = value;
                OnPropertyChanged();
            }
            get { return _path; }
        }

        public static void CreateInstance(Config conf, String path = null)
        {
            Instance = new Document(conf, path);
        }
        private Document(Config config, String path)
        {
            DocumentItems = config;
            _path = path;
        }
    }
}
