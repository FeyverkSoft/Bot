﻿using System;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия щелчка левой кнопки мышки
    /// </summary>
    internal sealed class MouseLClickExecutor : BaseExecutor
    {
        private IMouse Mouse { get; set; } = new Mouse();
        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <returns></returns>
        public override Boolean Invoke(ListAction actions)
        {
            Print(new { Date = DateTime.Now.ToString(), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);
            try
            {
                // Для данного действия не поддерживается список действий actions игнорируем, знаю что косяк архитектуры
                // Но возможно потом будут дабл клики, итд
                Mouse.MouseLeftCl();
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(), ex });
                return false;
            }
            return true;
        }
    }
}