namespace Signet.Core.Dependency
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;

    public interface IDependencyResolver : IDisposable
    {
        IUnityContainer GetContainer();

        void Inject<T>(T existing);

        void RegisterInstance<T>(T instance);

        T Resolve<T>();

        T Resolve<T>(IDictionary<string, object> constructionParams);

        T Resolve<T>(string name);

        T Resolve<T>(Type type);

        T Resolve<T>(string name, IDictionary<string, object> constructionParams);

        T Resolve<T>(Type type, string name);

        IEnumerable<T> ResolveAll<T>();
    }
}