namespace Signet.Core.Unity
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.Utils;

    public class UnityDependencyResolver : DisposableResource, IDependencyResolver, IDisposable
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver()
            : this(new UnityContainer())
        {
            try
            {
                UnityConfigurationSection configurationSection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                if (configurationSection == null)
                {
                    throw new InvalidOperationException("No unity configuration was found, could not instansiate container");
                }
                configurationSection.Configure(this._container);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._container.Dispose();
            }
            base.Dispose(disposing);
        }

        public IUnityContainer GetContainer()
        {
            return this._container;
        }

        public void Inject<T>(T existing)
        {
            this._container.BuildUp<T>(existing, new ResolverOverride[0]);
        }

        public void RegisterInstance<T>(T instance)
        {
            this._container.RegisterInstance<T>(instance);
        }

        public T Resolve<T>()
        {
            return this._container.Resolve<T>(new ResolverOverride[0]);
        }

        public T Resolve<T>(IDictionary<string, object> constructionParams)
        {
            if (constructionParams.Count == 0)
            {
                return this.Resolve<T>();
            }
            ParameterOverrides ctorParams = new ParameterOverrides();
            foreach (KeyValuePair<string, object> kvp in constructionParams)
            {
                ctorParams.Add(kvp.Key, kvp.Value);
            }
            return this._container.Resolve<T>(new ResolverOverride[] { ctorParams });
        }

        public T Resolve<T>(string name)
        {
            return this._container.Resolve<T>(name, new ResolverOverride[0]);
        }

        public T Resolve<T>(Type type)
        {
            return (T)this._container.Resolve(type, new ResolverOverride[0]);
        }

        public T Resolve<T>(string name, IDictionary<string, object> constructionParams)
        {
            if (constructionParams.Count == 0)
            {
                return this.Resolve<T>();
            }
            ParameterOverrides ctorParams = new ParameterOverrides();
            foreach (KeyValuePair<string, object> kvp in constructionParams)
            {
                ctorParams.Add(kvp.Key, kvp.Value);
            }
            return this._container.Resolve<T>(name, new ResolverOverride[] { ctorParams });
        }

        public T Resolve<T>(Type type, string name)
        {
            return (T)this._container.Resolve(type, name, new ResolverOverride[0]);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this._container.ResolveAll<T>(new ResolverOverride[0]);
        }
    }
}