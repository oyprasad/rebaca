namespace Signet.CM.DomainModel
{
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public class PlaylistEntity : IPlaylistEntity
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public bool? IsCharted { get; set; }

        public IList<IPlaylistItemEntity> Items { get; set; }

        public string Name { get; set; }

        public PlaylistPickPolicy? PickPolicy { get; set; }

        public ShuffleNoRepeatEnum? ShuffleNoRepeatType { get; set; }

        public int? ShuffleNoRepeatWithin { get; set; }

        public PlaylistType? Type { get; set; }

        public int TimeslotId { get; set; }

        public ITimeslotEntity Timeslot { get; set; }
    }
}