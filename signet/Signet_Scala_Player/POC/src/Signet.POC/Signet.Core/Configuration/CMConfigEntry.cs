namespace Signet.Core.Configuration
{
    using System.Configuration;

    public class CMConfigEntry : ConfigurationElement
    {
        [ConfigurationProperty("channelEndpoint", IsRequired = true)]
        public string ChannelEndpoint
        {
            get
            {
                return (string)base["channelEndpoint"];
            }
            set
            {
                base["channelEndpoint"] = value;
            }
        }

        [ConfigurationProperty("environment", DefaultValue = "Dev", IsRequired = false)]
        public EnvironmentOption Environment
        {
            get
            {
                return (EnvironmentOption)base["environment"];
            }
            set
            {
                base["environment"] = value;
            }
        }

        [ConfigurationProperty("mediaEndpoint", IsRequired = true)]
        public string MediaEndpoint
        {
            get
            {
                return (string)base["mediaEndpoint"];
            }
            set
            {
                base["mediaEndpoint"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)base["password"];
            }
            set
            {
                base["password"] = value;
            }
        }

        [ConfigurationProperty("playerEndpoint", IsRequired = true)]
        public string PlayerEndpoint
        {
            get
            {
                return (string)base["playerEndpoint"];
            }
            set
            {
                base["playerEndpoint"] = value;
            }
        }

        [ConfigurationProperty("playlistEndpoint", IsRequired = true)]
        public string PlaylistEndpoint
        {
            get
            {
                return (string)base["playlistEndpoint"];
            }
            set
            {
                base["playlistEndpoint"] = value;
            }
        }

        [ConfigurationProperty("rootUrl", IsRequired = true)]
        public string RootUrl
        {
            get
            {
                return (string)base["rootUrl"];
            }
            set
            {
                base["rootUrl"] = value;
            }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get
            {
                return (string)base["username"];
            }
            set
            {
                base["username"] = value;
            }
        }

        public enum EnvironmentOption
        {
            Dev,
            SystemTest,
            IntegrationTest,
            Live,
            Prod
        }
    }
}