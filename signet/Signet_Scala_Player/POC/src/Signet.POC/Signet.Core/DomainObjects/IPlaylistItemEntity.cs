namespace Signet.Core.DomainObjects
{
    using System;
    using System.Collections.Generic;

    public interface IPlaylistItemEntity : IEntity
    {
        int? DurationInSeconds { get; set; }

        TimeSpan? EndTime { get; set; }

        PlaylistItemType? ItemType { get; }

        int? MediaId { get; set; }

        bool? MeetAllConditions { get; set; }

        bool? PlayFullScreen { get; set; }

        int? PlaylistId { get; set; }

        string ReservationId { get; set; }

        int? SortOrder { get; set; }

        TimeSpan? StartTime { get; set; }

        int? SubPlaylistId { get; set; }

        PlaylistPickPolicy? SubPlaylistPickPolicy { get; set; }

        IList<ITimeScheduleEntity> TimeSchedules { get; set; }

        bool? UseValidRange { get; set; }

        DateTime? ValidFrom { get; set; }

        DateTime? ValidTo { get; set; }

        IPlaylistEntity Playlist { get; set; }

        IMediaEntity Media { get; set; }
    }
}