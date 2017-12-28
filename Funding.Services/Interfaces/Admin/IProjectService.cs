namespace Funding.Services.Interfaces.Admin
{
    using Funding.Services.Models.AdminViewModels.Projects;
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task<AdminProjectsServiceModel> ListAll(int page = 1);

        Task<string> DoNotApprove(int projectId);

        Task<bool> Approve(int projectId);

        Task<string> GetCreatorById(int projectId);

        Task<bool> DeleteProject(int projectId);
    }
}