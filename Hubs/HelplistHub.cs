using Microsoft.AspNetCore.SignalR;
using OperationCHAN.Data;

namespace OperationCHAN.Hubs
{
    public class HelplistHub : Hub
    {
        private ApplicationDbContext _db;
        public HelplistHub(ApplicationDbContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// Adds an entry to the helplist
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="room">The room you are in</param>
        public async Task AddToHelplist(int entryID, string nickname, string description, string room)
        {
            await Clients.All.SendAsync("AddToHelplist", entryID, nickname, description);
        }
        
        /// <summary>
        /// Removes an entry from archive
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="room">The room you are in</param>
        public async Task RemoveFromHelplist(int entryID, string room)
        {
            // This is only a line for testing
            await Clients.All.SendAsync("RemoveFromHelplist", entryID);
        }
        
        /// <summary>
        /// Adds an entry to the archive
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="room">The room you are in</param>
        public async Task AddToArchive(int entryID, string room, string nickname, string description)
        {
            // TODO Set student as finished in the database
            // Remove student from the helplist
            await RemoveFromHelplist(entryID, room);

            SetEntryStatus(entryID, "Finished");
            
            await Clients.All.SendAsync("AddToArchive", entryID, nickname, description);
        }

        private bool SetEntryStatus(int id, string status)
        {
            var entry = _db.HelpList.Where(entry => entry.Id == id).First();
            entry.Status = status;
            _db.SaveChangesAsync();
            return true;
        }
        
        /// <summary>
        /// Removes an entry from archive, and puts it back into the helplist
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="room">The room you are in</param>
        public async Task RemoveFromArchive(int entryID, string room)
        {
            // DO DATABASE SHIT HERE
            
            // This is only a line for testing
            await Clients.All.SendAsync("UnarchivedSuccess", entryID,"Unarchiving", "ID " + entryID + " room " + room);
        }

        /// <summary>
        /// Send a message to a specific group
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nickname"></param>
        /// <param name="description"></param>
        /// <param name="room"></param>
        public async Task SendMessageToGroup(string nickname, string description, string room)
        {
            await Clients.User(room).SendAsync("UserAdded", nickname, description, room);
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