namespace Signet.Core.DomainObjects
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface ITimeslotEntity : IEntity
    {
        Color? Color { get; set; }

        string Description { get; set; }

        bool DuckAudio { get; set; }

        DateTime EndDateTime { get; set; }

        bool IsCharted { get; set; }

        bool IsLocked { get; set; }

        MonthPeriod MonthPeriod { get; set; }

        string Name { get; set; }

        bool PlayFullScreen { get; set; }

        int? PlaylistId { get; set; }

        TimeslotPriorityEnum Priority { get; set; }

        RecurrencePattern RecurrencePattern { get; set; }

        int SortOrder { get; set; }

        DateTime StartDateTime { get; set; }

        IList<DayOfWeek> Weekdays { get; set; }

        IPlaylistEntity Playlist { get; set; }

        int FrameId { get; set; }

        IFrameEntity Frame { get; set; }
    }
}