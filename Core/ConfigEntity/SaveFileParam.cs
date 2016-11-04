using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using Core.Helpers;

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
        public String Path { get; set; }
        /// <summary>
        /// Тип сохраняемого файла
        /// </summary>
        [DataMember]
        [LocDescription("SaveFileParam_Type")]
        public ImageFileFormat Type { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [DataMember]
        [LocDescription("SaveFileParam_Name")]
        public String Name { get; set; }
        public SaveFileParam(String path, ImageFileFormat type, String name = null)
        {
            if(String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            Path = path;
            Type = type;
            Name = name;
        }

        public SaveFileParam()
        {
            
        }
        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override String ToString()
        {
            return ("\n" + this.GetString()).Replace("\n","\n\t");
        }
    }
}
