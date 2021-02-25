namespace Signet.Core.DomainObjects
{
    using System.Collections.Generic;
    using System.Drawing;

    public interface IFrameEntity : IEntity
    {
        int? ChannelId { get; set; }

        Color? Color { get; set; }

        bool? IsAudio { get; set; }

        bool? IsCharted { get; set; }

        string Name { get; set; }

        Point? Position { get; set; }

        AutoScaleEnum ScaleMode { get; set; }

        Size? Size { get; set; }

        int SortOrder { get; set; }

        IList<ITimeslotEntity> TimeSlots { get; set; }
    }
}