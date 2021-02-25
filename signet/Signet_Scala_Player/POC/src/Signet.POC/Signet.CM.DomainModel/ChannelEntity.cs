namespace Signet.CM.DomainModel
{
    using Signet.Core.DomainObjects;
    using System.Collections.Generic;

    public class ChannelEntity : IChannelEntity
    {
        public string Description { get; set; }

        public IFramesetEntity Frameset { get; set; }

        public int? FramesetId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<IFrameEntity> Frames { get; set; }
    }
}