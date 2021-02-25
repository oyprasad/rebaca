namespace Signet.Core.DomainObjects
{
    using System;
    using System.Collections.Generic;

    public interface ITimeScheduleEntity : IEntity
    {
        DateTime EndTime { get; set; }

        int SortOrder { get; set; }

        DateTime StartTime { get; set; }

        IList<DayOfWeek> Weekdays { get; set; }

        int PlayListItemId { get; set; }

        IPlaylistItemEntity PlaylistItem { get; set; }
    }
}