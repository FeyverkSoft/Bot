﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;
using Core.Core;

namespace Core
{
    /// <summary>
    /// Фабрика ядра
    /// </summary>
    public static class CoreFactory
    {
        /// <summary>
        /// Получить экземпляр ядра по умолчанию
        /// </summary>
        /// <returns></returns>
        public static IExecutiveCore GetCore()
        {
            if (AppConfig.DefaultCore)
                return new DefaultExecutiveCore(new DefaultActionFactory());
            return new DefaultExecutiveCore(new DefaultActionFactory());
        }
    }
}