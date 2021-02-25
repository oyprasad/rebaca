namespace Signet.Core.ServiceRepository
{
    using Signet.Core.Configuration;
    using Signet.Core.Translators;

    public interface IServiceRepository
    {
        void Configure(IServiceConnectionConfiguration config);

        IEntityTranslator GetTranslator<TTarget, TSource>();
    }
}