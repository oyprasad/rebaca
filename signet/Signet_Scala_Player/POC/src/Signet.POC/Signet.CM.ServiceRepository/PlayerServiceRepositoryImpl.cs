namespace Signet.CM.ServiceRepository
{
    using Signet.CM.Service.Stubs.Player;

    internal class PlayerServiceRepositoryImpl : PlayerServiceRepository
    {
        protected override playerDisplayTO[] GetAllPlayerDisplaysImpl(int playerId)
        {
            return base.getService().getPlayerDisplays(playerId, true);
        }

        protected override playerTO[] GetAllPlayersImpl(int maxResults)
        {
            listResultCriteriaTO lsc = new listResultCriteriaTO
            {
                maxResults = maxResults,
                maxResultsSpecified = maxResults != 0
            };
            return base.getService().list(null, lsc);
        }
    }
}