namespace Signet.CM.DomainModel
{
    using System.Collections.Generic;
    using System.Drawing;
    using Signet.Core.DomainObjects;

    public class FramesetEntity : IFramesetEntity
    {
        public int? ChannelId { get; set; }

        public IList<IFrameEntity> Frames { get; set; }

        public int Id { get; set; }

        public bool? IsDefaulSet { get; set; }

        public string Name { get; set; }

        public Size? ScreenSize { get; set; }
    }
}