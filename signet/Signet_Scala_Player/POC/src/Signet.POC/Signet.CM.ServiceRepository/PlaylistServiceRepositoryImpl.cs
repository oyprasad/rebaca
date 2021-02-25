namespace Signet.CM.ServiceRepository
{
    using Signet.CM.Service.Stubs.Playlist;

    internal class PlaylistServiceRepositoryImpl : PlaylistServiceRepository
    {
        protected override playlistItemTO[] GetAllPlaylistItemsImpl(int playlistId)
        {
            return base.getService().getPlaylistItems(playlistId, true);
        }

        protected override playlistTO[] GetAllPlaylistsImpl(int maxResults)
        {
            listResultCriteriaTO lsc = new listResultCriteriaTO
            {
                maxResults = maxResults,
                maxResultsSpecified = maxResults != 0
            };
            return base.getService().list(null, lsc);
        }

        protected override timeScheduleTO[] GetAllTimeSchedulesForPlaylistItemImpl(int playlistItemId)
        {
            return base.getService().getTimeSchedules(playlistItemId, true);
        }

        protected override playlistTO GetPlaylistByIdImpl(int playlistId)
        {
            return base.getService().get(playlistId, true);
        }
    }
}