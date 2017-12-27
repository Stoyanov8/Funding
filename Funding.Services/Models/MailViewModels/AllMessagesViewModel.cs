namespace Funding.Services.Models.MailViewModels
{
    using Funding.Data.Models;
    using Funding.Services.Mapping;
    using System;

    public class AllMessagesViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime SentDate { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        public bool isRead { get; set; }
    }
}