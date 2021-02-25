namespace Signet.CM.DomainModel
{
    using System;
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public class PlaylistItemEntity : IPlaylistItemEntity
    {
        public int? DurationInSeconds { get; set; }

        public TimeSpan? EndTime { get; set; }

        public int Id { get; set; }

        public PlaylistItemType? ItemType
        {
            get
            {
                return new PlaylistItemType?((this.MediaId.HasValue && (this.MediaId.Value != 0)) ? PlaylistItemType.Media : PlaylistItemType.SubPlaylist);
            }
        }

        public int? MediaId { get; set; }

        public bool? MeetAllConditions { get; set; }

        public bool? PlayFullScreen { get; set; }

        public int? PlaylistId { get; set; }

        public string ReservationId { get; set; }

        public int? SortOrder { get; set; }

        public TimeSpan? StartTime { get; set; }

        public int? SubPlaylistId { get; set; }

        public PlaylistPickPolicy? SubPlaylistPickPolicy { get; set; }

        public IList<ITimeScheduleEntity> TimeSchedules { get; set; }

        public bool? UseValidRange { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public IPlaylistEntity Playlist { get; set; }

        public IMediaEntity Media { get; set; }
    }
}