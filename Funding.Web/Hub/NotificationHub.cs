namespace Funding.Web.Hub
{
    using Funding.Common.Constants;
    using Funding.Data;
    using Funding.Data.Models;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotificationHub : Hub
    {
        private readonly FundingDbContext db;
        public NotificationHub(FundingDbContext db)
        {
            this.db = db;
        }
        public async Task Notification(string user)
        {
            var currentUser = await this.db.Users.SingleOrDefaultAsync(x => x.UserName == user);

            await this.Clients.User(currentUser.Id).InvokeAsync(nameof(GetMessages), new { user = currentUser });
        }
        private int GetMessages(User user)
        {
            return user.MessagesReceived.Where(x => x.isRead == false).Count();
        }
    }
}
