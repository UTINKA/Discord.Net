using Discord.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Discord
{
    public interface IDiscordClient : IDisposable
    {
        /// <summary>
        /// Returns the Connection State of this client.
        /// </summary>
        /// <remarks>
        /// When the client is a DiscordRestClient, this value will always return <see cref="ConnectionState.Disconnected"/>/>
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// await client.ConnectAsync();
        /// if (client.ConnectionState != ConnectionState.Connected)
        ///     Environment.Exit(-1);
        /// </code>
        /// </example>
        ConnectionState ConnectionState { get; }
        /// <summary>
        /// Returns the client used to make raw API requests to Discord.
        /// </summary>
        /// <remarks>
        /// This client should only be used when you need to need to request an endpoint that the library does not support.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// await client.ApiClient.SendAsync("POST", "/messages/{id}/<endpoint>");
        /// </code>
        /// </example>
        DiscordRestApiClient ApiClient { get; }
        /// <summary>
        /// Returns the user that this client is logged into.
        /// </summary>
        /// <remarks>
        /// Should the client be disconnected, this value will be null.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// await client.CurrentUser.ModifyAsync(m => m.Username = "name");
        /// </code>
        /// </example>
        ISelfUser CurrentUser { get; }

        /// <summary>
        /// Opens a WebSocket connection to Discord's gateway.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// When the client is a DiscordRestClient, this task will immidiately throw a NotSupportedException.
        /// </exception>
        /// <example>
        /// <code language="c#">
        /// var client = new DiscordSocketClient();
        /// await client.LoginAsync(TokenType.Bot, token);
        /// await client.Connect();
        /// </code>
        /// </example>
        /// <returns>
        /// An awaitable Task that returns when the connection is opened.
        /// </returns>
        Task ConnectAsync();
        /// <summary>
        /// Closes a WebSocket connection, if one is opened.
        /// </summary>
        /// /// <exception cref="NotSupportedException">
        /// When the client is a DiscordRestClient, this task will immidiately throw a NotSupportedException.
        /// </exception>
        /// <example>
        /// <code language="c#">
        /// [Command("reconnect")]
        /// [RequireOwner]
        /// public async Task Reconnect()
        /// {
        ///     await Context.Client.DisconnectAsync();
        ///     await Context.Client.ConnectAsync();
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An awaitable Task that returns when the disconnect is finished.
        /// </returns>
        Task DisconnectAsync();

        /// <summary>
        /// Returns the application that owns the bot account this client is logged in to.
        /// </summary>
        /// <remarks>
        /// This value will be cached after the first time it is called.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// var application = await client.GetApplicationInfoAsync();
        /// await ReplyAsync($"The bot's owner is {application.Owner.Id}");
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// When the client is logged into a user account, this endpoint will throw a 403.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable Task containing the bot account's parent application.
        /// </returns>
        Task<IApplication> GetApplicationInfoAsync();

        /// <summary>
        /// Get a channel entity from Discord.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using a DiscordRestClient, this method will **always** invoke a REST request, since
        /// there is no concept of a global entity cache on a REST client.
        /// 
        /// When using a DiscordSocketClient, this method will attempt to pull the object from its
        /// cache; if it cannot find an entity with this ID in the cache, it will attempt to download
        /// from Discord, respectful to the mode parameter.
        /// </para>
        /// </remarks>
        /// <param name="id">The ID of the channel.</param>
        /// <param name="mode">Whether this client should be allowed to request this object from Discord, or be limited to its cache.</param>
        /// <example>
        /// <code language="c#">
        /// var channel = await client.GetChannelAsync(81384956881809408) as ITextChannel;
        /// var topic = channel.Topic;
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// If the channel cannot be found, this endpoint will throw a 404.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable Task containing a context-less Channel object.
        /// </returns>
        Task<IChannel> GetChannelAsync(ulong id, CacheMode mode = CacheMode.AllowDownload);
        /// <summary>
        /// Get a collection of the open private channels this account has.
        /// </summary>
        /// <remarks>
        /// When the client is logged in to a Bot account, this collection will always start empty.
        /// Discord no longer populates the private channel collection for bots.
        /// </remarks>
        /// <param name="mode">Whether this client should be allowed to request this object from Discord, or be limited to its cache.</param>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable Task containing a collection of private channels.
        /// </returns>
        Task<IReadOnlyCollection<IPrivateChannel>> GetPrivateChannelsAsync(CacheMode mode = CacheMode.AllowDownload);

        /// <summary>
        /// Gets the integrations for this account.
        /// </summary>
        /// <example>
        /// <code language="c#">
        /// var connections = await client.GetConnectionsAsync();
        /// await ReplyAsync($"Connections: {string.Join(", ", connections.Select(c => c.Name))}");
        /// </code>
        /// </example>
        /// <returns>A collection of integrations assosciated with this account.</returns>
        Task<IReadOnlyCollection<IConnection>> GetConnectionsAsync();

        /// <summary>
        /// Gets a guild entity from Discord.
        /// </summary>
        /// <param name="id">The ID of the guild.</param>
        /// <param name="mode">Whether this client should be allowed to request this object from Discord, or be limited to its cache.</param>
        /// <example>
        /// <code language="c#">
        /// var guild = await client.GetGuild(150482537465118720);
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// If the guild cannot be found, this endpoint will throw a 404.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable Task containing a guild.
        /// </returns>
        Task<IGuild> GetGuildAsync(ulong id, CacheMode mode = CacheMode.AllowDownload);
        /// <summary>
        /// Gets a collection of all the guilds this bot is a member of.
        /// </summary>
        /// <param name="mode">Whether this client should be allowed to request this object from Discord, or be limited to its cache.</param>
        /// <example>
        /// <code language="c#">
        /// var guilds = await client.GetGuildsAsync();
        /// await ReplyAsync($"This bot is a member of {guilds.Count} guilds.");
        /// </code>
        /// </example>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable Task containing a collection of guilds.
        /// </returns>
        Task<IReadOnlyCollection<IGuild>> GetGuildsAsync(CacheMode mode = CacheMode.AllowDownload);
        /// <summary>
        /// Creates a guild.
        /// </summary>
        /// <remarks>
        /// Bot accounts **cannot** use this method, unless they are whitelisted by Discord.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// var region = (await client.GetVoiceRegionsAsync()).FirstOrDefault();
        /// var guild = await client.CreateGuildAsync("my guild", region);
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// When the client is logged in as a bot account, this endpoint will throw a 403.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <param name="name">The name of the guild.</param>
        /// <param name="region">The region this guild's voice server will be created in.</param>
        /// <param name="jpegIcon">A stream containing an icon for this guild.</param>
        /// <returns>
        /// An awaitable Task containing the guild that was just created.
        /// </returns>
        Task<IGuild> CreateGuildAsync(string name, IVoiceRegion region, Stream jpegIcon = null);

        /// <summary>
        /// Gets an invite entity from Discord.
        /// </summary>
        /// <remarks>
        /// The navigation properties on IInvite will not work when the invite is pulled directly from a client.
        /// 
        /// In order to use the navigation properties, the invite must be retrieved from a <see cref="IGuild"/>.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// var invite = await Context.Client.GetInviteAsync("discord-api");
        /// await ReplyAsync($"Invite URL: {invite.Url}");
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// If the invite cannot be found, this endpoint will throw a 404.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If using the navigation properties on this object, an InvalidOperationException will be thrown; 
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <param name="inviteId">The code of the invite.</param>
        /// <returns>
        /// An awaitable Task containing the invite.
        /// </returns>
        Task<IInvite> GetInviteAsync(string inviteId);

        /// <summary>
        /// Gets a global user from Discord.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="mode">Whether this client should be allowed to request this object from Discord, or be limited to its cache.</param>
        /// <example>
        /// <code language="c#">
        /// var user = await client.GetUserAsync(66078337084162048);
        /// await ReplyAsync($"The user's name is {user.Username}");
        /// </code>
        /// </example>
        /// <exception cref="Net.HttpException">
        /// If the user cannot be found, this endpoint will throw a 404.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>
        /// An awaitable task containing the user.
        /// </returns>
        Task<IUser> GetUserAsync(ulong id, CacheMode mode = CacheMode.AllowDownload);
        /// <summary>
        /// Gets a global user from Discord, using the user's Username#Discriminator
        /// </summary>
        /// <remarks>
        /// When the client is a DiscordRestClient, this method will always return null.
        /// 
        /// This method will always pull from the global user cache, should a cache exist.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// public async Task<ulong> FindUser(string name, string discrim)
        /// {
	    ///     return (await client.GetUser(name, discrim)).Id;
        /// }
        /// </code>
        /// </example>
        /// <param name="username">The user's username</param>
        /// <param name="discriminator">The user's discriminator</param>
        /// <returns>An awaitable Task containing the user.</returns>
        Task<IUser> GetUserAsync(string username, string discriminator);

        /// <summary>
        /// Retrieves a collection of voice regions Discord supports.
        /// </summary>
        /// <example>
        /// <code language="c#">
        /// var regions = await Context.Client.GetVoiceRegionsAsync();
        /// await ReplyAsync(string.Join(", ", regions.Select(r => r.Name)));
        /// </code>
        /// </example>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>A collection of voice regions Discord supports.</returns>
        Task<IReadOnlyCollection<IVoiceRegion>> GetVoiceRegionsAsync();
        /// <summary>
        /// Retrieves a voice region entity by its moniker.
        /// </summary>
        /// <remarks>
        /// If a voice region with this ID cannot be found, this method will return null.
        /// </remarks>
        /// <example>
        /// <code language="c#">
        /// var region = await client.GetVoiceRegionAsync("us-east");
        /// </code>
        /// </example>
        /// <param name="id">The moniker of the voice region.</param>
        /// <exception cref="InvalidOperationException">
        /// If the client is not logged in, an InvalidOperationException will be thrown.
        /// </exception>
        /// <returns>A voice region entity.</returns>
        Task<IVoiceRegion> GetVoiceRegionAsync(string id);
    }
}
