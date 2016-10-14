using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.Helpers;
using LogWrapper;
using Plugin;

namespace Core
{
    internal static class Assemblys
    {
        /// <summary>
        /// Список сборок в плагинах
        /// </summary>
        internal static readonly List<Assembly> AssemblyPluginsList = new List<Assembly>();
        /// <summary>
        /// Список плагинов
        /// </summary>
        internal static readonly List<IPlugin> PluginsList = new List<IPlugin>();

        private static List<Type> _typeList;
        internal static List<Type> TypeList => _typeList ?? (_typeList = Assembly.GetExecutingAssembly().GetTypes().ToList());

        /// <summary>
        /// Загрузка плагинов и их типов
        /// </summary>
        public static void LoadPlugins()
        {
            //Если выбран путь игнорирования плагинов
            if (!AppConfig.LoadPlugin)
            {
                Log.WriteLine(new {Message = "Загрузка плагинов была отключена"});
                return;
            }
            //При повторном вызове не загружаем всё снова
            if (AssemblyPluginsList.Count > 0)
                return;
            //Получаем путь до программы
            var sPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName); ;

            //Проходим циклом по всем файлом с расширением .dll
            DirectoryHelper.CreateDirectory($"{sPath}/Plugins");
            foreach (var f in Directory.GetFiles($"{sPath}/Plugins", "*.dll"))
            {
                var assem = Assembly.LoadFrom(f);
                if (!AssemblyPluginsList.Contains(assem))
                    AssemblyPluginsList.Add(assem);

                foreach (var t in AssemblyPluginsList.Select(x => x.GetTypes().FirstOrDefault(t => t.GetInterfaces().Any(i => i == typeof(IPlugin)))).Where(tt => tt != null))
                {
                    try
                    {
                        //Подписываемся на события плагина и добавляем его в список плагинов
                        IPlugin p = (IPlugin)Activator.CreateInstance(t);
                        if (!PluginsList.Contains(p))
                            PluginsList.Add(p);
                        Log.WriteLine(new { message = $"Add plagin {p.Name}" });
                    }
                    catch { }
                }
            }
        }
    }
}
