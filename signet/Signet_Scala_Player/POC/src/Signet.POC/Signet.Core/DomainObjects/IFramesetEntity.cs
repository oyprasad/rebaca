namespace Signet.Core.DomainObjects
{
    using System.Collections.Generic;
    using System.Drawing;

    public interface IFramesetEntity : IEntity
    {
        int? ChannelId { get; set; }

        IList<IFrameEntity> Frames { get; set; }

        bool? IsDefaulSet { get; set; }

        string Name { get; set; }

        Size? ScreenSize { get; set; }
    }
}