using System;
using System.Reflection;

namespace RSCoreInterface
{
    public abstract class RSCoreProvider
    {
        protected RSCoreProvider()
        {
            return;
        }

        public static IRSCore RSCore
        {
            get
            {
                lock (_lock) {
                    // We don't need yet to handle several instances. So we create the
                    // core instance once on first retrieval attempt. 
                    if (null == _instance) { _instance = InstanciateCore(); }
                    return _instance;
                }
            }
        }

        private static IRSCore InstanciateCore()
        {
            try {
                // For time being we have a single implementation hence we hardcode the
                // names.
                Assembly assembly = Assembly.Load("RSCore");
                Type providerType = assembly.GetType("RSCore.RSCoreProviderImpl", true);
                ConstructorInfo constructor = providerType
                    .GetConstructor(BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public,
                        null, Type.EmptyTypes, null);
                object rawInstance = constructor.Invoke(null);
                IRSCore result = rawInstance as IRSCore;
                providerType
                    .GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)
                    .Invoke(result, null);
                return result;
            }
            catch (Exception e) {
                throw new RSCoreInternalErrorException(Messages.CoreInstanciationFailure, e);
            }
        }

        private static IRSCore _instance;
        private static object _lock = new object();
    }
}
