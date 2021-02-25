namespace Signet.Core.ServiceRepository
{
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public interface IMediaServiceRepository : IServiceRepository
    {
        IList<IMediaEntity> GetAllMedia(int maxResults = 0);

        IMediaEntity GetMediaById(int mediaId);
    }
}