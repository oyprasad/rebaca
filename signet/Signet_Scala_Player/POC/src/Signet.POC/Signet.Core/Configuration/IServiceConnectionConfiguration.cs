namespace Signet.Core.Configuration
{
    public interface IServiceConnectionConfiguration
    {
        string ChannelEndpointUrl { get; }

        string MediaEndpointUrl { get; }

        string Password { get; }

        string PlayerlEndpointUrl { get; }

        string PlaylistEndpointUrl { get; }

        string RootUrl { get; }

        string Username { get; }
    }
}