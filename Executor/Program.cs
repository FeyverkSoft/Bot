using System;
using System.Collections.Generic;
using System.Threading;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Executor
{
    class Program
    {

        static void Main(string[] args)
        {
            IExecutiveCore core = new DefaultExecutiveCore();
            core.OnPrintMessageEvent += Console.WriteLine;

#if !DEBUG
            core.Run(DnConf());
#else

            Thread.Sleep(8000);
            core.Run(new ListBotAction() {
                new BotAction(
                 ActionType.ExpectWindow,
                 new ListAct { new ExpectWindowAct("AkelPad", true) }),
                new BotAction(ActionType.MouseSetPos, new MouseSetPosAct(100, 100, true)),
                });

#endif

            /* 
             //mouse.MouseMove(100, 100);
             keyBoard.PressKey(KeyCode.I);*/
            //mouse.MouseRightCl();
            Console.ReadLine();
        }

        static Config DnConf()
        {
            IConfigReader cr = new ConfigReader("test.jsn");
#if DEBUG
            var list = new List<BotAction>
            {
                new BotAction(
                    ActionType.Loop,
                    new ListAct { new LoopAct(20, new List<BotAction>()
                        {
                            new BotAction(
                                        ActionType.ExpectWindow,
                                        new ListAct { new ExpectWindowAct("DragonNest", true) }),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(6000, 500)}),
                                    //нет смысла, навёл, и само пускай тыкает
                                    //new BotAction(
                                    //    ActionType.MouseMove,
                                    //    new List<MouseMoveAct> { new MouseMoveAct(10, 150) }),
                                    //new BotAction(
                                    //    ActionType.Sleep,
                                    //    new ListAction  { new SleepAct(1000, 500)}),
                                    new BotAction(
                                        ActionType.MouseLClick),
                                    new BotAction(
                                        ActionType.MouseMove,
                                        new List<MouseMoveAct> { new MouseMoveAct(200, -190) }),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(1000, 500)}),
                                    new BotAction(
                                        ActionType.MouseLClick),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(600, 500)}),
                                    new BotAction(
                                        ActionType.MouseMove,
                                        new List<MouseMoveAct> { new MouseMoveAct(0, 380) }),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(600, 500)}),
                                    new BotAction(
                                        ActionType.MouseLClick),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(12600, 2000)}),
                                    new BotAction(
                                        ActionType.MouseLClick),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(1800000, 62000)}),
                                    new BotAction(
                                        ActionType.MouseLClick),
                                    new BotAction(
                                        ActionType.Sleep,
                                        new ListAct  { new SleepAct(12600, 2000)}),
                        })}),
            };
            var conf = new Config(list);

            var conf1 = conf.ToJson();
            var conf2 = conf1.ParseJson<Config>();
            cr.Save(conf);
#endif
            return cr.Load();
        }
    }
}
