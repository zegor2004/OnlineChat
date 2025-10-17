using Chat.Domain.Abstractions.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Chat
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        public ChatController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(string email_2)
        {
            var email = User.FindFirst("email")?.Value;
            var chat = await _chatServices.Create(email, email_2);
            return Ok(chat);
        }
    }
}
