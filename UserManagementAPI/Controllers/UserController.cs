using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.DTOs;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            var usersDTO = users.Select(UserToDTO);

            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            var userDTO = UserToDTO(user);

            return Ok(userDTO);

        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO userDTO)
        {
            var user = DTOToUser(userDTO);
            await _userRepository.Add(user);

            var newUserDTO = CreatedAtAction(nameof(GetUser), new {id = user.Id}, UserToDTO(user));

            var response = new { User = newUserDTO.Value, Message = "User added successfully" };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
                return BadRequest();

            var user = DTOToUser(userDTO);

            try
            {
                await _userRepository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepository.UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var response = new { User = userDTO, Message = "User updated successfully" };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return NotFound();

            await _userRepository.Delete(user);

            var userDTO = UserToDTO(user);

            var response = new { User = userDTO, Message = "User deleted successfully" };
            return Ok(response);
        }

        private static UserDTO UserToDTO(User user) =>
            new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Address = user.Address
            };

        private static User DTOToUser(UserDTO userDTO) =>
            new()
            {
                Id = userDTO.Id,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DateOfBirth = userDTO.DateOfBirth,
                Gender = userDTO.Gender,
                Address = userDTO.Address
            };
    }
}
