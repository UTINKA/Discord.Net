using System;

namespace Discord
{
    /// <summary>
    /// Any entity used by the Discord API
    /// </summary>
    /// <typeparam name="TId">The type of this entity's ID</typeparam>
    public interface IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> 
        /// Gets the unique identifier for this object. 
        /// </summary>
        TId Id { get; }

    }
}
