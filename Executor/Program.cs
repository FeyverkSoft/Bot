using System;
using System.Collections.Generic;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Helpers;
using Core.Handlers;
using System.Threading;
using Core.Core;

namespace Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            IMouse mouse = new Mouse();
            IKeyBoard keyBoard = new KeyBoard();
            IConfigReader cr = new ConfigReader("test.jsn");
            var list = new List<BotAction>
            {
                new BotAction(
                    ActionType.ExpectWindow,
                    new ListAction { new ExpectWindowAct("DragonNest", true) }),
                new BotAction(
                    ActionType.Sleep,
                    new ListAction  { new SleepAct(6000, 500)}),
                new BotAction(
                    ActionType.KeyBoard,
                    new ListAction { new KeyBoardAct(KeyCode.Up)}),
                new BotAction(
                    ActionType.Sleep,
                    new List<SleepAct>  { new SleepAct(1000, 500)}),
                new BotAction(
                    ActionType.MouseMove,
                    new List<MouseMoveAct> { new MouseMoveAct(100, 100) }),
                new BotAction(
                    ActionType.KeyBoard,
                    new ListAction { new KeyBoardAct(KeyCode.I)}),
                new BotAction(
                    ActionType.Sleep,
                    new List<SleepAct>  { new SleepAct(3000, 500)}),
                new BotAction(
                    ActionType.MouseMove,
                    new List<MouseMoveAct> { new MouseMoveAct(150, 120) }),
                new BotAction(
                    ActionType.KeyBoard,
                    new ListAction { new KeyBoardAct(KeyCode.I)}),

                //new BotAction(
                //    ActionType.Loop,
                //    new ListAction { new LoopAct(10, new List<BotAction>()
                //        {
                //            new BotAction(
                //            ActionType.MouseMove,
                //            new List<MouseMoveAct> { new MouseMoveAct(100, 100) })
                //        })}),
            };
            var conf = new Config(list);
            var conf1 = conf.ToJson();
            var conf2 = conf1.ParseJson<Config>();
            cr.Save(conf);
            var loads = cr.Load();

            IExecutiveCore core = new DefaultExecutiveCore();
            core.OnPrintMessageEvent += (message) => Console.WriteLine(message);

            Thread.Sleep(5000);
            core.Run(conf);

            /* 
             //mouse.MouseMove(100, 100);
             keyBoard.PressKey(KeyCode.I);*/
            //mouse.MouseRightCl();
            Console.ReadLine();
        }
    }
}
