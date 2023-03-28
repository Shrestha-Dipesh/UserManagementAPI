using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(User user)
        {
            _context.Users.Add(user);
            return await Save();
        }

        public async Task<bool> Delete(User user)
        {
            _context.Users.Remove(user);
            return await Save();

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await Save();
        }

        public bool UserExists(int id)
        {
            var userExists = _context.Users.Any(u => u.Id == id);
            return userExists;
        }
    }
}
