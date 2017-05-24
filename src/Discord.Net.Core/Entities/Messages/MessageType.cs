namespace Discord
{
    /// <summary>
    /// The type of message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// A normal text message
        /// </summary>
        Default = 0,
        RecipientAdd = 1,
        RecipientRemove = 2,
        Call = 3,
        ChannelNameChange = 4,
        ChannelIconChange = 5,
        ChannelPinnedMessage = 6
    }
}
