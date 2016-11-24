using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.Helpers;
using Core.Plugin;
using LogWrapper;

namespace Core
{
    public static class Assemblys
    {
        /// <summary>
        /// Список сборок в плагинах
        /// </summary>
        internal static readonly List<Assembly> AssemblyPluginsList = new List<Assembly>();

        /// <summary>
        /// Список плагинов
        /// </summary>
        public static readonly List<IPlugin> PluginsList = new List<IPlugin>();

        /// <summary>
        /// Список плагинов
        /// </summary>
        internal static readonly List<Type> PluginsTypeList = new List<Type>();

        private static List<Type> _typeList;

        internal static List<Type> TypeList
            => _typeList ?? (_typeList = Assembly.GetExecutingAssembly().GetTypes().ToList());

        /// <summary>
        /// Загрузка плагинов и их типов
        /// </summary>
        internal static void LoadPlugins()
        {
            //Если выбран путь игнорирования плагинов
            if (!AppConfig.Instance.LoadPlugin)
            {
                Log.WriteLine(new { Message = "Загрузка плагинов была отключена" });
                return;
            }
            //При повторном вызове не загружаем всё снова
            if (AssemblyPluginsList.Count > 0 || PluginsList.Count > 0)
                return;
            //Получаем путь до программы
            var sPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            //Проходим циклом по всем файлом с расширением .dll
            DirectoryHelper.CreateDirectory($"{sPath}/Plugins");
            foreach (var f in Directory.GetFiles($"{sPath}/Plugins", "*.dll"))
            {
                var assem = Assembly.LoadFrom(f);
                if (!AssemblyPluginsList.Contains(assem))
                    AssemblyPluginsList.Add(assem);
                try
                {
                    var tm = assem.GetTypes().FirstOrDefault(t => t.GetInterfaces().Any(i => i == typeof(IPlugin)));
                    try
                    {
                        //Подписываемся на события плагина и добавляем его в список плагинов
                        IPlugin p = (IPlugin)Activator.CreateInstance(tm);
                        if (!PluginsList.Contains(p))
                        {
                            //Добавляем плагины
                            if (!AppConfig.Instance.PrioritetList.Contains(tm.Assembly.GetName().Name))
                                AppConfig.Instance.PrioritetList.Add(tm.Assembly.GetName().Name);
                            PluginsList.Add(p);
                            PluginsTypeList.AddRange(assem.GetTypes());
                        }
                        Log.WriteLine(new { message = $"Add plagin {p.Name}" });
                    }
                    catch { }

                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex, LogLevel.Error);
                }
                foreach (var s in AppConfig.Instance.PrioritetList.Where(s => s != Assembly.GetExecutingAssembly().GetName().Name))
                {
                    if (PluginsList.Any(x => x.GetType().Assembly.GetName().Name == s))
                        continue;
                    AppConfig.Instance.PrioritetList.Remove(s);
                }
            }
        }

        //надо подумать, нужно ли оно....
        //
        //public static List<Type>
    }
}
