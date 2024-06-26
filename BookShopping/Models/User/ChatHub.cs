using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class ChatHub : Hub
{
    private readonly BookDbContext _DbContext;

    public ChatHub(BookDbContext context)
    {
        _DbContext = context;
    }

    public async Task<List<string>> GetMessagesWithUser(string userId, string otherUserId)
    {
        var messages = await _DbContext.Messages
            .Where(m => (m.SenderId == userId && m.RecipientId == otherUserId) ||
                        (m.SenderId == otherUserId && m.RecipientId == userId))
            .OrderBy(m => m.SentAt)
            .Select(m => m.Content)
            .ToListAsync();

        return messages;
    }
    public async Task SendMessageToUser(string recipient, string message)
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

        await Clients.Group(recipient).SendAsync("ReceiveMessage", sender, message);
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User.Identity.Name;
        if (!string.IsNullOrEmpty(userName))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        }

        await base.OnConnectedAsync();
    }
}
