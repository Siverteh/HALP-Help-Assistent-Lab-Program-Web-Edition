using Microsoft.AspNetCore.SignalR;

namespace OperationCHAN.Hubs
{
    public class HelplistHub : Hub
    {
        /// <summary>
        /// Sends a message to all connected clients
        /// </summary>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="room">The room you are in</param>
        public async Task SendMessage(string nickname, string description, string room)
        {
            await Clients.All.SendAsync("ReceiveMessage", nickname, description, room);
        }
    }
}