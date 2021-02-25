namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Playlist;
    using Signet.Core.DomainObjects;

    public class PlaylistTranslator : EntityMapperTranslator<IPlaylistEntity, playlistTO>
    {
        protected override playlistTO BusinessToService(IPlaylistEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IPlaylistEntity ServiceToBusiness(playlistTO value)
        {
            return new PlaylistEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                Description = value.description, 
                PickPolicy = new PlaylistPickPolicy?(value.pickPolicy.ToEntityEnum()), 
                IsCharted = new bool?(value.isCharted), 
                ShuffleNoRepeatWithin = new int?(value.shuffleNoRepeatWithin), 
                ShuffleNoRepeatType = new ShuffleNoRepeatEnum?(value.shuffleNoRepeatType.ToEntityEnum()), 
                Type = new PlaylistType?(value.playlistType.ToEntityEnum()) 
            };
        }
    }
}