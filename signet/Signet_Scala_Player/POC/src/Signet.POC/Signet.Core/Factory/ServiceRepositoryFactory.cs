namespace Signet.Core.Factory
{
    using Signet.Core.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.ServiceRepository;

    public static class ServiceRepositoryFactory
    {
        private static IServiceConnectionConfiguration _config;

        public static void Configure(IServiceConnectionConfiguration config)
        {
            _config = config;
        }

        public static T GetServiceRepository<T>() where T : IServiceRepository
        {
            T instance = IoC.Resolve<T>();
            if (instance != null)
            {
                instance.Configure(_config);
            }
            return instance;
        }
    }
}