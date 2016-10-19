using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExecutor
{
    /// <summary>
    /// Текущий исполняемый документ
    /// </summary>
   public class Document
    {
        public static Document Instance { get; private set; }

        public static void CreateInstance()
        {
            Instance = new Document();
        }
    }
}
