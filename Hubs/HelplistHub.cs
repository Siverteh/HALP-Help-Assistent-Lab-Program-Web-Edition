using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol;
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
        /// <param name="entryID">The ID of the ticket in the database</param>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="course">The course you are in</param>
        public async Task AddToHelplist(int entryID, string course, string nickname, string description)
        {
            await Clients.Groups(course).SendAsync("AddToHelplist", entryID, nickname, description);
        }
        
        /// <summary>
        /// Removes an entry from archive
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="course">The course you are in</param>
        public async Task RemoveFromHelplist(int entryID, string course)
        {
            // This is only a line for testing
            await Clients.Groups(course).SendAsync("RemoveFromHelplist", entryID);
        }
        
        /// <summary>
        /// Adds an entry to the archive
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="nickname">The nickname to show</param>
        /// <param name="description">The description to show</param>
        /// <param name="course">The room you are in</param>
        public async Task AddToArchive(int entryID, string course, string nickname, string description)
        {
            // Remove student from the helplist
            await RemoveFromHelplist(entryID, course);

            SetTicketStatus(entryID, "Finished");

            var usr = Clients.Groups(course);
            
            await Clients.Groups(course).SendAsync("AddToArchive", entryID, nickname, description);
        }

        /// <summary>
        /// Removes an entry from archive, and puts it back into the helplist
        /// </summary>
        /// <param name="entryID">The ID of the entry in the database</param>
        /// <param name="course">The course you are in</param>
        public async Task RemoveFromArchive(int entryID, string course, string nickname, string description)
        {
            await AddToHelplist(entryID, course, nickname, description);

            SetTicketStatus(entryID, "Waiting");
            
            await Clients.User(course).SendAsync("RemoveFromArchive", entryID);
        }
        
        private bool SetTicketStatus(int id, string status)
        {
            var entry = _db.HelpList.Where(entry => entry.Id == id).First();
            entry.Status = status;
            _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Send a message to a specific group
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nickname"></param>
        /// <param name="description"></param>
        /// <param name="course"></param>
        public async Task SendMessageToGroup(string nickname, string description, string course)
        {
            await Clients.Group(course).SendAsync("UserAdded", nickname, description, course);
        }
        
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("SendCourseCode");

            return base.OnConnectedAsync();
        }
        
        /// <summary>
        /// Add the user to the group
        /// </summary>
        /// <param name="groupName"></param>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var test = Groups.ToJson();
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