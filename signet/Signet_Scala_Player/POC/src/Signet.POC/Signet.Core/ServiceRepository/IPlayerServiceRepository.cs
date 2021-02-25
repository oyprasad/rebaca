namespace Signet.Core.ServiceRepository
{
    using System.Collections.Generic;
    using Signet.Core.DomainObjects;

    public interface IPlayerServiceRepository : IServiceRepository
    {
        IList<IPlayerDisplayEntity> GetAllPlayerDisplays(int playerId);

        IList<IPlayerEntity> GetAllPlayers(int maxResults = 0);
    }
}