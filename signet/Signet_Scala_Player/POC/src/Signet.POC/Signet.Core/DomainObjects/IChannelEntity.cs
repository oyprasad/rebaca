using System.Collections.Generic;
namespace Signet.Core.DomainObjects
{
    public interface IChannelEntity : IEntity
    {
        string Description { get; set; }

        IFramesetEntity Frameset { get; set; }

        int? FramesetId { get; set; }

        string Name { get; set; }

        IList<IFrameEntity> Frames { get; set; }
    }
}