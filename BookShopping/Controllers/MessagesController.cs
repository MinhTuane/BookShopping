﻿using Microsoft.AspNetCore.Authorization;
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
            var userIds = await _messageRepo.GetAllInteractedUserIds(User.Identity.Name);

            return View(userIds);

        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> GetMessages(string otherUserId)
        {
            var userId = User.Identity.Name;

            if (string.IsNullOrEmpty(otherUserId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { title = "Recipient ID and message content are required." });
            }
            var messages = await _db.Messages
                .Where(m => (m.SenderId == userId && m.RecipientId == otherUserId) ||
                            (m.SenderId == otherUserId && m.RecipientId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Json(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserMessages(string userId)
        {
            var messages = await _db.Messages
                .Where(m => (m.SenderId == userId && m.RecipientId == User.Identity.Name) ||
                            (m.RecipientId == userId && m.SenderId == User.Identity.Name))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            var messageModels = messages.Select(m => new MessageModel
            {
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt.ToString("g")
            });

            return Json(messageModels);
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

        [HttpPost]
        public async Task<IActionResult> MarkMessageAsRead(string recipientId)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(recipientId))
                {
                    return BadRequest("Recipient ID is required.");
                }

                var userId = User.Identity.Name;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                try
                {
                    await _db.Messages
                        .Where(m => m.RecipientId == userId && m.SenderId == recipientId && !m.IsRead)
                        .ExecuteUpdateAsync(m => m.SetProperty(b => b.IsRead, true));

                    return Ok("Messages marked as read.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return StatusCode(500, "An error occurred while marking messages as read.");
                }

            }

            return Unauthorized("User is not authenticated.");
        }
    }
}
