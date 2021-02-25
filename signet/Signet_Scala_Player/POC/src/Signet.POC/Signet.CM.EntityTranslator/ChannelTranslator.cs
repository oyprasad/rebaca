namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Channel;
    using Signet.Core.DomainObjects;

    public class ChannelTranslator : EntityMapperTranslator<IChannelEntity, channelTO>
    {
        protected override channelTO BusinessToService(IChannelEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IChannelEntity ServiceToBusiness(channelTO value)
        {
            return new ChannelEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                Description = value.description, 
                FramesetId = new int?(value.framesetId) 
            };
        }
    }
}