namespace BookShopping.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> GetMessageByIdAsync(int id);
        Task<List<Message>> GetAllMessagesAsync(string userId);
        Task<List<UserMessageModel>> GetAllAdminMessages(string userId);
        Task<Message> SendMessageAsync(Message message);
        Task MarkMessageAsReadAsync(int messageId);
    }
}
