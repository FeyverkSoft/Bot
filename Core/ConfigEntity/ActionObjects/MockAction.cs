using System.Runtime.Serialization;
using CommonLib.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("MockAction", typeof(Resources.CoreText))]
    public class MockAction : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.Mock;

        public MockAction(){}

    }
}
