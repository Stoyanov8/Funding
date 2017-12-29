namespace Funding.Services.Interfaces
{
    using Funding.Services.Models.ProjectViewModels;
    using System;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task<string> AddProject(string Name,
            string Description,
            string ImageUrl,
            decimal Goal,
            DateTime StarDate,
            DateTime EndDate,
            string userName,
            string tags);

        Task<DetailsProjectModel> GetProjectById(int projectId);

        Task<bool> MakeDonation(int projectId, string userName, string message, decimal amount);

        Task<ProjectsListingHomeViewModel> GetAllProjects(int page);

        Task<bool> ProjectExist(int projectId);

        Task<bool> UserAlreadyDonated(int projectId, string userName);

        Task<MyProjectsListingViewModel> GetMyProjects(int page, string user);

        Task<bool> DeleteProject(string user, int projectId);

        Task<EditProjectModel> GetEditModel(string userName, int Id);

        Task<string> EditProject(int Id,
          string name,
          DateTime startDate,
          DateTime endDate, string description,
          decimal goal,
          string imageUrl);

        Task<ProjectsFundedViewModel> GetFundedProjects(int page, string email);

        Task<bool> DeleteComment(int Id, string userName);

        Task<bool> AddComment(string comment, int projectId, string userName);

        bool IsImageUrl(string URL);

        Task<ProjectsSearchViewModel> GetSearchResults(string searchTerm, bool tag, int page);

        Task<string[]> MostPopularSearches();
    }
}