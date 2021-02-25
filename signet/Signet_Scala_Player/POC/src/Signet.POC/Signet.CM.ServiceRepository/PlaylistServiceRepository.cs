namespace Signet.CM.ServiceRepository
{
    using System;
    using System.Collections.Generic;
    using Signet.CM.Service.Stubs.Playlist;
    using Signet.Core.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Translators;

    public abstract class PlaylistServiceRepository : BaseServiceRepository, IPlaylistServiceRepository
    {
        protected PlaylistServiceRepository()
        {
            base.serviceInstance = new playlistService();
        }

        protected override void ConfigureEndpoint(IServiceConnectionConfiguration config)
        {
            base.serviceInstance.Url = config.PlaylistEndpointUrl;
        }

        public IList<IPlaylistItemEntity> GetAllPlaylistItems(int playlistId)
        {
            playlistItemTO[] playlistItemArray = null;
            IList<IPlaylistItemEntity> playlistItems = new List<IPlaylistItemEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    playlistItemArray = this.GetAllPlaylistItemsImpl(playlistId);
                });
                playlistItemArray.ForEach<playlistItemTO>(delegate(playlistItemTO p)
                {
                    playlistItems.Add(this.GetTranslator<IPlaylistItemEntity, playlistItemTO>().Translate<IPlaylistItemEntity>(p));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return playlistItems;
        }

        protected abstract playlistItemTO[] GetAllPlaylistItemsImpl(int playlistId);

        public IList<IPlaylistEntity> GetAllPlaylists(int maxResults = 0)
        {
            playlistTO[] playlistArray = null;
            IList<IPlaylistEntity> playlists = new List<IPlaylistEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    playlistArray = this.GetAllPlaylistsImpl(maxResults);
                });
                playlistArray.ForEach<playlistTO>(delegate(playlistTO p)
                {
                    playlists.Add(this.GetTranslator<IPlaylistEntity, playlistTO>().Translate<IPlaylistEntity>(p));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return playlists;
        }

        protected abstract playlistTO[] GetAllPlaylistsImpl(int maxResults);

        public IList<ITimeScheduleEntity> GetAllTimeSchedulesForPlaylistItem(int playlistItemId)
        {
            IEnumerable<timeScheduleTO> timeScheduleArray = null;
            IList<ITimeScheduleEntity> timeSchedules = new List<ITimeScheduleEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    timeScheduleArray = this.GetAllTimeSchedulesForPlaylistItemImpl(playlistItemId);
                });
                timeScheduleArray.ForEach<timeScheduleTO>(delegate(timeScheduleTO t)
                {
                    timeSchedules.Add(this.GetTranslator<ITimeScheduleEntity, timeScheduleTO>().Translate<ITimeScheduleEntity>(t));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return timeSchedules;
        }

        protected abstract timeScheduleTO[] GetAllTimeSchedulesForPlaylistItemImpl(int playlistItemId);

        public IPlaylistEntity GetPlaylistById(int playlistId)
        {
            playlistTO playlistTO = null;
            IPlaylistEntity playlist = null;
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    playlistTO = this.GetPlaylistByIdImpl(playlistId);
                });
                playlist = base.GetTranslator<IPlaylistEntity, playlistTO>().Translate<IPlaylistEntity>(playlistTO);
            }
            finally
            {
                base.EndProfile();
            }
            return playlist;
        }

        protected abstract playlistTO GetPlaylistByIdImpl(int playlistId);

        protected playlistService getService()
        {
            if (base.serviceInstance == null)
            {
                throw new InvalidOperationException("CM Playlist Service instance not created");
            }
            return (base.serviceInstance as playlistService);
        }

        protected override void InitTranslators()
        {
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Playlist"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("PlaylistItem"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("TimeSchedule"));
        }
    }
}