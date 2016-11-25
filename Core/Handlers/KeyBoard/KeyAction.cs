using CommonLib.Attributes;

namespace Core.Handlers.KeyBoard
{
    [LocDescription("KeyAction", typeof(Resources.CoreText))]
    public enum KeyAction
    {
        /// <summary>
        /// Нажимает и отпускает клавишу
        /// </summary>
        [LocDescription("KeyAction_Press", typeof(Resources.CoreText))]
        Press,
        /// <summary>
        /// Нажимает и удерживает клавишу
        /// </summary>
        [LocDescription("KeyAction_Down", typeof(Resources.CoreText))]
        Down,
        /// <summary>
        /// Отпускает клавишу
        /// </summary>
        [LocDescription("KeyAction_Up", typeof(Resources.CoreText))]
        Up
    }
}
