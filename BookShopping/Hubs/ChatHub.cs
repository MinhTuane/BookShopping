using Microsoft.AspNetCore.SignalR;

namespace BookShopping.Hubs
{
    public class ChatHub : Hub
    {
        private readonly BookDbContext _DbContext;

        public ChatHub(BookDbContext context)
        {
            _DbContext = context;
        }

        public async Task SendMessage(string recipient, string message)
        {
            var sender = Context.User.Identity.Name;

            if (string.IsNullOrEmpty(sender))
            {
                throw new ArgumentNullException(nameof(sender));
            }

            var newMessage = new Message
            {
                SenderId = sender,
                RecipientId = recipient,
                Content = message,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _DbContext.Messages.Add(newMessage);
            await _DbContext.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", sender, message, newMessage.SentAt);
        }
    }
}
