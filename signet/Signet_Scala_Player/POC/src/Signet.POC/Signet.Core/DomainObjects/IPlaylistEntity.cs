namespace Signet.Core.DomainObjects
{
    using System.Collections.Generic;

    public interface IPlaylistEntity : IEntity
    {
        string Description { get; set; }

        bool? IsCharted { get; set; }

        IList<IPlaylistItemEntity> Items { get; set; }

        string Name { get; set; }

        PlaylistPickPolicy? PickPolicy { get; set; }

        ShuffleNoRepeatEnum? ShuffleNoRepeatType { get; set; }

        int? ShuffleNoRepeatWithin { get; set; }

        PlaylistType? Type { get; set; }

        int TimeslotId { get; set; }

        ITimeslotEntity Timeslot { get; set; }
    }
}