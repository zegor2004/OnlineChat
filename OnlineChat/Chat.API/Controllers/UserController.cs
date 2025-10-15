using Chat.API.Contracts;
using Chat.Application.Services;
using Chat.Domain.Abstractions;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();

            var response = users.Select(b => new UserResponse(b.Email, b.Password, b.Name));

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Registration(UserRequest request)
        {
            var mes = await _usersService.Registration(request.Email, request.Password, request.Name);

            if (!string.IsNullOrEmpty(mes)) return BadRequest(mes);

            return Ok(mes);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login(UserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);
            if (string.IsNullOrEmpty(token)) return BadRequest("Invalid email or password");
            return Ok(token);
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
