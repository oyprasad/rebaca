namespace Signet.CM.ServiceRepository
{
    using Signet.CM.Service.Stubs.Media;

    internal class MediaServiceRepositoryImpl : MediaServiceRepository
    {
        protected override mediaTO[] GetAllMediaImpl(int maxResults)
        {
            listResultCriteriaTO lsc = new listResultCriteriaTO
            {
                maxResults = maxResults,
                maxResultsSpecified = maxResults != 0
            };
            return base.getService().list(null, lsc);
        }

        protected override mediaTO GetMediaByIdImpl(int mediaId)
        {
            return base.getService().get(mediaId, true);
        }
    }
}