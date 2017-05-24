namespace Discord
{
    /// <summary>
    /// Any object that can be mentioned in a message.
    /// </summary>
    public interface IMentionable
    {
        /// <summary>
        /// Returns a special string used to mention this object.
        /// </summary>
        string Mention { get; }
    }
}
