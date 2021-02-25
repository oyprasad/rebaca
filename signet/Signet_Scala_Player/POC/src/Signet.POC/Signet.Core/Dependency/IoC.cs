namespace Signet.Core.Dependency
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    public static class IoC
    {
        private static IDependencyResolver _resolver;

        public static IUnityContainer GetContainer()
        {
            return _resolver.GetContainer();
        }

        public static void InitializeWith(IDependencyResolverFactory factory)
        {
            _resolver = factory.CreateInstance();
        }

        public static void Inject<T>(T existing)
        {
            _resolver.Inject<T>(existing);
        }

        public static void Register<T>(T instance)
        {
            _resolver.RegisterInstance<T>(instance);
        }

        public static void Reset()
        {
            if (_resolver != null)
            {
                _resolver.Dispose();
            }
        }

        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        public static T Resolve<T>(IDictionary<string, object> constructionParams)
        {
            return _resolver.Resolve<T>(constructionParams);
        }

        public static T Resolve<T>(string name)
        {
            return _resolver.Resolve<T>(name);
        }

        public static T Resolve<T>(Type type)
        {
            return _resolver.Resolve<T>(type);
        }

        public static T Resolve<T>(string name, IDictionary<string, object> constructionParams)
        {
            return _resolver.Resolve<T>(name, constructionParams);
        }

        public static T Resolve<T>(Type type, string name)
        {
            return _resolver.Resolve<T>(type, name);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }
    }
}