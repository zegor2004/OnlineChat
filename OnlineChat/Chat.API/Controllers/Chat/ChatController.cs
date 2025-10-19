using Chat.Domain.Abstractions.Chat;
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
        public async Task<List<>> GetChatsPreview()
        {
            IChatService
        }
        [HttpGet]
        public async Task<> GetChat(string email)
        {

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
