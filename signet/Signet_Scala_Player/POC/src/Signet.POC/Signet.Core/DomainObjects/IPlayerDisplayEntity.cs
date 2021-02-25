namespace Signet.Core.DomainObjects
{
    public interface IPlayerDisplayEntity : IEntity
    {
        int? ChannelId { get; set; }

        string Description { get; set; }

        int? PlayerId { get; set; }

        int? ScreenCounter { get; set; }
    }
}