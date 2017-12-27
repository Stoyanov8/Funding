namespace Funding.Services.Models.AdminViewModels.Projects
{
    using Funding.Data.Models;
    using Funding.Services.Mapping;
    using System;

    public class ProjectsListingViewModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Goal { get; set; }

        public decimal MoneyCollected { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public bool isApproved { get; set; }
    }
}