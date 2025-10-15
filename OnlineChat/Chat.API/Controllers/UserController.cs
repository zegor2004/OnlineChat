using Chat.API.Contracts;
using Chat.Application.Services;
using Chat.Domain.Abstractions;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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

            var response = users.Select(b => new UserResponse(b.Email, b.PasswordHash, b.Name));

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Registration([FromBody] UserRequest request)
        {
            var mes = ChatUser.DataValidation(request.Email, request.Password, request.Name);
            if (!string.IsNullOrEmpty(mes))
            {
                return BadRequest(mes);
            }

            var User = ChatUser.Create(request.Email, request.Password, request.Name);

            var email = await _usersService.CreateUser(User);

            return Ok(email);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login(UserRequest request)
        {
            var user = ChatUser.Create(request.Email, request.Password, request.Name);

            var password = await _usersService.Login(user);
            return Ok(password);
        }
        [HttpPut("{Email}")]
        public async Task<ActionResult<string>> UpdateUser(string Email, [FromBody] UserRequest request)
        {
            var email = await _usersService.UpdateUser(Email, request.Password, request.Name);

            return Ok(email);
        }
        [HttpDelete("{Email}")]
        public async Task<ActionResult<string>> DeleteUser(string Email)
        {
            var email = await _usersService.DeleteUser(Email);

            return NoContent();
        }
    }
}
