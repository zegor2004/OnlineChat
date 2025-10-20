using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Chat
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        public ChatController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<ChatViewModel>>> GetChatsPreview()
        {
            var email = User.FindFirst("email")?.Value;
            var chats = await _chatServices.GetChatPreview(email);
            return Ok(chats);
        }
        [HttpGet]
        public async Task<ActionResult<ChatViewModel>> GetChat(string email_2)
        {
            var email_1 = User.FindFirst("email")?.Value;
            var chat = await _chatServices.GetChat(email_1, email_2);
            return Ok(chat);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> SendMessage(string email_2)
        {
            var email_1 = User.FindFirst("email")?.Value;
            //var chat_id = await _chatServices.SendMessage(email_1, email_2);
            return Ok(new Guid());
        }
    }
}
