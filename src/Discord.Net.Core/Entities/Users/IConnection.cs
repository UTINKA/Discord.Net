using System.Collections.Generic;

namespace Discord
{
    /// <summary>
    /// A connection between a Discord account and a third-party service
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// The ID of the user on the connection
        /// </summary>
        string Id { get; }
        /// <summary>
        /// The type of service this connection is to
        /// </summary>
        string Type { get; }
        /// <summary>
        /// The display name of this user on the connection
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Has the connection been revoked?
        /// </summary>
        bool IsRevoked { get; }

        IReadOnlyCollection<ulong> IntegrationIds { get; }
    }
}
