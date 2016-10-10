using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace Core
{
    internal static class Assemblys
    {
        internal static readonly List<Assembly> AssemblyPluginsList = new List<Assembly>();
        internal static readonly List<IPlugin> PluginsList = new List<IPlugin>();

        private static List<Type> _typeList;
        internal static List<Type> TypeList => _typeList ?? (_typeList = Assembly.GetExecutingAssembly().GetTypes().ToList());
    }
}
