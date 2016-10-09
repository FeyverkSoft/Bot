using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Параметры сохранения файла
    /// </summary>
    public class SaveFileParam
    {
        /// <summary>
        /// Путь сохранения файла
        /// </summary>
        public String Path { get; private set; }
        /// <summary>
        /// Тип сохраняемого файла
        /// </summary>
        public String Type { get; private set; }

        public SaveFileParam(String path, String type)
        {
            if(String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (String.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));
            Path = path;
            Type = type;
        }
    }
}
