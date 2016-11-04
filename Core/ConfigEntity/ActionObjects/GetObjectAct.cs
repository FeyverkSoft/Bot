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
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.GetObject;
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
        public Boolean SetFocus { get; set; }

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
        public Point ObjectPos { get; set; }


        /// <summary>
        /// Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно
        /// </summary>
        [DataMember]
        [LocDescription("GetObjectAct_PrevResult")]
        public Boolean PrevResult { get; set; } = false;


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
        /// <param name="prevResult">Использовать для позиции результат предыдущего действия</param>
        /// <param name="objectPos">Позиция объекта, который необходимо захватить</param>
        /// <param name="setFocus">Передать фокус найденному объекту?</param>
        [JsonConstructor]
        public GetObjectAct(Point objectPos, Boolean setFocus = true, Boolean prevResult = false)
        {
            ObjectPos = objectPos;
            SetFocus = setFocus;
            PrevResult = prevResult;
        }

        public GetObjectAct(){}
    }
}
