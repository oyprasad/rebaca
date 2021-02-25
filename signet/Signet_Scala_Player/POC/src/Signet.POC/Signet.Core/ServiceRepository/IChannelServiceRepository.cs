namespace Signet.Core.ServiceRepository
{
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public interface IChannelServiceRepository : IServiceRepository
    {
        IList<IChannelEntity> GetAllChannels(int maxResults = 0);

        IList<IFramesetEntity> GetAllFramesets(int maxResults = 0);

        IList<IFrameEntity> GetAllFramesForChannel(int channelId);

        IList<ITimeslotEntity> GetAllTimeslotsForChannelAndFrame(int channelId, int frameId);

        IFramesetEntity GetFramesetForChannel(int channelId);
    }
}