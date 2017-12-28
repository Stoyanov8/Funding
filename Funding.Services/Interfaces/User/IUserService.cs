namespace Funding.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> EmailExist(string email);

        Task<bool> isDeleted(string email);

        Task<string> GetUserFullName(string email);
    }
}