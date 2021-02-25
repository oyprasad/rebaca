namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Playlist;
    using Signet.Core.DomainObjects;

    public class PlaylistItemTranslator : EntityMapperTranslator<IPlaylistItemEntity, playlistItemTO>
    {
        protected override playlistItemTO BusinessToService(IPlaylistItemEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IPlaylistItemEntity ServiceToBusiness(playlistItemTO value)
        {
            return new PlaylistItemEntity 
            { 
                Id = value.id, 
                SortOrder = new int?(value.sortOrder), 
                MediaId = new int?(value.mediaId), 
                SubPlaylistId = new int?(value.playlistId), 
                DurationInSeconds = new int?(value.duration), 
                UseValidRange = new bool?(value.useValidRange), 
                ValidFrom = new DateTime?(value.startValidDate), 
                ValidTo = new DateTime?(value.endValidDate), 
                MeetAllConditions = new bool?(value.meetAllConditions), 
                ReservationId = value.reservationId, 
                SubPlaylistPickPolicy = new PlaylistPickPolicy?((PlaylistPickPolicy)value.subPlaylistPickPolicy), 
                PlayFullScreen = new bool?(value.playFullscreen), 
                StartTime = getTimeSpan(value.startTime), 
                EndTime = getTimeSpan(value.endTime)
            };
        }

        private TimeSpan? getTimeSpan(string value)
        {
            TimeSpan interval;
            if (TimeSpan.TryParseExact(value, @"hh\:mm\:ss\.ff", null, out interval))
                return interval;
            else
                return default(TimeSpan?);
        }
    }
}