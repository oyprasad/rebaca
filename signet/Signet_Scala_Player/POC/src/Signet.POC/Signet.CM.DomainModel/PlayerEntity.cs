namespace Signet.CM.DomainModel
{
    using Signet.Core.DomainObjects;

    public class PlayerEntity : IPlayerEntity
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public bool? IsEnabled { get; set; }

        public bool? IsPreviewPlayer { get; set; }

        public string Mac { get; set; }

        public string Name { get; set; }

        public PlayerType? Type { get; set; }

        public string UniqueId { get; set; }
    }
}