
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly BookDbContext _db;

        public MessageRepository(BookDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserMessageModel>> GetAllAdminMessages(string userId)
        {
            var userMessages = await _db.Messages
                .Where(m => m.SenderId == userId || m.RecipientId == userId)
                .GroupBy(m => m.SenderId == userId ? m.RecipientId : m.SenderId)
                .Select(g => new UserMessageModel
                {
                    UserId = g.Key,
                    Messages = g.ToList()
                })
                .ToListAsync();
            return userMessages;
        }

        public async Task<List<Message>> GetAllMessagesAsync(string userId)
        {
            return await _db.Messages.Where(m => m.RecipientId == userId || m.SenderId == userId).ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _db.Messages.FindAsync(id);
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var message = await _db.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            return message;
        }
    }
}
