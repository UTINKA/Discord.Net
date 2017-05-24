namespace Discord
{
    /// <summary>
    /// A summary of a guild that the user is a member of
    /// </summary>
    /// <remarks>
    /// These entities are lacking in information, and it is preferred to use <see cref="IGuild"/>
    /// instead.
    /// </remarks>
    public interface IUserGuild : IDeletable, ISnowflakeEntity
    {
        /// <summary> Gets the name of this guild. </summary>
        string Name { get; }
        /// <summary> Returns the url to this guild's icon, or null if one is not set. </summary>
        string IconUrl { get; }
        /// <summary> Returns true if the current user owns this guild. </summary>
        bool IsOwner { get; }
        /// <summary> Returns the current user's permissions for this guild. </summary>
        GuildPermissions Permissions { get; }
    }
}
