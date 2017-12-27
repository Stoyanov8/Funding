namespace Funding.Services.Models.ProjectViewModels
{
    using Funding.Data.Models;
    using Funding.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;

    public class DetailsProjectModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Goal { get; set; }

        public decimal MoneyCollected { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Backers { get; set; }

        public string CreatorName { get; set; }

        public IList<Tag> Tags { get; set; }

        public IList<CommentViewModel> Comments { get; set; }

        public string NewComment { get; set; }
      
    }
}