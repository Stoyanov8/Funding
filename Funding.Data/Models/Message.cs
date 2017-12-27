namespace Funding.Data.Models
{
    using Funding.Common.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MessageConst.MaxLength, MinimumLength = MessageConst.MinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(MessageConst.MaxLength, MinimumLength = MessageConst.MinLength)]
        public string Content { get; set; }
        [Required]
        public DateTime SentDate { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        [Required]
        public bool isRead { get; set; }
    }
}