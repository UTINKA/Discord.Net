using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Discord
{
    /// <summary>
    /// Any type of client that can connect to the Discord API
    /// </summary>
    public interface IDiscordClient : IDisposable
    {
        /// <summary>
        /// The connection state of the client
        /// </summary>
        /// <remarks>
        /// This property is not applicable to the REST client, as it does not maintain an ongoing
        /// connection to Discord.
        /// </remarks>
        ConnectionState ConnectionState { get; }
        /// <summary>
        /// The user logged in to this client
        /// </summary>
        ISelfUser CurrentUser { get; }
        /// <summary>
        /// The type of token this client is using
        /// </summary>
        TokenType TokenType { get; }

        /// <summary>
        /// Open a connection to Discord
        /// </summary>
        /// <returns>A task that returns when the connection has been started.</returns>
        Task StartAsync();
        /// <summary>
        /// Closes any open connection to Discord
        /// </summary>
        /// <returns>A task that returns when the connection has been stopped.</returns>
        Task StopAsync();

        /// <summary>
        /// Get the application info for the current bot
        /// </summary>
        /// <remarks>
        /// This method will only work when the <see cref="TokenType"/> is a bot token.
        /// </remarks>
        /// <exception cref="Net.HttpException">
        /// If the client is not logged in as a bot, this endpoint will throw a 403.
        /// </exception>
        /// <returns>A task containing the application info.</returns>
        Task<IApplication> GetApplicationInfoAsync(RequestOptions options = null);

        /// <summary>
        /// Get a channel entity from the API
        /// </summary>
        /// <param name="id">The Snowflake-based ID of the channel</param>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing the channel entity</returns>
        Task<IChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Get a collection of the current user's private channels (DMs and Groups)
        /// </summary>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing a collection of the user's private channels</returns>
        Task<IReadOnlyCollection<IPrivateChannel>> GetPrivateChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Get a collection of the current user's DMs
        /// </summary>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing a collection of the user's DMs</returns>
        Task<IReadOnlyCollection<IDMChannel>> GetDMChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Get a collection of the current user's group channels
        /// </summary>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing a collection of the user's groups</returns>
        Task<IReadOnlyCollection<IGroupChannel>> GetGroupChannelsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);

        /// <summary>
        /// Get a collection of the current user's connections
        /// </summary>
        /// <remarks>
        /// Bot accounts can not have connections, so this collection will always be empty for them.
        /// </remarks>
        /// <returns>A task containing a collection of the current user's connections</returns>
        Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync(RequestOptions options = null);

        /// <summary>
        /// Get a guild entity from the API
        /// </summary>
        /// <exception cref="Net.HttpException">
        /// If the guild could not be found, or the bot is not a member of the guild, this 
        /// endpoint will throw a 403
        /// </exception>
        /// <param name="id">The ID of the guild</param>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing the guild entity</returns>
        Task<IGuild> GetGuildAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Get a collection of the current user's guilds
        /// </summary>
        /// <param name="mode">Should this collection be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing the current user's guilds</returns>
        Task<IReadOnlyCollection<IGuild>> GetGuildsAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Create a guild
        /// </summary>
        /// <remarks>
        /// Bot accounts that are not whitelisted may only create 10 guilds. 
        /// </remarks>
        /// <exception cref="Net.HttpException">
        /// If the bot account is not whitelisted and has already created 10 guilds, the request
        /// will fail.
        /// </exception>
        /// <param name="name">The name of the guild</param>
        /// <param name="region">The voice region this guild should have - see <see cref="GetVoiceRegionsAsync(RequestOptions)"/></param>
        /// <param name="jpegIcon">A <see cref="Stream"/> containing the icon for the guild.</param>
        /// <returns>A task containing the recently created guild.</returns>
        Task<IGuild> CreateGuildAsync(string name, IVoiceRegion region, Stream jpegIcon = null, RequestOptions options = null);
        
        /// <summary>
        /// Get an invite entity from the API
        /// </summary>
        /// <param name="inviteId">The code of the invite</param>
        /// <returns>A task containing the invite entity</returns>
        /// <example>
        /// <code>
        /// var invite = await client.GetInviteAsync("discord-api");
        /// </code>
        /// </example>
        Task<IInvite> GetInviteAsync(string inviteId, RequestOptions options = null);

        /// <summary>
        /// Get a global user entity from the API
        /// </summary>
        /// <remarks>
        /// Users fetched through this method will be global entities only, meaning they cannot be
        /// cast into guild-specific entities.
        /// </remarks>
        /// <param name="id">The ID of the user</param>
        /// <param name="mode">Should this entity be pulled from the client's cache, if it has one?</param>
        /// <returns>A task containing the user entity</returns>
        Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null);
        /// <summary>
        /// Get a global user entity from the client's cache
        /// </summary>
        /// <remarks>
        /// This method requires a cache, so it will not function on REST clients
        /// </remarks>
        /// <param name="username">The user's username</param>
        /// <param name="discriminator">The user's four-digit discriminator</param>
        /// <returns>A task containing the user</returns>
        /// <example>
        /// <code>
        /// DiscordSocketClient client;
        /// var user = await client.GetUserAsync("1234", "5678");
        /// </code>
        /// </example>
        Task<IUser> GetUserAsync(string username, string discriminator, RequestOptions options = null);

        /// <summary>
        /// Get a collection of current voice regions supported by the Discord API
        /// </summary>
        /// <returns>A task containing a collection of voice regions supported by Discord</returns>
        Task<IReadOnlyCollection<IVoiceRegion>> GetVoiceRegionsAsync(RequestOptions options = null);
        /// <summary>
        /// Get a voice region by its ID.
        /// </summary>
        /// <param name="id">The ID of the voice region</param>
        /// <returns>A task containing a voice region</returns>
        /// <example>
        /// <code>
        /// var region = await client.GetVoiceRegionAsync("us-east");
        /// </code>
        /// </example>
        Task<IVoiceRegion> GetVoiceRegionAsync(string id, RequestOptions options = null);
    }
}
