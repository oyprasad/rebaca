namespace Signet.Core.DomainObjects
{
    public interface IPlayerEntity : IEntity
    {
        string Description { get; set; }

        bool? IsEnabled { get; set; }

        bool? IsPreviewPlayer { get; set; }

        string Mac { get; set; }

        string Name { get; set; }

        PlayerType? Type { get; set; }

        string UniqueId { get; set; }
    }
}