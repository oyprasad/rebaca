namespace Signet.CM.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Signet.Core.DomainObjects;

    public class TimeslotEntity : ITimeslotEntity
    {
        public Color? Color { get; set; }

        public string Description { get; set; }

        public bool DuckAudio { get; set; }

        public DateTime EndDateTime { get; set; }

        public int Id { get; set; }

        public bool IsCharted { get; set; }

        public bool IsLocked { get; set; }

        public MonthPeriod MonthPeriod { get; set; }

        public string Name { get; set; }

        public bool PlayFullScreen { get; set; }

        public int? PlaylistId { get; set; }

        public TimeslotPriorityEnum Priority { get; set; }

        public RecurrencePattern RecurrencePattern { get; set; }

        public int SortOrder { get; set; }

        public DateTime StartDateTime { get; set; }

        public IList<DayOfWeek> Weekdays { get; set; }

        public IPlaylistEntity Playlist { get; set; }

        public int FrameId { get; set; }

        public IFrameEntity Frame{ get; set; }
    }
}