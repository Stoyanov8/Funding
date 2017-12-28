using AutoMapper;
using Funding.Data;
using Funding.Data.Models;
using Funding.Web.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funding.Test.Services
{
    public static class DatabaseInitializer
    {
        public static FundingDbContext InitializeForProjectService()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var dbOptions = new DbContextOptionsBuilder<FundingDbContext>()
               .UseInMemoryDatabase("Funding.Test").Options;

            var db = new FundingDbContext(dbOptions);
            var user = new User
            {
                Id = "georgi11",
                UserName = "georgi",
                Email = "georgi@yahoo.com"
            };
            var firstProject = new Project
            {
                Id = 1,
                Name = "first project",
                Creator = user,
                CreatorId = user.Id,
                ImageUrl = "www.abv.bg",
                Goal = 150m,
                MoneyCollected = 5,
                isApproved = true,
                Backers = new List<Backers>()
            };

            firstProject.Backers.Add(new Backers
            {
                User = user,
                UserId = user.Id
            });
            var secondProject = new Project
            {
                Id = 2,
                Name = "second project",
                ImageUrl = "www.abv2.bg",
                Goal = 150m,
                MoneyCollected = 5,
                isApproved = true
            };
            var thirdProject = new Project
            {
                Id = 3,
                Name = "third project",
                ImageUrl = "www.abv3.bg",
                Goal = 150m,
                MoneyCollected = 5,
                isApproved = true
            };

            db.AddRange(firstProject, secondProject, thirdProject, user);
            db.SaveChanges();
            return db;
        }

        public static FundingDbContext InitializeForInboxService()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var dbOptions = new DbContextOptionsBuilder<FundingDbContext>()
               .UseInMemoryDatabase("Funding.Test").Options;
            var db = new FundingDbContext(dbOptions);

            var user = new User
            {
                Id = "georgi11",
                UserName = "georgi",
                Email = "georgi@yahoo.com",
                MessagesReceived = new List<Message>()
            };

            var user1 = new User
            {
                Id = "kolio11",
                UserName = "kolio",
                Email = "kolio@yahoo.com",
                MessagesReceived = new List<Message>()
            };
            var message = new Message
            {
                Id = 1,
                isRead = false,
                Receiver = user,
                ReceiverId = user.Id,
                Sender = user1,
                SenderId = user1.Id,
                Content = "Test purpose",
                Title = "Test",
                SentDate = new DateTime(2017, 12, 22)
            };
            db.AddRange(user1, user, message);
            user.MessagesReceived.Add(message);
            db.SaveChangesAsync();

            return db;
        }

        public static FakeUserManager GetUserManager()
            => new FakeUserManager();
    }
}
