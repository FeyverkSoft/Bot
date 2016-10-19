using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;

namespace WpfExecutor
{
    /// <summary>
    /// Текущий исполняемый документ
    /// Сингелтон
    /// </summary>
    public class Document
    {
        public static Document Instance { get; private set; }
        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public Config DocumentItems { get; }

        /// <summary>
        /// Действия которые необходимо выполнить
        /// </summary>
        public String Path { get; }

        public static void CreateInstance(Config conf, String path = null)
        {
            Instance = new Document(conf, path);
        }
        private Document(Config config, String path)
        {
            DocumentItems = config;
            Path = path;
        }
    }
}
