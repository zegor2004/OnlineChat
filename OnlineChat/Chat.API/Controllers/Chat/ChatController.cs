using Chat.API.Contracts.Chat;
using Chat.API.Contracts.Chat.Request;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var chats = await _chatServices.GetChatPreview(email);
            return Ok(chats);
        }
        [HttpGet]
        public async Task<ActionResult<ChatViewModel>> GetChat(GetChatRequest request)
        {
            var email_1 = User.FindFirst(ClaimTypes.Email)?.Value;
            var chat = await _chatServices.GetChat(email_1, request.email);
            return Ok(chat);
        }
        [HttpPost]
        public async Task<ActionResult<ChatModel>> SendMessage(SendMessageRequest request)
        {
            var email_1 = User.FindFirst(ClaimTypes.Email)?.Value;
            var message = await _chatServices.SendMessage(email_1, request.email, request.text);
            return Ok(message);
        }
    }
}
