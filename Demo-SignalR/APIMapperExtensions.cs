using Demo_SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Demo_SignalR;

public static class APIMapperExtensions
{
    /// <summary>
    /// This method maps custom API endpoints to the WebApplication.
    /// </summary>
    /// <param name="app"></param>
    public static void MapCustomAPIEndpoints(this WebApplication app)
    {
        MapChatHubWithAuth(app);
        MapChatHubWithAuthAndGroups(app);
    }

    /// <summary>
    /// Maps the chat hub endpoint with authorization to the application's request pipeline, enabling authenticated
    /// users to receive user-specific chat messages via SignalR.
    /// </summary>
    /// <remarks>This method configures a POST endpoint at 'broadcast_to_user' that requires authentication.
    /// Only authenticated users can receive messages through this endpoint. The endpoint sends a chat message to the
    /// user identified by the current authentication context.</remarks>
    /// <param name="app">The web application to which the chat hub endpoint is added.</param>
    private static void MapChatHubWithAuth(this WebApplication app)
    {
        app.MapPost("broadcast_to_user", async (string message,
            IHubContext<ChatHub, IMessageHub> context,
            ClaimsPrincipal claims_principal) =>
        {

            string? user_id = claims_principal.FindFirstValue(ClaimTypes.NameIdentifier);

            await context.Clients.User(user_id!).RecieveChatMessageAsync($"This is a unqiue message for user: {user_id} | Message:" + message);
            return Results.NoContent();
        }).RequireAuthorization();
    }

    /// <summary>
    /// Configures the chat hub endpoint with authentication and group support for the application.
    /// </summary>
    /// <remarks>This method maps a POST endpoint for broadcasting chat messages to all connected clients
    /// using SignalR. It should be called during application startup to enable chat functionality.</remarks>
    /// <param name="app">The web application to configure with the chat hub endpoint.</param>
    private static void MapChatHubWithAuthAndGroups(this WebApplication app)
    {
        app.MapPost("broadcast", async (string message, IHubContext<ChatHub, IMessageHub> context) =>
        {
            await context.Clients.All.RecieveChatMessageAsync(message);
            return Results.NoContent();
        });
    }
}
