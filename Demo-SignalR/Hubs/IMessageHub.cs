namespace Demo_SignalR.Hubs;

/// <summary>
/// Interface IMessageHub defines the contract for receiving chat messages in a SignalR hub. 
/// It contains a single method, RecieveChatMessageAsync, which takes a string message as a parameter and returns a Task. 
/// This interface is implemented by the ChatHub class to allow clients to receive chat messages through SignalR. 
/// The method can be called by the hub to send messages to connected clients, enabling real-time communication in the chat application.
/// </summary>
public interface IMessageHub
{
    /// <summary>
    /// Asynchronously processes an incoming chat message.
    /// </summary>
    /// <param name="message">The text of the chat message to process.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RecieveChatMessageAsync(string message);
}
