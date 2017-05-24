namespace Discord
{
    /// <summary>
    /// An application for use with Discord's OAuth2 API
    /// </summary>
    public interface IApplication : ISnowflakeEntity
    {
        /// <summary>
        /// The name of the application
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The description of the application
        /// </summary>
        string Description { get; }
        /// <summary>
        /// A list of host addresses that can be used for RPC authentication with the application
        /// </summary>
        string[] RPCOrigins { get; }
        /// <summary>
        /// Flags set on the application (whitelisting)
        /// </summary>
        ulong Flags { get; }
        /// <summary>
        /// The URL of the application's icon
        /// </summary>
        string IconUrl { get; }

        /// <summary>
        /// The user account who owns the application
        /// </summary>
        IUser Owner { get; }
    }
}
