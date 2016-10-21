using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class Document: BaseViewModel
    {
        public static Document Instance { get; private set; }

        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public Config DocumentItems { private set; get; }

        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public String Path { private set; get; }

        public static void CreateInstance(Config conf, String path = null)
        {
            Instance = new Document(conf, path);
            Instance.OnPropertyChanged(nameof(DocumentItems));
            Instance.OnPropertyChanged(nameof(path));
            Instance.OnPropertyChanged(nameof(Instance));
        }
        private Document(Config config, String path)
        {
            DocumentItems = config;
            Path = path;
        }
    }
}
