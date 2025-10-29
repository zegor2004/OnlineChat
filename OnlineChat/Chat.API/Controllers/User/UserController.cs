using System.Security.Claims;
using Chat.API.Contracts.User.Request;
using Chat.Domain.Abstractions.User;
using Chat.Domain.Models.User;
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
        public async Task<ActionResult<List<UserViewModel>>> FindUserByName(FindUserRequest request)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null) return Unauthorized();

            var userId = new Guid(nameIdentifier.Value);

            var users = await _usersService.FindUserByName(userId, request.name);
            if (users.Count == 0) return NotFound();

            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegUserRequest request)
        {
            var mes = await _usersService.Registration(request.Email, request.Password, request.Name);

            if (!string.IsNullOrEmpty(mes)) 
                return BadRequest(mes);
            
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);
            if (string.IsNullOrEmpty(token)) 
                return BadRequest("Invalid email/password");
            return Ok(token);
        }
        //[HttpPut]
        //public async Task<ActionResult> UpdateUserData(RegUserRequest request)
        //{
        //    var email = await _usersService.UpdateUser(request.Email, request.Password, request.Name);

        //    return Ok(email);
        //}
        //[HttpDelete("{Email}")]
        //public async Task<ActionResult<string>> DeleteUser(string Email)
        //{
        //    var email = await _usersService.DeleteUser(Email);

        //    return NoContent();
        //}
    }
}
