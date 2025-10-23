using System.Security.Claims;
using Chat.API.Contracts.User.Request;
using Chat.API.Contracts.User.Response;
using Chat.Application.Services;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.User
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
        public async Task<ActionResult<List<UserResponse>>> FindUserByName(FindUserRequest request)
        {
            var users = await _usersService.FindUserByName(request.name);

            var response = users.Select(b => new UserResponse(b.UserId, b.Name));

            //var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Registration(RegUserRequest request)
        {
            var mes = await _usersService.Registration(request.Email, request.Password, request.Name);

            if (!string.IsNullOrEmpty(mes)) 
                return BadRequest(mes);

            return Ok(mes);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);
            if (string.IsNullOrEmpty(token)) 
                return BadRequest("Invalid email/password");
            return Ok(token);
        }
        [HttpPut("{Email}")]
        public async Task<ActionResult<string>> UpdateUser(string Email, [FromBody] RegUserRequest request)
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
