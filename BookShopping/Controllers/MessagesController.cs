using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageRepository _messageRepo;
        private readonly BookDbContext _db;

        public MessagesController(IMessageRepository messageRepo, BookDbContext db)
        {
            _messageRepo = messageRepo;
            _db = db;
        }

        public async Task<IActionResult> UserChat()
        {
            // Logic to fetch and return messages
            var messages = await _messageRepo.GetAllMessagesAsync(User.Identity.Name);

            return View(messages);
        }
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> AdminChat()
        {
            // Logic to fetch and return messages
            var messages = await _messageRepo.GetAllMessagesAsync(User.Identity.Name);

            return View(messages);

        }

        [HttpGet]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _messageRepo.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string content, string RecipientId = "admin@gmail.com")
        {
            if (ModelState.IsValid)
            {
                var senderId = User.Identity.Name;

                if (string.IsNullOrEmpty(RecipientId) || string.IsNullOrEmpty(content))
                {
                    return BadRequest(new { title = "Recipient ID and message content are required." });
                }

                var message = new Message
                {
                    SenderId = senderId,
                    RecipientId = RecipientId,
                    Content = content,
                    SentAt = DateTime.UtcNow,
                    IsRead = false
                };

                await _messageRepo.SendMessageAsync(message);
            }

            if (RecipientId == "admin@gmail.com")
            {
                return RedirectToAction("UserChat");
            }

            return RedirectToAction("AdminChat");
        }

        public async Task<IActionResult> GetUserMessages(string userId)
        {
            var messages = await _messageRepo.GetAllMessagesAsync(userId);
            return Ok(messages);
        }

        public async Task<IActionResult> GetUnreadMessagesCount()
        {
            var userId = User.Identity.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var unreadCount = await _db.Messages.Where(m => m.RecipientId == userId && !m.IsRead)
                .CountAsync();
            return Ok(unreadCount);
        }
    }
}
