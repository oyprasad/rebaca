namespace Signet.CM.DomainModel
{
    using System;
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public class TimescheduleEntity : ITimeScheduleEntity
    {
        public DateTime EndTime { get; set; }

        public int Id { get; set; }

        public int SortOrder { get; set; }

        public DateTime StartTime { get; set; }

        public IList<DayOfWeek> Weekdays { get; set; }

        public int PlayListItemId { get; set; }

        public IPlaylistItemEntity PlaylistItem { get; set; }
    }
}