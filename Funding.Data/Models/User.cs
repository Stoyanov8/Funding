namespace Funding.Data.Models
{
    using Funding.Common.Constants;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {

        [Required]
        [StringLength(UserConst.UserMaxLenght, MinimumLength = UserConst.UserMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(UserConst.UserMaxLenght, MinimumLength = UserConst.UserMinLength)]
        public string LastName { get; set; }

        [Required]
        [Range(UserConst.MinAge, UserConst.MaxAge, ErrorMessage = UserConst.Age)]
        public int Age { get; set; }

        public bool isDeleted { get; set; }

        public IList<Message> MessagesReceived { get; set; } = new List<Message>();

        public IList<Message> MessagesSent { get; set; } = new List<Message>();

        public IList<Project> ProjectsCreated { get; set; } = new List<Project>();

        public IList<Backers> ProjectsFunded { get; set; } = new List<Backers>();

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}