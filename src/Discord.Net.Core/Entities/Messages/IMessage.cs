using System;
using System.Collections.Generic;

namespace Discord
{
    /// <summary>
    /// Any type of message sent in a message channel.
    /// </summary>
    public interface IMessage : ISnowflakeEntity, IDeletable
    {
        /// <summary>
        /// Gets the type of this system message.
        /// </summary>
        MessageType Type { get; }
        /// <summary>
        /// Gets the source of this message.
        /// </summary>
        MessageSource Source { get; }
        /// <summary>
        /// Returns true if this message was sent as a text-to-speech message.
        /// </summary>
        bool IsTTS { get; }
        /// <summary>
        /// Returns true if this message was added to its channel's pinned messages.
        /// </summary>
        bool IsPinned { get; }
        /// <summary>
        /// Returns the content for this message.
        /// </summary>
        /// <remarks>
        /// The raw content of a message will not resolve mentions into a human-readable format,
        /// you can use <see cref="IUserMessage.Resolve(TagHandling, TagHandling, TagHandling, TagHandling, TagHandling)"/>
        /// </remarks>
        string Content { get; }
        /// <summary>
        /// Gets the time this message was sent.
        /// </summary>
        DateTimeOffset Timestamp { get; }
        /// <summary>
        /// Gets the time of this message's last edit, if any.
        /// </summary>
        DateTimeOffset? EditedTimestamp { get; }
        
        /// <summary>
        /// Gets the channel this message was sent to.
        /// </summary>
        /// <remarks>
        /// When the message was sent from a guild, this property will be of type <see cref="ITextChannel"/>.
        /// By casting this value to an ITextChannel, you can access the guild directly.
        /// </remarks>
        /// <example>
        /// <code>
        /// var guild = (message.Channel as ITextChannel)?.Guild;
        /// </code>
        /// </example>
        IMessageChannel Channel { get; }
        /// <summary>
        /// Gets the author of this message.
        /// </summary>
        IUser Author { get; }

        /// <summary>
        /// Returns all attachments included in this message.
        /// </summary>
        IReadOnlyCollection<IAttachment> Attachments { get; }
        /// <summary>
        /// Returns all embeds included in this message.
        /// </summary>
        /// <remarks>
        /// When the <see cref="Source"/> is not <see cref="MessageSource.Webhook"/>, this collection
        /// will contain a maximum of one element; only webhooks can send multiple embeds.
        /// </remarks>
        IReadOnlyCollection<IEmbed> Embeds { get; }
        /// <summary>
        /// Returns all non-text elements included in this message's content.
        /// </summary>
        IReadOnlyCollection<ITag> Tags { get; }
        /// <summary>
        /// Returns the ids of channels mentioned in this message.
        /// </summary>
        IReadOnlyCollection<ulong> MentionedChannelIds { get; }
        /// <summary>
        /// Returns the ids of roles mentioned in this message.
        /// </summary>
        IReadOnlyCollection<ulong> MentionedRoleIds { get; }
        /// <summary>
        /// Returns the ids of users mentioned in this message.
        /// </summary>
        IReadOnlyCollection<ulong> MentionedUserIds { get; }
    }
}
