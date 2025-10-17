using Chat.Domain.Abstractions.Chat;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Guid>> SendMessage(string email_2)
        {
            var email_1 = User.FindFirst("email")?.Value;
            var chat_id = await _chatServices.SendMessage(email_1, email_2);
            return Ok(chat_id);
        }
    }
}
