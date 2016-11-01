using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация об объекте который необходимо получить
    /// </summary>
    [LocDescription("GetObjectAct")]
    public class GetObjectAct : BaseActionObject
    {

        ///// <summary>
        ///// Тип ожидаемого объекта
        ///// </summary>
        //[DataMember]
        //public EObjectType ObjectType { get;}

        /// <summary>
        /// Указывает, на то что после того как объект найден, ему необходимо передать фокус
        /// </summary>
        [DataMember]
        [LocDescription("GetObjectAct_SetFocus")]
        public Boolean SetFocus { get; }

        ///// <summary>
        ///// Наименование объекта
        ///// </summary>
        //[DataMember]
        //public String ObjectName { get; private set; }
        /// <summary>
        /// Позиция объекта который необходимо захватить
        /// </summary>
        [DataMember]
        [LocDescription("GetObjectAct_ObjectPos")]
        public Point ObjectPos { get; private set; }
        /// <summary>
        /// Тип ожидаемого объекта
        /// </summary>
        /// <param name="objectType">Тип искомого объекта</param>
        /// <param name="objectName">Наименование искомого объекта</param>
        /// <param name="setFocus">Передать фокус найденному объекту?</param>
        public GetObjectAct( /*EObjectType objectType,String objectName,*/ Boolean setFocus = true)
        {
            //ObjectType = objectType;
          //  ObjectName = objectName;
            SetFocus = setFocus;
        }

        /// <summary>
        /// Тип ожидаемого объекта
        /// </summary>
        /// <param name="objectType">Тип искомого объекта</param>
        /// <param name="objectPos">Позиция объекта, который необходимо захватить</param>
        /// <param name="setFocus">Передать фокус найденному объекту?</param>
        [JsonConstructor]
        public GetObjectAct(/*EObjectType objectType,*/ Point objectPos, Boolean setFocus = true)
        {
            //ObjectType = objectType;
            ObjectPos = objectPos;
            SetFocus = setFocus;
        }
    }
}
