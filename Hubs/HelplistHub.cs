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
        
        /// <summary>
        /// Sends a message to all connected clients
        /// </summary>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="room">The room you are in</param>
        public async Task UnArchive(string nickname, string description, string room)
        {
            await Clients.All.SendAsync("ReceiveMessage", nickname, description, room);
        }
        
        /// <summary>
        /// Send a message to a specific group
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nickname"></param>
        /// <param name="description"></param>
        /// <param name="room"></param>
        public async Task SendMessageToGroup(string user, string nickname, string description, string room)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", nickname, description, room);
        }
        
        /// <summary>
        /// Add the user to the group
        /// </summary>
        /// <param name="groupName"></param>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        /// <summary>
        /// Remove a user from the group
        /// </summary>
        /// <param name="groupName"></param>
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}