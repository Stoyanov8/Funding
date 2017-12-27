namespace Funding.Services.Services
{
    using AutoMapper.QueryableExtensions;
    using Funding.Common.Constants;
    using Funding.Data;
    using Funding.Data.Models;
    using Funding.Services.Interfaces;
    using Funding.Services.Models.MailViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InboxService : IInboxService
    {
        private readonly FundingDbContext db;

        public InboxService(FundingDbContext db)
        {
            this.db = db;
        }

        public async Task<MessagesListViewModel> GetAllMessages(string name, int page = 1)
        {
            List<AllMessagesViewModel> messages = null;

            if (page < 1)
            {
                page = 1;
            }

            double totalMessages = await this.db.Messages
                          .Where(x => x.Receiver.UserName == name)
                          .CountAsync();

            messages = await this.db.Messages
                  .Where(x => x.Sender.UserName == name)
                  .ProjectTo<AllMessagesViewModel>().ToListAsync();

            var numberOfpages = (int)Math.Ceiling(totalMessages / Page.InboxSize);

            if (page > numberOfpages)
            {
                page = numberOfpages;
            }
            return new MessagesListViewModel
            {
                Messages = messages.OrderByDescending(x => x.SentDate).Skip((page - 1) * Page.InboxSize).Take(Page.InboxSize),
                Page = page,
                NumberOfPages = numberOfpages
            };
        }

        public async Task<bool> SendMessage(string senderName, string receiverName, string title, string content)
        {
            var receiver = await this.GetUserByName(receiverName);

            var sender = await this.GetUserByName(senderName);

            if (receiver == null)
            {
                return false;
            }
            else
            {
                Message message = new Message
                {
                    Title = title,
                    Content = content,
                    Receiver = receiver,
                    ReceiverId = receiver.Id,
                    SenderId = sender.Id,
                    Sender = sender,
                    isRead = false,
                    SentDate = DateTime.Now
                };

                receiver.MessagesReceived.Add(message);

                await this.db.SaveChangesAsync();

                return true;
            }
        }

        public async Task<AllMessagesViewModel> GetSingleMessage(string currentUser, int messageId)
        {
            if (!await UserIsAuthorized(currentUser, messageId))
            {
                return new AllMessagesViewModel
                {
                    Title = Guard.Title
                };
            }

            var message = await this
             .db
             .Messages
             .SingleOrDefaultAsync(m => m.Id == messageId);

            message.isRead = true;

            await this.db.SaveChangesAsync();

            return await db.Messages
                .Where(m => m == message)
                .ProjectTo<AllMessagesViewModel>().SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteMessage(string currentUser, int messageId)
        {
            var user = await this.GetUserByName(currentUser);

            var messageToRemove = await this
                .db
                .Messages
                .SingleOrDefaultAsync(m => m.Id == messageId);

            if (!await UserIsAuthorized(currentUser, messageId))
            {
                return false;
            }

            this.db.Messages.Remove(messageToRemove);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetUnreadMessages(string userName) =>
           await this.db.Messages
           .Where(x => x.Sender.UserName == userName && x.isRead == false)
           .CountAsync();

        private async Task<User> GetUserByName(string name)
            => await this.db.Users.SingleOrDefaultAsync(u => u.UserName == name);

        private async Task<bool> UserIsAuthorized(string name, int messageId)
            => await this.db.Messages.AnyAsync(x => x.Sender.UserName == name && x.Id == messageId);
    }
}