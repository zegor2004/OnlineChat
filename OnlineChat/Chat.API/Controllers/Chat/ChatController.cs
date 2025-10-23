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
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var chats = await _chatServices.GetChatPreview(userId);
            return Ok(chats);
        }
        [HttpGet]
        public async Task<ActionResult<ChatViewModel>> GetChat(GetChatRequest request)
        {
            var userIdFrom = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var chat = await _chatServices.GetChat(userIdFrom, request.userId);
            return Ok(chat);
        }
        [HttpPost]
        public async Task<ActionResult<ChatModel>> SendMessage(SendMessageRequest request)
        {
            var userIdFrom = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var message = await _chatServices.SendMessage(userIdFrom, request.userId, request.text);
            return Ok(message);
        }
    }
}
