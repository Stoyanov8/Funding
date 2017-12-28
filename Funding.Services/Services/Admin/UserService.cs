namespace Funding.Services.Services.Admin
{
    using AutoMapper.QueryableExtensions;
    using Funding.Common.Constants;
    using Funding.Data;
    using Funding.Data.Models;
    using Funding.Services.Interfaces.Admin;
    using Funding.Services.Models.AdminViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly FundingDbContext db;

        public UserService(FundingDbContext db)
        {
            this.db = db;
        }

        public async Task<AdminUsersListingServiceModel> ListAll(int page = 1)
        {
            List<UserListingViewModel> users = null;

            if (page < 1)
            {
                page = 1;
            }

            int totalUsers = await db.Users.CountAsync();

            int numberOfPages = (int)Math.Ceiling((double)totalUsers / Page.UsersSize);

            users = await this.db.Users
                .Where(x => x.Email != Account.AdminName)
                .ProjectTo<UserListingViewModel>().ToListAsync();

            return new AdminUsersListingServiceModel
            {
                Users = users.OrderBy(x => x.isDeleted).Skip((page - 1) * Page.UsersSize).Take(Page.UsersSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<UserEditViewModel> GetUserById(string userId)
            => await this.db
                .Users
                .Where(x => x.Id == userId)
                .ProjectTo<UserEditViewModel>()
                .SingleOrDefaultAsync();

        public async Task<string> EditUser(string userId, string firstName, string lastName, int age, string email, string userName)
        {
            string message = null;

            bool emailAlreadyExist = await this.db.Users.AnyAsync(x => x.Email == email && x.Id != userId);

            bool result = await this.db.Users.AnyAsync(x => x.Id == userId);

            if (!result)
            {
                return message = UserConst.UserDoesntExist;
            }
            if (emailAlreadyExist)
            {
                return message = String.Format(UserConst.EmailTaken, email);
            }

            var user = await UserById(userId);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Age = age;
            user.Email = email;
            user.UserName = userName;
            await this.db.SaveChangesAsync();

            message = String.Format(UserConst.UserUpdated, userName);
            return message;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await this.UserById(userId);

            if (user == null)
            {
                return false;
            }
            user.isDeleted = true;

            await this.db.SaveChangesAsync();
            return true;
        }

        private async Task<User> UserById(string id)
            => await this.db.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ActivateUser(string userId)
        {
            var user = await this.UserById(userId);

            if (user == null)
            {
                return false;
            }
            user.isDeleted = false;

            await this.db.SaveChangesAsync();
            return true;
        }
    }
}