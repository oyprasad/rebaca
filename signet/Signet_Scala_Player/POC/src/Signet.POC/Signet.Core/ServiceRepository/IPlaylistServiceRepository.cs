namespace Signet.Core.ServiceRepository
{
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public interface IPlaylistServiceRepository : IServiceRepository
    {
        IList<IPlaylistItemEntity> GetAllPlaylistItems(int playlistId);

        IList<IPlaylistEntity> GetAllPlaylists(int maxResults = 0);

        IList<ITimeScheduleEntity> GetAllTimeSchedulesForPlaylistItem(int playlistItemId);

        IPlaylistEntity GetPlaylistById(int playlistId);
    }
}