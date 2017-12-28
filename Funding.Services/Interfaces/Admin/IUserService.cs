namespace Funding.Services.Interfaces.Admin
{
    using Funding.Services.Models.AdminViewModels;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<AdminUsersListingServiceModel> ListAll(int page = 1);

        Task<UserEditViewModel> GetUserById(string userId);

        Task<string> EditUser(string id, string firstName, string lastName, int age, string email, string userName);

        Task<bool> DeleteUser(string userId);

        Task<bool> ActivateUser(string userId);
    }
}