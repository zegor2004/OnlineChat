using Chat.API.Contracts;
using Chat.Application.Services;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;
        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();

            var response = users.Select(b => new UserResponse( b.Email, b.Password, b.Name));

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateUser([FromBody] UserRequest request)
        {
            var (Mes, User) = ChatUser.Create(request.Email, request.Password, request.Name);

            if (!string.IsNullOrEmpty(Mes))
            {
                return BadRequest(Mes);
            }

            var email = await _usersService.CreateUser(User);

            return Ok(email);
        }
        [HttpPut("{Email}")]
        public async Task<ActionResult<string>> UpdateUser(string Email, [FromBody] UserRequest request)
        {
            var email = await _usersService.UpdateUser(request.Email, request.Password, request.Name);

            return Ok(email);
        }
        [HttpDelete("{Email}")]
        public async Task<ActionResult<string>> DeleteUser(string Email)
        {
            var email = await _usersService.DeleteUser(Email);

            return Ok(email);
        }
    }
}
