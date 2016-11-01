using System;
using System.Collections.Generic;
using System.Threading;
using Core;
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
            IExecutiveCore core = CoreFactory.GetCore();
            core.OnPrintMessageEvent += Console.WriteLine;

#if !DEBUG
            Console.WriteLine(args?.ToJson());
            core.Run(args?.Length == 0 ? DnConf() : DnConf(args[0]));
#else
            var cr = ConfigReaderFactory.Get<Config>();
            var list = new ListBotAction
            {
                new BotAction(ActionType.GetMousePos),
                new BotAction(ActionType.Label, new LabelAct("test")),
                new BotAction(ActionType.GetObject, new GetObjectAct()),
                new BotAction(ActionType.Sleep, new SleepAct(2500)),
                new BotAction(ActionType.GOTO, new GoToAct("test")),
                new BotAction(ActionType.If, new IfAction(nameof(BaseExecutorResult), "sd","xxxx","yyyy"))
            };
            var conf = new Config(list);
            cr.Save(conf, "test22.jsn");
            var d = cr.Load("test22.jsn");
            d = d;
            Thread.Sleep(4000);
            core.Run(conf);

#endif

            /* 
             //mouse.MouseMove(100, 100);
             keyBoard.PressKey(KeyCode.I);*/
            //mouse.MouseRightCl();
            Console.ReadLine();
        }

        static Config DnConf(String path = "test.jsn")
        {
            var cr = ConfigReaderFactory.Get<Config>();
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
            cr.Save(conf, path);
#endif
            return cr.Load(path);
        }
    }
}
