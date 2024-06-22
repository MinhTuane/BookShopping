
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
        public async Task<List<Message>> GetAllMessagesAsync(string userId)
        {
            return await _db.Messages.Where(m => m.SenderId == userId || m.RecipientId == userId)
                .ToListAsync();
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
