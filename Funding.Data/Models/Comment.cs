using Funding.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Funding.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ProjectConst.CommentMaxLength, MinimumLength = ProjectConst.CommentMinLength)]
        public string Content { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public DateTime SentDate { get; set; }
    }
}
