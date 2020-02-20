using RPedretti.RazorComponents.Sample.Shared.Domain;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.SignalRServer.Repository
{
    public interface IUserRepository
    {
        #region Methods

        Task<bool> AddUserAsync(User user);

        /// <summary>
        /// Searches for a user at the repository
        /// </summary>
        /// <param name="username">The username to look for</param>
        /// <returns>Returns a task wich result yields a User</returns>
        Task<User> GetUserAsync(string username);

        Task<bool> RemoveUserAsync(string username);

        #endregion Methods
    }
}
