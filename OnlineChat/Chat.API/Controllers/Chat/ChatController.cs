using Chat.API.Contracts.Chat;
using Chat.API.Contracts.Chat.Request;
using Chat.Domain.Abstractions.Chat;
using Chat.Domain.Models.Chat;
using Chat.Domain.Models.Chat.Message;
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
        private readonly IChatService _chatServices;
        public ChatController(IChatService chatServices)
        {
            _chatServices = chatServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<ChatViewModel>>> GetChatsPreview()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null) return Unauthorized();
            var userId = new Guid(nameIdentifier.Value);

            var chats = await _chatServices.GetChatPreview(userId);
            if (chats.Count == 0) return NotFound();
            
            return Ok(chats);
        }
        [HttpGet]
        public async Task<ActionResult<ChatViewModel>> GetChat(GetChatRequest request)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null) return Unauthorized();
            var userIdFrom = new Guid(nameIdentifier.Value);

            var chat = await _chatServices.GetChat(userIdFrom, request.userId);
            if (chat.ChatId == Guid.Empty) return NotFound();

            return Ok(chat);
        }
        [HttpPost]
        public async Task<ActionResult<MessageModel>> SendMessage(SendMessageRequest request)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null) return Unauthorized();
            var userIdFrom = new Guid(nameIdentifier.Value);
            var message = await _chatServices.SendMessage(userIdFrom, request.userId, request.text);
            return Ok(message);
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateStatusMessage(MessageIdRequest request)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier == null) return Unauthorized();

            var result = await _chatServices.UpdateStatusMessage(request.messageId);
            if (!result) return BadRequest();

            return Ok(result);
        }
    }
}
