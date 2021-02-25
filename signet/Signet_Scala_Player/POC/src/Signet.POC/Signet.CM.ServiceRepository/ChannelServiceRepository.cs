namespace Signet.CM.ServiceRepository
{
    using System;
    using System.Collections.Generic;
    using Signet.CM.Service.Stubs.Channel;
    using Signet.Core.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Translators;

    public abstract class ChannelServiceRepository : BaseServiceRepository, IChannelServiceRepository
    {
        protected ChannelServiceRepository()
        {
            base.serviceInstance = new channelService();
        }

        protected override void ConfigureEndpoint(IServiceConnectionConfiguration config)
        {
            base.serviceInstance.Url = config.ChannelEndpointUrl;
        }

        public IList<IChannelEntity> GetAllChannels(int maxResults = 0)
        {
            channelTO[] channelArray = null;
            IList<IChannelEntity> channels = new List<IChannelEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    channelArray = this.GetAllChannelsImpl(maxResults);
                });
                channelArray.ForEach<channelTO>(delegate(channelTO c)
                {
                    channels.Add(this.GetTranslator<IChannelEntity, channelTO>().Translate<IChannelEntity>(c));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return channels;
        }

        protected abstract channelTO[] GetAllChannelsImpl(int maxResults);

        public IList<IFramesetEntity> GetAllFramesets(int maxResults = 0)
        {
            framesetTO[] framesetArray = null;
            IList<IFramesetEntity> framesets = new List<IFramesetEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    framesetArray = this.GetAllFramesetsImpl(maxResults);
                });
                framesetArray.ForEach<framesetTO>(delegate(framesetTO f)
                {
                    framesets.Add(this.GetTranslator<IFramesetEntity, framesetTO>().Translate<IFramesetEntity>(f));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return framesets;
        }

        protected abstract framesetTO[] GetAllFramesetsImpl(int maxResults);

        public IList<IFrameEntity> GetAllFramesForChannel(int channelId)
        {
            frameTO[] frameArray = null;
            IList<IFrameEntity> frames = new List<IFrameEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    frameArray = this.GetAllFramesForChannelImpl(channelId);
                });
                frameArray.ForEach<frameTO>(delegate(frameTO f)
                {
                    frames.Add(this.GetTranslator<IFrameEntity, frameTO>().Translate<IFrameEntity>(f));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return frames;
        }

        protected abstract frameTO[] GetAllFramesForChannelImpl(int channelId);

        public IList<ITimeslotEntity> GetAllTimeslotsForChannelAndFrame(int channelId, int frameId)
        {
            timeslotTO[] timeslotArray = null;
            IList<ITimeslotEntity> timeslots = new List<ITimeslotEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    timeslotArray = this.GetAllTimeslotsForChannelAndFrameImpl(channelId, frameId);
                });
                timeslotArray.ForEach<timeslotTO>(delegate(timeslotTO t)
                {
                    timeslots.Add(this.GetTranslator<ITimeslotEntity, timeslotTO>().Translate<ITimeslotEntity>(t));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return timeslots;
        }

        protected abstract timeslotTO[] GetAllTimeslotsForChannelAndFrameImpl(int channelId, int frameId);

        public IFramesetEntity GetFramesetForChannel(int channelId)
        {
            framesetTO framesetTO = null;
            IFramesetEntity frameset = null;
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    framesetTO = this.GetFramesetForChannelImpl(channelId);
                });
                frameset = base.GetTranslator<IFramesetEntity, framesetTO>().Translate<IFramesetEntity>(framesetTO);
            }
            finally
            {
                base.EndProfile();
            }
            return frameset;
        }

        protected abstract framesetTO GetFramesetForChannelImpl(int channelId);

        protected channelService getService()
        {
            if (base.serviceInstance == null)
            {
                throw new InvalidOperationException("CM Channel Service instance not created");
            }
            return (base.serviceInstance as channelService);
        }

        protected override void InitTranslators()
        {
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Channel"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Frameset"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Frame"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Timeslot"));
        }
    }
}