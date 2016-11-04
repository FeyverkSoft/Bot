using System.Runtime.Serialization;
using Core.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("MockAction")]
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
