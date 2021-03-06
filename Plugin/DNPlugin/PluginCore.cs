﻿using System;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.Plugin;
using ImgComparer.ActionObjects;
using ImgComparer.Factories;

namespace ImgComparer
{
    public class PluginCore : IPlugin
    {
        public String Name => "ImageComparer";

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public IExecutorResult Invoke(BotAction actions, IExecutorResult previousResult = null)
        {
            if (actions?.ActionType != ActionType.PluginAct)
                throw new NotSupportedException($"ActionType is not PluginAct");
            if (!(previousResult is BitmapExecutorResult))
                throw new NotSupportedException($"{nameof(previousResult)} is not {nameof(BitmapExecutorResult)}");
            var act = actions.SubActions.Cast<PluginCompareImgAction>().First();

            var img1 = ((BitmapExecutorResult)previousResult).Bitmap;

            IRecogn recogn = new Recogn();

            return new BooleanExecutorResult(recogn.ImgRecogn(img1, act.SamplePath));
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Отображать меню или нет?
        /// </summary>
        public Boolean ShowMenue => true;

        /// <summary>
        /// Меню которое предоставляет бот
        /// </summary>
        public PluginMenuItemModel Menu { get; }

        public PluginCore()
        {
            Menu = new PluginMenuItemModel
            {
                Title = "Показать настройки распознователя",
                Command = () =>
                {
                    var wf = WindowFactory.CreateMainWindow(null);
                    wf.ShowDialog();
                }
            };
        }
    }
}
