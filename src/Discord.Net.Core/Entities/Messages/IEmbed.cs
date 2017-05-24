using System;
using System.Collections.Immutable;

namespace Discord
{
    /// <summary>
    /// An embedded object in a message
    /// </summary>
    public interface IEmbed
    {
        /// <summary>
        /// The URL this embed should link to
        /// </summary>
        string Url { get; }
        /// <summary>
        /// The type of embed
        /// </summary>
        /// <remarks>
        /// This value will typically be 'rich' or 'link'.
        /// 
        /// Rich embeds are used with embeds triggered without a link, such as webhooks or bots, 
        /// while Link embeds will be used when a user sends a link.
        /// </remarks>
        string Type { get; }
        /// <summary>
        /// The title to appear in the header of the embed
        /// </summary>
        string Title { get; }
        /// <summary>
        /// The description content of the embed
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The timestamp to appear in the footer of the embed, if one is present
        /// </summary>
        DateTimeOffset? Timestamp { get; }
        /// <summary>
        /// The color of the bar on the embed, if one is specified
        /// </summary>
        Color? Color { get; }
        /// <summary>
        /// The image of this embed, if one is specified
        /// </summary>
        EmbedImage? Image { get; }
        /// <summary>
        /// The video of this embed, if one is specified
        /// </summary>
        EmbedVideo? Video { get; }
        /// <summary>
        /// The author of this embed, if one is specified
        /// </summary>
        EmbedAuthor? Author { get; }
        /// <summary>
        /// The footer of this embed, if one is specified
        /// </summary>
        EmbedFooter? Footer { get; }
        /// <summary>
        /// The integration providing this embed, if one is specified
        /// </summary>
        EmbedProvider? Provider { get; }
        /// <summary>
        /// The thumbnail of this embed, if one is specified
        /// </summary>
        EmbedThumbnail? Thumbnail { get; }
        /// <summary>
        /// A collection of fields present in this embed, if the embed is a type of rich embed
        /// </summary>
        ImmutableArray<EmbedField> Fields { get; }
    }
}
