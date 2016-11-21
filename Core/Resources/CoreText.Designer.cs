﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CoreText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CoreText() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Core.Resources.CoreText", typeof(CoreText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Тип доступных боту действий.
        /// </summary>
        internal static string ActionType {
            get {
                return ResourceManager.GetString("ActionType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ожидай окно.
        /// </summary>
        internal static string ActionType_ExpectWindow {
            get {
                return ResourceManager.GetString("ActionType_ExpectWindow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Получи позицию курсора.
        /// </summary>
        internal static string ActionType_GetMousePos {
            get {
                return ResourceManager.GetString("ActionType_GetMousePos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Получи информацию об объекте.
        /// </summary>
        internal static string ActionType_GetObject {
            get {
                return ResourceManager.GetString("ActionType_GetObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Получить скриншот (эксперементально).
        /// </summary>
        internal static string ActionType_GetScreenshot {
            get {
                return ResourceManager.GetString("ActionType_GetScreenshot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на GOTO.
        /// </summary>
        internal static string ActionType_GOTO {
            get {
                return ResourceManager.GetString("ActionType_GOTO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Особый оператор условного ветвления (эксперементально).
        /// </summary>
        internal static string ActionType_If {
            get {
                return ResourceManager.GetString("ActionType_If", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Нажми последовательность клавиш.
        /// </summary>
        internal static string ActionType_KeyBoard {
            get {
                return ResourceManager.GetString("ActionType_KeyBoard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Нажми сочитание клавиш.
        /// </summary>
        internal static string ActionType_KeyBoardKeys {
            get {
                return ResourceManager.GetString("ActionType_KeyBoardKeys", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метка.
        /// </summary>
        internal static string ActionType_Label {
            get {
                return ResourceManager.GetString("ActionType_Label", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Выполнить в цикле.
        /// </summary>
        internal static string ActionType_Loop {
            get {
                return ResourceManager.GetString("ActionType_Loop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ничего не делать :) Заглушка..
        /// </summary>
        internal static string ActionType_Mock {
            get {
                return ResourceManager.GetString("ActionType_Mock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Кликни левой кнопкой мыши.
        /// </summary>
        internal static string ActionType_MouseLClick {
            get {
                return ResourceManager.GetString("ActionType_MouseLClick", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Нажми лувую кнопку мыши.
        /// </summary>
        internal static string ActionType_MouseLPress {
            get {
                return ResourceManager.GetString("ActionType_MouseLPress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отпусти левую кнопку мыши.
        /// </summary>
        internal static string ActionType_MouseLUp {
            get {
                return ResourceManager.GetString("ActionType_MouseLUp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Перемести указатель мыши относительно координат.
        /// </summary>
        internal static string ActionType_MouseMove {
            get {
                return ResourceManager.GetString("ActionType_MouseMove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Кликни правой кнопкой мыши..
        /// </summary>
        internal static string ActionType_MouseRClick {
            get {
                return ResourceManager.GetString("ActionType_MouseRClick", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Нажми правую кнопку мыши.
        /// </summary>
        internal static string ActionType_MouseRPress {
            get {
                return ResourceManager.GetString("ActionType_MouseRPress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отпусти правую кнопку мыши.
        /// </summary>
        internal static string ActionType_MouseRUp {
            get {
                return ResourceManager.GetString("ActionType_MouseRUp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Задать абсолютную позицию указателя..
        /// </summary>
        internal static string ActionType_MouseSetPos {
            get {
                return ResourceManager.GetString("ActionType_MouseSetPos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Любые действия плагина.
        /// </summary>
        internal static string ActionType_PluginAct {
            get {
                return ResourceManager.GetString("ActionType_PluginAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вызвать плагин.
        /// </summary>
        internal static string ActionType_PluginInvoke {
            get {
                return ResourceManager.GetString("ActionType_PluginInvoke", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отправь уведомление.
        /// </summary>
        internal static string ActionType_SendMessage {
            get {
                return ResourceManager.GetString("ActionType_SendMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Усни!.
        /// </summary>
        internal static string ActionType_Sleep {
            get {
                return ResourceManager.GetString("ActionType_Sleep", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Работа со стеком.
        /// </summary>
        internal static string ActionType_Stack {
            get {
                return ResourceManager.GetString("ActionType_Stack", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Название метки не может быть пустым.
        /// </summary>
        internal static string ConfigValidator_GetErrorListInternal_Error_EmptyLabelName {
            get {
                return ResourceManager.GetString("ConfigValidator_GetErrorListInternal_Error_EmptyLabelName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка действия &quot;{0}&quot;.
        /// </summary>
        internal static string ConfigValidator_Invalid_action {
            get {
                return ResourceManager.GetString("ConfigValidator_Invalid_action", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка действия &quot;{0}&quot;, Метка &quot;{1}&quot; не может быть объявленна более одного раза, в одной области видимости..
        /// </summary>
        internal static string ConfigValidator_Invalid_action_declared_more_than_once_in_the_same_scope {
            get {
                return ResourceManager.GetString("ConfigValidator_Invalid_action_declared_more_than_once_in_the_same_scope", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка действия &quot;{0}&quot;, Метка &quot;{1}&quot; не найдена в текущей области видимости..
        /// </summary>
        internal static string ConfigValidator_Invalid_action_Label_not_found {
            get {
                return ResourceManager.GetString("ConfigValidator_Invalid_action_Label_not_found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка действия &quot;{0}&quot;; Данное действие может содержать только одно под действие..
        /// </summary>
        internal static string ConfigValidator_Invalid_action_one_sub_action {
            get {
                return ResourceManager.GetString("ConfigValidator_Invalid_action_one_sub_action", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Версия исполняемой конфигурации не совпадает сверсией интерпретатора! Возможны побочные эффекты..
        /// </summary>
        internal static string DifferentConfigVersions {
            get {
                return ResourceManager.GetString("DifferentConfigVersions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Тип канала доставки уведомления.
        /// </summary>
        internal static string EMessageType {
            get {
                return ResourceManager.GetString("EMessageType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Е-Маил.
        /// </summary>
        internal static string EMessageType_Email {
            get {
                return ResourceManager.GetString("EMessageType_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Равно.
        /// </summary>
        internal static string Equal {
            get {
                return ResourceManager.GetString("Equal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр поиска.
        /// </summary>
        internal static string ESearchParam {
            get {
                return ResourceManager.GetString("ESearchParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя Объекта Содержит указанную подстроку..
        /// </summary>
        internal static string ESearchParam_Contained {
            get {
                return ResourceManager.GetString("ESearchParam_Contained", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя Объекта Заканчивается на..
        /// </summary>
        internal static string ESearchParam_End {
            get {
                return ResourceManager.GetString("ESearchParam_End", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя Объекта Начинается с..
        /// </summary>
        internal static string ESearchParam_Start {
            get {
                return ResourceManager.GetString("ESearchParam_Start", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие со стеком.
        /// </summary>
        internal static string EStackAction {
            get {
                return ResourceManager.GetString("EStackAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Очистить.
        /// </summary>
        internal static string EStackAction_Clear {
            get {
                return ResourceManager.GetString("EStackAction_Clear", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Просмотреть (Peek).
        /// </summary>
        internal static string EStackAction_Peek {
            get {
                return ResourceManager.GetString("EStackAction_Peek", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Извлечь.
        /// </summary>
        internal static string EStackAction_Pop {
            get {
                return ResourceManager.GetString("EStackAction_Pop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Поместить.
        /// </summary>
        internal static string EStackAction_Push {
            get {
                return ResourceManager.GetString("EStackAction_Push", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр поиска.
        /// </summary>
        internal static string ExpectWindowAct_SearchParam {
            get {
                return ResourceManager.GetString("ExpectWindowAct_SearchParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Передать фокус окну.
        /// </summary>
        internal static string ExpectWindowAct_SetFocus {
            get {
                return ResourceManager.GetString("ExpectWindowAct_SetFocus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Наименование ожидаемого окна.
        /// </summary>
        internal static string ExpectWindowAct_WinTitle {
            get {
                return ResourceManager.GetString("ExpectWindowAct_WinTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Описание параметров действия ожидания окна с указанным именем..
        /// </summary>
        internal static string ExpectWindowActDescription {
            get {
                return ResourceManager.GetString("ExpectWindowActDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие получения информации об объекте.
        /// </summary>
        internal static string GetObjectAct {
            get {
                return ResourceManager.GetString("GetObjectAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Позиция объекта на экране..
        /// </summary>
        internal static string GetObjectAct_ObjectPos {
            get {
                return ResourceManager.GetString("GetObjectAct_ObjectPos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Использовать позицию из результата предыдущего действия, если это возможно.
        /// </summary>
        internal static string GetObjectAct_PrevResult {
            get {
                return ResourceManager.GetString("GetObjectAct_PrevResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Установить фокус.
        /// </summary>
        internal static string GetObjectAct_SetFocus {
            get {
                return ResourceManager.GetString("GetObjectAct_SetFocus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на GOTO.
        /// </summary>
        internal static string GoToAct {
            get {
                return ResourceManager.GetString("GoToAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Название ярлычка для перехода.
        /// </summary>
        internal static string GoToAct_LabelName {
            get {
                return ResourceManager.GetString("GoToAct_LabelName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Условия.
        /// </summary>
        internal static string IfAction {
            get {
                return ResourceManager.GetString("IfAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на В случае не успеха перейти к метке.
        /// </summary>
        internal static string IfAction_FailLabel {
            get {
                return ResourceManager.GetString("IfAction_FailLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Проверка на тип предыдущего.
        /// </summary>
        internal static string IfAction_PrevResType {
            get {
                return ResourceManager.GetString("IfAction_PrevResType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на В случае успеха перейти к метке.
        /// </summary>
        internal static string IfAction_SuccessLabel {
            get {
                return ResourceManager.GetString("IfAction_SuccessLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Условия.
        /// </summary>
        internal static string IfAction_Сonditions {
            get {
                return ResourceManager.GetString("IfAction_Сonditions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на При выполнении действия {0}, произошла ошибка. {1}Тип действия не совпадает с членами действия..
        /// </summary>
        internal static string IncorrectAction {
            get {
                return ResourceManager.GetString("IncorrectAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Больше.
        /// </summary>
        internal static string IsGreaterThan {
            get {
                return ResourceManager.GetString("IsGreaterThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Больше или равно.
        /// </summary>
        internal static string IsGreaterThanOrEqual {
            get {
                return ResourceManager.GetString("IsGreaterThanOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Меньше.
        /// </summary>
        internal static string IsLessThan {
            get {
                return ResourceManager.GetString("IsLessThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Меньше или равно.
        /// </summary>
        internal static string IsLessThanOrEqual {
            get {
                return ResourceManager.GetString("IsLessThanOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметр нажатия клавиши.
        /// </summary>
        internal static string KeyBoardAct {
            get {
                return ResourceManager.GetString("KeyBoardAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Клавиша, нажатие которой надо эмулировать.
        /// </summary>
        internal static string KeyBoardAct_Key {
            get {
                return ResourceManager.GetString("KeyBoardAct_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Время удерживания (DxInput only).
        /// </summary>
        internal static string KeyBoardAct_Time {
            get {
                return ResourceManager.GetString("KeyBoardAct_Time", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Парметры событя одновременного нажатия нескольких клавиш.
        /// </summary>
        internal static string KeyBoardKeysAct {
            get {
                return ResourceManager.GetString("KeyBoardKeysAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Метка.
        /// </summary>
        internal static string LabelAct {
            get {
                return ResourceManager.GetString("LabelAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Название метки.
        /// </summary>
        internal static string LabelAct_LabelName {
            get {
                return ResourceManager.GetString("LabelAct_LabelName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие цикла.
        /// </summary>
        internal static string LoopAct {
            get {
                return ResourceManager.GetString("LoopAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Список действий которые необходимо выполнить в цикле.
        /// </summary>
        internal static string LoopAct_Actions {
            get {
                return ResourceManager.GetString("LoopAct_Actions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Колличество выполнений цикла.
        /// </summary>
        internal static string LoopAct_IterationCount {
            get {
                return ResourceManager.GetString("LoopAct_IterationCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Фальшивка.
        /// </summary>
        internal static string MockAction {
            get {
                return ResourceManager.GetString("MockAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие перемещения мышки.
        /// </summary>
        internal static string MouseMoveAct {
            get {
                return ResourceManager.GetString("MouseMoveAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Смещение по оси Y.
        /// </summary>
        internal static string MouseMoveAct_Dy {
            get {
                return ResourceManager.GetString("MouseMoveAct_Dy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Смещение по оси X.
        /// </summary>
        internal static string MouseMoveAct_Point {
            get {
                return ResourceManager.GetString("MouseMoveAct_Point", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Двигать к объекту являющемуся результатом предыдущего вызова.
        /// </summary>
        internal static string MouseMoveAct_ToObject {
            get {
                return ResourceManager.GetString("MouseMoveAct_ToObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры установки абсолютной позиции курсора.
        /// </summary>
        internal static string MouseSetPosAct {
            get {
                return ResourceManager.GetString("MouseSetPosAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Новая позиция курсора.
        /// </summary>
        internal static string MouseSetPosAct_Point {
            get {
                return ResourceManager.GetString("MouseSetPosAct_Point", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Использовать относительные координаты.
        /// </summary>
        internal static string MouseSetPosAct_Relatively {
            get {
                return ResourceManager.GetString("MouseSetPosAct_Relatively", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Наименование окна относительно которого указывается положение..
        /// </summary>
        internal static string MouseSetPosAct_RelativelyWindowName {
            get {
                return ResourceManager.GetString("MouseSetPosAct_RelativelyWindowName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Не равно.
        /// </summary>
        internal static string NotEqual {
            get {
                return ResourceManager.GetString("NotEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры вызова плагина.
        /// </summary>
        internal static string PluginInvokeAct {
            get {
                return ResourceManager.GetString("PluginInvokeAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действия плагина.
        /// </summary>
        internal static string PluginInvokeAct_Actions {
            get {
                return ResourceManager.GetString("PluginInvokeAct_Actions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Наименование плагина.
        /// </summary>
        internal static string PluginInvokeAct_PluginName {
            get {
                return ResourceManager.GetString("PluginInvokeAct_PluginName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Опции сохранения файла.
        /// </summary>
        internal static string SaveFileParam {
            get {
                return ResourceManager.GetString("SaveFileParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя файла.
        /// </summary>
        internal static string SaveFileParam_Name {
            get {
                return ResourceManager.GetString("SaveFileParam_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Путь.
        /// </summary>
        internal static string SaveFileParam_Path {
            get {
                return ResourceManager.GetString("SaveFileParam_Path", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Сохранить в файл.
        /// </summary>
        internal static string SaveFileParam_SaveFile {
            get {
                return ResourceManager.GetString("SaveFileParam_SaveFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Тип файла.
        /// </summary>
        internal static string SaveFileParam_Type {
            get {
                return ResourceManager.GetString("SaveFileParam_Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры скриншота.
        /// </summary>
        internal static string ScreenShotAct {
            get {
                return ResourceManager.GetString("ScreenShotAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Оттенки серого.
        /// </summary>
        internal static string ScreenShotAct_GrayScale {
            get {
                return ResourceManager.GetString("ScreenShotAct_GrayScale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Верхняя левая координата области захвата..
        /// </summary>
        internal static string ScreenShotAct_Point {
            get {
                return ResourceManager.GetString("ScreenShotAct_Point", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Использовать позицию из результата предыдущего действия, если это возможно.
        /// </summary>
        internal static string ScreenShotAct_PrevResult {
            get {
                return ResourceManager.GetString("ScreenShotAct_PrevResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Опции сохранения файла.
        /// </summary>
        internal static string ScreenShotAct_SaveFileParam {
            get {
                return ResourceManager.GetString("ScreenShotAct_SaveFileParam", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Размер области захвата.
        /// </summary>
        internal static string ScreenShotAct_Size {
            get {
                return ResourceManager.GetString("ScreenShotAct_Size", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие отправки сообщения.
        /// </summary>
        internal static string SendMessageAct {
            get {
                return ResourceManager.GetString("SendMessageAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Тело уведомления.
        /// </summary>
        internal static string SendMessageAct_Body {
            get {
                return ResourceManager.GetString("SendMessageAct_Body", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Включить предыдущие действие.
        /// </summary>
        internal static string SendMessageAct_IncludePrevRes {
            get {
                return ResourceManager.GetString("SendMessageAct_IncludePrevRes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Тип уведомления.
        /// </summary>
        internal static string SendMessageAct_MessageType {
            get {
                return ResourceManager.GetString("SendMessageAct_MessageType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Получатель сообщения.
        /// </summary>
        internal static string SendMessageAct_Recipient {
            get {
                return ResourceManager.GetString("SendMessageAct_Recipient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры сна.
        /// </summary>
        internal static string SleepAct {
            get {
                return ResourceManager.GetString("SleepAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Продолжительность сна.
        /// </summary>
        internal static string SleepAct_Delay {
            get {
                return ResourceManager.GetString("SleepAct_Delay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Максимальное время случайной задержки.
        /// </summary>
        internal static string SleepAct_MaxRandDelay {
            get {
                return ResourceManager.GetString("SleepAct_MaxRandDelay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Параметры действия со стеком.
        /// </summary>
        internal static string StackAct {
            get {
                return ResourceManager.GetString("StackAct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Действие со стеком.
        /// </summary>
        internal static string StackAct_Action {
            get {
                return ResourceManager.GetString("StackAct_Action", resourceCulture);
            }
        }
    }
}
