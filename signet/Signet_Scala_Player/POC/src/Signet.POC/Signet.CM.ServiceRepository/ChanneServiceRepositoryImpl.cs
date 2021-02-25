namespace Signet.CM.ServiceRepository
{
    using Signet.CM.Service.Stubs.Channel;

    internal class ChanneServiceRepositoryImpl : ChannelServiceRepository
    {
        protected override channelTO[] GetAllChannelsImpl(int maxResults)
        {
            listResultCriteriaTO lsc = new listResultCriteriaTO
            {
                maxResults = maxResults,
                maxResultsSpecified = maxResults != 0
            };
            return base.getService().list(null, lsc);
        }

        protected override framesetTO[] GetAllFramesetsImpl(int maxResults)
        {
            listResultCriteriaTO lsc = new listResultCriteriaTO
            {
                maxResults = maxResults,
                maxResultsSpecified = maxResults != 0
            };
            return base.getService().listFramesets(null, lsc);
        }

        protected override frameTO[] GetAllFramesForChannelImpl(int channelId)
        {
            return base.getService().getFrames(channelId, true);
        }

        protected override timeslotTO[] GetAllTimeslotsForChannelAndFrameImpl(int channelId, int frameId)
        {
            return base.getService().getTimeslots(channelId, true, frameId, true);
        }

        protected override framesetTO GetFramesetForChannelImpl(int channelId)
        {
            return base.getService().getFrameset(channelId, true);
        }
    }
}