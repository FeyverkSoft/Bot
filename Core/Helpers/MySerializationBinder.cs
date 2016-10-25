using System;
using System.Linq;
using Core.ConfigEntity.ActionObjects;
using Newtonsoft.Json.Serialization;

namespace Core.Helpers
{
    public class MySerializationBinder : DefaultSerializationBinder
    {

        /// <summary>При переопределении в производном классе управляет привязкой сериализованного объекта к типу.</summary>
        /// <returns>Тип объекта, новый экземпляр которого создает форматер.</returns>
        /// <param name="assemblyName">Задает имя <see cref="T:System.Reflection.Assembly" /> сериализованного объекта. </param>
        /// <param name="typeName">Задает имя <see cref="T:System.Type" /> сериализованного объекта. </param>
        public override Type BindToType(String assemblyName, String typeName)
        {
            var type = Assemblys.TypeList.FirstOrDefault(x => x.FullName == typeName && x.Assembly.GetName().Name == assemblyName);
            if (type != null)
                return type;

            type = Assemblys.PluginsTypeList.FirstOrDefault(x => x.FullName == typeName && x.Assembly.GetName().Name == assemblyName);

            if (type != null)
                return type;

            if(!AppConfig.Instance.LoadPlugin)
                return typeof(MockAction);

            throw new Exception($"Тип {typeName} в сборке {assemblyName} не был найден не в одной из подключенных сборок");
        }
    }
}