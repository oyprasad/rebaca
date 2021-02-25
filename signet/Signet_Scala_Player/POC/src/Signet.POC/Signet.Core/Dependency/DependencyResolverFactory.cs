namespace Signet.Core.Dependency
{
    using System;
    using System.Configuration;

    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory()
            : this(ConfigurationManager.AppSettings["DependencyResolverTypeName"])
        {
        }

        public DependencyResolverFactory(string resolverTypeName)
        {
            this._resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public IDependencyResolver CreateInstance()
        {
            return (Activator.CreateInstance(this._resolverType) as IDependencyResolver);
        }
    }
}