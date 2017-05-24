using System;
using System.Threading.Tasks;

namespace Discord
{
    /// <summary>
    /// A user logged in to the client
    /// </summary>
    public interface ISelfUser : IUser
    {
        /// <summary> Gets the email associated with this user. </summary>
        string Email { get; }
        /// <summary> Returns true if this user's email has been verified. </summary>
        bool IsVerified { get; }
        /// <summary> Returns true if this user has enabled MFA on their account. </summary>
        bool IsMfaEnabled { get; }

        /// <summary>
        /// Modify the current user
        /// </summary>
        /// <param name="func">A function to specify what entities should be changed on the user</param>
        /// <returns>A task that returns when the API request was completed</returns>
        /// <example>
        /// <code>
        /// await client.CurrentUser.ModifyAsync(x =&gt; { x.Username = &quot;1234&quot;; });
        /// </code>
        /// </example>
        Task ModifyAsync(Action<SelfUserProperties> func, RequestOptions options = null);
    }
}