using System;

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

        /// <summary>
        /// Наименование файла
        /// </summary>
        public String Name { get; private set; }
        public SaveFileParam(String path, String type, String name = null)
        {
            if(String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (String.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));
            Path = path;
            Type = type;
            Name = name;
        }
    }
}
