namespace Signet.CM.DomainModel
{
    using System.Collections.Generic;
    using System.Drawing;
    using Signet.Core.DomainObjects;

    public class FrameEntity : IFrameEntity
    {
        public int? ChannelId { get; set; }

        public Color? Color { get; set; }

        public int Id { get; set; }

        public bool? IsAudio { get; set; }

        public bool? IsCharted { get; set; }

        public string Name { get; set; }

        public Point? Position { get; set; }

        public AutoScaleEnum ScaleMode { get; set; }

        public System.Drawing.Size? Size { get; set; }

        public int SortOrder { get; set; }

        public IList<ITimeslotEntity> TimeSlots { get; set; }
    }
}