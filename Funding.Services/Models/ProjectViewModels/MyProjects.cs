namespace Funding.Services.Models.ProjectViewModels
{
    using Funding.Data.Models;
    using Funding.Services.Mapping;

    public class MyProjects: IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Goal { get; set; }

        public decimal MoneyCollected { get; set; }
    }
}
