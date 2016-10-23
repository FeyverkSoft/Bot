using System;
using System.Runtime.Serialization;
using Core.Attributes;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Параметры сохранения файла
    /// </summary>
    [DataContract]
    [LocDescription("SaveFileParam")]
    public class SaveFileParam
    {
        /// <summary>
        /// Путь сохранения файла
        /// </summary>
        [DataMember]
        [LocDescription("SaveFileParam_Path")]
        public String Path { get; private set; }
        /// <summary>
        /// Тип сохраняемого файла
        /// </summary>
        [DataMember]
        [LocDescription("SaveFileParam_Type")]
        public String Type { get; private set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [DataMember]
        [LocDescription("SaveFileParam_Name")]
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
