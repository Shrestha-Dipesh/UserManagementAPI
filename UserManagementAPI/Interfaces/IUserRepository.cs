using UserManagementAPI.Models;

namespace UserManagementAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<bool> Save();
        bool UserExists(int id);
    }
}
