using System;
using System.Threading.Tasks;

namespace Discord
{
    public interface IRole : ISnowflakeEntity, IDeletable, IMentionable, IComparable<IRole>
    {
        /// <summary>
        /// Gets the guild owning this role.
        /// </summary>
        IGuild Guild { get; }

        /// <summary>
        /// Gets the color given to users of this role.
        /// </summary>
        /// <remarks>
        /// If this role does not have a color set, this value will be <see cref="Color.Default"/>
        /// </remarks>
        Color Color { get; }
        /// <summary>
        /// Returns true if users of this role are separated in the user list.
        /// </summary>
        bool IsHoisted { get; }
        /// <summary>
        /// Returns true if this role is automatically managed by Discord or an integration.
        /// </summary>
        bool IsManaged { get; }
        /// <summary>
        /// Returns true if this role may be mentioned in messages.
        /// </summary>
        bool IsMentionable { get; }
        /// <summary>
        /// Gets the name of this role.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the permissions granted to members of this role.
        /// </summary>
        GuildPermissions Permissions { get; }
        /// <summary>
        /// Gets this role's position relative to other roles in the same guild.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Modifies this role.
        /// </summary>
        /// <param name="func">A function to specify what entities should be changed on the role</param>
        /// <example>
        /// <code>
        /// await role.ModifyAsync(x =&gt; { x.Color = new Color(0x0022DD); });
        /// </code>
        /// </example>
        Task ModifyAsync(Action<RoleProperties> func, RequestOptions options = null);
    }
}
