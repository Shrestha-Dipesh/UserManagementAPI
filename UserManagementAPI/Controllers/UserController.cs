using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
                return NotFound();

            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
                return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);

        }

        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            if (_context.Users == null)
                return NotFound();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new { User = user, Message = "User added successfully" };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var response = new { User = user, Message = "User updated successfully" };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
                return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            var response = new { User = user, Message = "User deleted successfully" };
            return Ok(response);
        }
    }
}
