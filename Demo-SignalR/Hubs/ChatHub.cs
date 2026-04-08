using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Demo_SignalR.Hubs;

//[Authorize] // Use decorator to indicate if methods from the hub must be authorized
/// <summary>
/// ChatHub is a SignalR hub that allows clients to send and receive chat messages.
/// It implements the IMessageHub interface, which defines the method for receiving chat messages.
/// The hub supports broadcasting messages to all clients, sending messages to specific groups, and sending messages to specific users.
/// The OnConnectedAsync method is overridden to notify all clients when a new client joins the chat. The OnDisconnectedAsync method can be overridden to handle client disconnections if needed.
/// </summary>
public sealed class ChatHub : Hub<IMessageHub>
{
    // <inheritdoc />
    public override async Task OnConnectedAsync()
    {
        await this.Clients.All.RecieveChatMessageAsync($"{this.Context.ConnectionId} has joined the chat.");
        await base.OnConnectedAsync();
    }

    // <inheritdoc />
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await this.Clients.All.RecieveChatMessageAsync($"{this.Context.ConnectionId} left the chat.");
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Sends a chat message to all connected clients. The message is prefixed with the sender's connection ID for identification.
    /// </summary>
    /// <param name="message">The text of the chat message to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SendMessageToAllAsync(string message)
    {
        await Clients.All.RecieveChatMessageAsync($"{Context.ConnectionId} : {message}");
    }

    /// <summary>
    /// Sends a chat message to the specified groups asynchronously.
    /// </summary>
    /// <remarks>If any group name in the collection does not exist, the message will not be delivered to that
    /// group. The message is sent to all specified groups in parallel.</remarks>
    /// <param name="message">The message text to send to each group.</param>
    /// <param name="group_names">A collection of group names that will receive the message. Cannot be null or contain null values.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    public async Task SendMessageToGroup(string message, IEnumerable<string> group_names)
    {
        await Clients.Groups(group_names).RecieveChatMessageAsync($"{message} : Targeted groups: {string.Join(",", group_names)}");
    }

    /// <summary>
    /// Sends a chat message to the specified users asynchronously.
    /// </summary>
    /// <param name="message">The message text to send to the users. Cannot be null.</param>
    /// <param name="users">A collection of user identifiers representing the recipients of the message. Cannot be null or contain null
    /// elements.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    public async Task SendMessageToUsers(string message, IEnumerable<string> users)
    {
        await Clients.Users(users).RecieveChatMessageAsync($"{message} : Users: {string.Join(",", users)}");
    }

}
