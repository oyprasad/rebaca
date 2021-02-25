namespace Signet.Core.Utils
{
    using System.Linq;
    using Signet.Core.Configuration;

    public class CMConnectionInfo : IServiceConnectionConfiguration
    {
        private CMConnectionInfo()
        {
        }

        public static IServiceConnectionConfiguration getConfig(CMConfigEntry.EnvironmentOption environment)
        {
            CMConfigEntry config = (from cs in CMConfigEntriesSection.CurrentConfigurations().ConfigEntries.OfType<CMConfigEntry>()
                                    where cs.Environment == environment
                                    select cs).Single<CMConfigEntry>();
            return new CMConnectionInfo { Username = config.Username, Password = config.Password, RootUrl = config.RootUrl, ChannelEndpointUrl = config.ChannelEndpoint, MediaEndpointUrl = config.MediaEndpoint, PlayerlEndpointUrl = config.PlayerEndpoint, PlaylistEndpointUrl = config.PlaylistEndpoint };
        }

        public string ChannelEndpointUrl { get; private set; }

        public string MediaEndpointUrl { get; private set; }

        public string Password { get; private set; }

        public string PlayerlEndpointUrl { get; private set; }

        public string PlaylistEndpointUrl { get; private set; }

        public string RootUrl { get; private set; }

        public string Username { get; private set; }
    }
}