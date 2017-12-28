namespace Funding.Services.Services
{
    using Funding.Data;
    using Funding.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly FundingDbContext db;

        public UserService(FundingDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> EmailExist(string email) => await this.db.Users.AnyAsync(x => x.Email == email);

        public async Task<bool> isDeleted(string email)
        {
            var result = await this.db.Users.SingleOrDefaultAsync(x => x.Email == email);
            return result.isDeleted;
        }
        public async Task<string> GetUserFullName(string email)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user.FirstName + " " + user.LastName;
        }
    }
}