namespace Signet.Core.Utils
{
    using Signet.Core.Dependency;
    using Signet.Core.Task;
    using Signet.Core.Extensions;

    public static class Bootstrapper
    {
        public static void Init()
        {
            IoC.InitializeWith(new DependencyResolverFactory());
        }

        public static void Run()
        {
            IoC.ResolveAll<IStartupTask>().ForEach(t => t.Execute());
        }
    }
}