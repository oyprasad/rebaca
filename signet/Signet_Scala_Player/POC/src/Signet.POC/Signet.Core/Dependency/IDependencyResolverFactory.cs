namespace Signet.Core.Dependency
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}