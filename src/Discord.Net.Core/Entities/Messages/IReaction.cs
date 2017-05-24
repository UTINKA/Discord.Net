namespace Discord
{
    public interface IReaction
    {
        /// <summary>
        /// The emote used in this reaction.
        /// </summary>
        /// <remarks>
        /// When the reaction is a unicode emoji, this value will be of type <see cref="Emoji"/>.
        /// When the reaction if a custom Discord emote, this value will be a type of <see cref="Discord.Emote"/>
        /// </remarks>
        IEmote Emote { get; }
    }
}
