namespace Signet.CM.DomainModel
{
    using Signet.Core.DomainObjects;

    public class PlayerDisplayEntity : IPlayerDisplayEntity
    {
        public int? ChannelId { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int? PlayerId { get; set; }

        public int? ScreenCounter { get; set; }
    }
}