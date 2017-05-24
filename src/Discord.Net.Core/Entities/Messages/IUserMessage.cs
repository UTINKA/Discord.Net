using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord
{
    /// <summary>
    /// A type of message sent by a User
    /// </summary>
    public interface IUserMessage : IMessage
    {
        /// <summary>
        /// Modifies this message.
        /// </summary>
        /// <remarks>
        /// When an embed is present, the content field may be omitted.
        /// </remarks>
        /// <param name="func">A function to specify what entities should be changed on the message</param>
        /// <example>
        /// <code language="c#">
        /// var message = await ReplyAsync("abc");
        /// await message.ModifyAsync(x =>
        /// {
        ///     x.Content = "";
        ///     x.Embed = new EmbedBuilder()
        ///         .WithColor(new Color(40, 40, 120))
        ///         .WithAuthor(a => a.Name = "foxbot")
        ///         .WithTitle("Embed!")
        ///         .WithDescription("This is an embed.");
        /// });
        /// </code>
        /// </example>
        Task ModifyAsync(Action<MessageProperties> func, RequestOptions options = null);
        /// <summary>
        /// Adds this message to its channel's pinned messages.
        /// </summary>
        /// <seealso cref="IMessageChannel.GetPinnedMessagesAsync(RequestOptions)"/>
        Task PinAsync(RequestOptions options = null);
        /// <summary>
        /// Removes this message from its channel's pinned messages.
        /// </summary>
        /// <seealso cref="IMessageChannel.GetPinnedMessagesAsync(RequestOptions)"/>
        Task UnpinAsync(RequestOptions options = null);

        /// <summary>
        /// Returns all reactions included in this message.
        /// </summary>
        IReadOnlyDictionary<IEmote, ReactionMetadata> Reactions { get; }

        /// <summary>
        /// Adds a reaction to this message
        /// </summary>
        /// <param name="emote">The emote to add as a reaction</param>
        /// <example>
        /// <code>
        /// client.MessageReceived += message =&gt;
        /// {
        ///     if (message.Content == &quot;aaa&quot;)
        ///         await message.AddReactionAsync(new Emoji(&quot;&lt;your unicode emoji here&gt;&quot;));
        /// };
        /// </code>
        /// </example>
        Task AddReactionAsync(IEmote emote, RequestOptions options = null);
        /// <summary>
        /// Removes a reaction from a message
        /// </summary>
        /// <param name="emote">The emote of the reaction to be removed</param>
        /// <param name="user">The user whose reaction should be removed</param>
        /// <example>
        /// <code>
        /// var emote = message.Reactions.FirstOrDefault(x =&gt; x.Key.Name == &quot;emoji here&quot;);
        /// await message.RemoveReactionAsync(emote, client.CurrentUser);
        /// </code>
        /// </example>
        Task RemoveReactionAsync(IEmote emote, IUser user, RequestOptions options = null);
        /// <summary>
        /// Removes all reactions from this message.
        /// </summary>
        Task RemoveAllReactionsAsync(RequestOptions options = null);
        // TODO: what is this
        Task<IReadOnlyCollection<IUser>> GetReactionUsersAsync(string emoji, int limit = 100, ulong? afterUserId = null, RequestOptions options = null);

        /// <summary> Transforms this message's text into a human readable form by resolving its tags. </summary>
        string Resolve(
            TagHandling userHandling = TagHandling.Name,
            TagHandling channelHandling = TagHandling.Name,
            TagHandling roleHandling = TagHandling.Name,
            TagHandling everyoneHandling = TagHandling.Ignore,
            TagHandling emojiHandling = TagHandling.Name);
    }
}
