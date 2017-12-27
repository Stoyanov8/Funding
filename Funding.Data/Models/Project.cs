using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Funding.Common.Constants;
namespace Funding.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ProjectConst.MaxLenght, MinimumLength = ProjectConst.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ProjectConst.MaxLenght, MinimumLength = ProjectConst.DescriptionMinLenght)]
        public string Description { get; set; }

        [Required]
        [StringLength(ProjectConst.MaxLenght, MinimumLength = ProjectConst.ImageUrlMinLenght)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(ProjectConst.MinimumGoal, double.MaxValue)]
        public decimal Goal { get; set; }

        public decimal MoneyCollected { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public bool isApproved { get; set; }

        public IList<Backers> Backers { get; set; } = new List<Backers>();

        public IList<ProjectsTags> Tags { get; set; } = new List<ProjectsTags>();

        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}