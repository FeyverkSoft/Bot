using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using LogWrapper;

namespace Core.Core
{
    public abstract class BaseWrapperProxy<TSource, TProxy> : RealProxy
        where TProxy : BaseWrapperProxy<TSource, TProxy>
        where TSource : class
    {
        private readonly TSource _instance;

        protected BaseWrapperProxy(TSource instance)
            : base(typeof(TSource))
        {
            _instance = instance;
        }

        public static TSource CreateInstance(TSource instance)
        {
            return (TSource)((RealProxy)
                typeof(TProxy).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(TSource) }, null)
                              .Invoke(new Object[] { instance }))
                              .GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;

            try
            {
                return new ReturnMessage(method.Invoke(_instance, methodCall.InArgs), null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }
        }
    }

    internal class CoreWrapperProxy : BaseWrapperProxy<IExecutiveCore, CoreWrapperProxy>
    {
        public CoreWrapperProxy(IExecutiveCore instance) : base(instance)
        {
        }
    }
}
