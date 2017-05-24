using System;

namespace Discord
{
    /// <summary>
    /// Any entity which uses a snowflake as its ID
    /// </summary>
    public interface ISnowflakeEntity : IEntity<ulong>
    {
        /// <summary>
        /// The moment this entity was created
        /// </summary>
        DateTimeOffset CreatedAt { get; }
    }
}
