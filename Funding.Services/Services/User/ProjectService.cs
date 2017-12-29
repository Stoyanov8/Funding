using AutoMapper.QueryableExtensions;
using Funding.Common.Constants;
using Funding.Data;
using Funding.Data.Models;
using Funding.Services.Interfaces;
using Funding.Services.Models.ProjectViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funding.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FundingDbContext db;
        private readonly UserManager<User> userManager;
        public ProjectService(FundingDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<ProjectsListingHomeViewModel> GetAllProjects(int page)
        {
            List<ProjectsHome> projects = null;

            if (page < 1)
            {
                page = 1;
            }

            int totalProjects = await db.Projects.CountAsync();

            int numberOfPages = (int)Math.Ceiling((double)totalProjects / Page.UsersSize);

            projects = await this
                .db
                .Projects
                .Where(x => x.isApproved == true)
                .ProjectTo<ProjectsHome>()
                .ToListAsync();

            return new ProjectsListingHomeViewModel
            {
                Projects = projects.OrderByDescending(x => x.Id).Skip((page - 1) * Page.ProjectHomeSize).Take(Page.ProjectHomeSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<string> AddProject(string Name,
            string Description,
            string ImageUrl,
            decimal Goal,
            DateTime StarDate,
            DateTime EndDate,
            string userName,
            string tags)

        {
            string message = string.Empty;

            if (StarDate < DateTime.Now)
            {
                return message = ProjectConst.StartDateMessage;
            }
            if (EndDate < StarDate)
            {
                return message = ProjectConst.EndDateMessage;
            }

            var user = await GetUserByName(userName);

            var allTags = tags.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            try
            {
                var project = new Project
                {
                    Name = Name,
                    Description = Description,
                    ImageUrl = ImageUrl,
                    Goal = Goal,
                    StartDate = StarDate,
                    EndDate = EndDate,
                    Creator = user,
                    CreatorId = user.Id,
                };
                foreach (var tag in allTags)
                {
                    var tagExist = await this.db.Tags.SingleOrDefaultAsync(x => x.Name.ToLower() == tag.ToLower());

                    if (tagExist == null)
                    {
                        project.Tags.Add(new ProjectsTags
                        {
                            Tag = new Tag
                            {
                                Name = tag,
                            }
                        });
                    }
                    else
                    {
                        project.Tags.Add(new ProjectsTags
                        {
                            Tag = tagExist
                        });
                    }
                }

                await this.db.Projects.AddAsync(project);
                await this.db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return message = ProjectConst.Failed;
            }
            return message = ProjectConst.Success;
        }

        public async Task<DetailsProjectModel> GetProjectById(int projectId) =>
            await this.db
                .Projects
                .Where(x => x.Id == projectId)
                .Select(x => new DetailsProjectModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    MoneyCollected = x.MoneyCollected,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Goal = x.Goal,
                    Comments = x.Comments.Select(a => new CommentViewModel
                    {
                        Id = a.Id,
                        Content = a.Content,
                        SentDate = a.SentDate,
                        User = this.db.Users.SingleOrDefault(b => b.Id == a.UserId).Email,

                    }).OrderByDescending(a => a.Id).ToList(),
                    CreatorName = this.db.Users.SingleOrDefault(a => a.Id == x.CreatorId).UserName,
                    Backers = x.Backers.Where(a => a.ProjectId == x.Id).Count(),
                    Tags = this.db.ProjectTags.Where(t => t.ProjectId == x.Id).Select(a => new Tag
                    {
                        Name = a.Tag.Name
                    }).ToList(),

                }).SingleOrDefaultAsync();

        public async Task<bool> MakeDonation(int projectId, string userName, string message, decimal amount)
        {
            var project = await GetProject(projectId);
            var user = await this.GetUserByName(userName);

            try
            {
                project.MoneyCollected += amount;
                if (!project.Backers.Any(x => x.User == user))
                {
                    project.Backers.Add(new Backers
                    {
                        User = user
                    });
                }

                var creator = await this.db.Users.SingleOrDefaultAsync(x => x.Id == project.CreatorId);

                creator.MessagesSent.Add(new Message
                {
                    Sender = user,
                    SenderId = user.Id,
                    Title = String.Format(ProjectConst.ProjectFunded, project.Name, amount),
                    Content = message,
                    Receiver = creator,
                    ReceiverId = creator.Id
                });
                await this.db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MyProjectsListingViewModel> GetMyProjects(int page, string userName)
        {
            var user = await this.GetUserByName(userName);

            List<MyProjects> projects = null;

            if (page < 1)
            {
                page = 1;
            }

            projects = await this
            .db
            .Projects
            .Where(x => x.CreatorId == user.Id)
            .ProjectTo<MyProjects>()
            .ToListAsync();

            int totalProjects = projects.Count();

            int numberOfPages = (int)Math.Ceiling((double)totalProjects / Page.UsersSize);

            if (page > numberOfPages)
            {
                page = 1;
            }
            return new MyProjectsListingViewModel
            {
                Projects = projects.OrderByDescending(x => x.Id).Skip((page - 1) * Page.ProjectHomeSize).Take(Page.ProjectHomeSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<bool> UserAlreadyDonated(int projectId, string userName)
        {
            var user = await this.GetUserByName(userName);
            return await this.db.Projects.AnyAsync(x => x.Backers.Any(a => a.UserId == user.Id && a.ProjectId == projectId));
        }

        public async Task<bool> DeleteProject(string userName, int projectId)
        {
            var user = await this.GetUserByName(userName);
            var project = await this.GetProject(projectId);

            if (!this.db.Projects.Any(x => x.Id == projectId && x.CreatorId == user.Id))
            {
                return false;
            }
            this.db.Remove(project);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<EditProjectModel> GetEditModel(string userName, int projectId)
        {
            var user = await this.GetUserByName(userName);
            var project = await this.GetProject(projectId);

            if (user == null || project == null)
            {
                return null;
            }
            if (!this.db.Projects.Any(x => x.Id == projectId && x.CreatorId == user.Id))
            {
                return null;
            }

            return await this.db
                 .Projects
                 .Where(x => x.Id == projectId)
                 .ProjectTo<EditProjectModel>()
                 .SingleOrDefaultAsync();
        }

        public async Task<string> EditProject(int projectId,
            string name,
            DateTime startDate,
            DateTime endDate, string description,
            decimal goal,
            string imageUrl)
        {
            string message = string.Empty;

            var project = await this.GetProject(projectId);

            if (startDate < DateTime.Now)
            {
                return message = ProjectConst.StartDateMessage;
            }
            if (endDate < startDate)
            {
                return message = ProjectConst.EndDateMessage;
            }

            project.Name = name;
            project.Description = description;
            project.ImageUrl = imageUrl;
            project.Goal = goal;
            project.StartDate = startDate;
            project.EndDate = endDate;

            await this.db.SaveChangesAsync();

            return message = ProjectConst.SuccesfullyEdited;
        }

        public async Task<ProjectsFundedViewModel> GetFundedProjects(int page, string email)
        {
            var user = await this.GetUserByName(email);

            List<ProjectsFunded> projects = null;

            if (page < 1)
            {
                page = 1;
            }


            projects = await this.db.Backers
               .Where(x => x.UserId == user.Id)
               .Select(x => x.Project)
               .ProjectTo<ProjectsFunded>()
               .ToListAsync();

            int totalProjects = projects.Count();

            int numberOfPages = (int)Math.Ceiling((double)totalProjects / Page.UsersSize);
            if (page > numberOfPages)
            {
                page = 1;
            }
            return new ProjectsFundedViewModel
            {
                Projects = projects.OrderByDescending(x => x.Id).Skip((page - 1) * Page.ProjectHomeSize).Take(Page.ProjectHomeSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        public bool IsImageUrl(string project)
        {
            if (!project.EndsWith(ProjectConst.Jpeg) && !project.EndsWith(ProjectConst.Jpg) && !project.EndsWith(ProjectConst.Png) && !project.EndsWith(ProjectConst.Gif))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddComment(string comment, int projectId, string userName)
        {
            var project = await this.GetProject(projectId);
            var user = await this.GetUserByName(userName);

            if (project == null || user == null)
            {

                return false;
            }

            project.Comments.Add(new Comment
            {
                Project = project,
                ProjectId = project.Id,
                User = user,
                UserId = user.Id,
                Content = comment,
                SentDate = DateTime.Now
            });
            await this.db.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteComment(int Id, string userName)
        {
            var user = await this.GetUserByName(userName);

            bool result = await UsersComment(Id, user.Id);

            if (!result && !await UserIsInRole(user))
            {
                return false;
            }

            var commentToRemove = await GetCommentById(Id);

            if (commentToRemove == null)
            {
                return false;
            }

            this.db.Remove(commentToRemove);
            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<bool> UserIsInRole(User user)
        {
            var isAdmin = await this.userManager.IsInRoleAsync(user, Roles.Admin);
            var isProjectManager = await this.userManager.IsInRoleAsync(user, Roles.ProjectAdmin);

            if (isAdmin || isProjectManager)
            {
                return true;
            }
            return false;
        }


        public async Task<ProjectsSearchViewModel> GetSearchResults(string searchTerm, bool tag, int page)
        {
            List<ProjectsHome> projects = new List<ProjectsHome>();

            if (page < 1)
            {
                page = 1;
            }

            if (tag)
            {
                var tagProjects = await this.db
                    .ProjectTags
                    .Where(x => x.Tag.Name.ToLower()
                    .Contains(searchTerm.ToLower()))
                    .Select(x => x.Project)
                    .ProjectTo<ProjectsHome>()
                    .ToListAsync();

                var nameProjects = await this.db
                  .Projects
                  .Where(x => x.Name.ToLower()
                  .Contains(searchTerm.ToLower()))
                  .ProjectTo<ProjectsHome>()
                  .ToListAsync();

                projects.AddRange(tagProjects);

                projects.AddRange(nameProjects);

                projects = projects.GroupBy(x => x.Id).Select(group => group.First()).ToList();
            }

            else
            {
                projects = await this.db.Projects
                  .Where(x => x.Name.ToLower()
                  .Contains(searchTerm.ToLower()))
                  .ProjectTo<ProjectsHome>()
                  .ToListAsync();
            }

            if (projects == null)
            {
                return null;
            }

            await this.AddSearchInDatabase(searchTerm);

            int totalProjects = projects.Count();

            int numberOfPages = (int)Math.Ceiling((double)totalProjects / Page.UsersSize);

            if (page > numberOfPages)
            {
                page = 1;
            }
            return new ProjectsSearchViewModel
            {
                Projects = projects
                .OrderByDescending(x => x.Id).Skip((page - 1) * Page.ProjectHomeSize)
                .Take(Page.ProjectHomeSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        private async Task AddSearchInDatabase(string searchTerm)
        {
            SearchHistory search = await this.db.Searches.SingleOrDefaultAsync(x => x.Name == searchTerm);

            if (search == null)
            {
                await this.db.AddAsync(new SearchHistory
                {
                    Name = searchTerm,
                    SearchCount = 1
                });
            }
            else
            {
                search.SearchCount++;
            }
            await this.db.SaveChangesAsync();
        }

        public async Task<string[]> MostPopularSearches()       
            =>  await this.db.Searches.OrderByDescending(x => x.SearchCount).Select(x=> x.Name).ToArrayAsync();
       
        private async Task<Comment> GetCommentById(int id)
          => await this.db.Comments.SingleOrDefaultAsync(x => x.Id == id);

        private async Task<bool> UsersComment(int commentId, string userId)
            => await this.db.Comments.AnyAsync(x => x.UserId == userId && x.Id == commentId);

        private async Task<User> GetUserByName(string name)
          => await this.db.Users.SingleOrDefaultAsync(x => x.UserName == name);

        private async Task<Project> GetProject(int projectId)
            => await this.db.Projects.SingleOrDefaultAsync(x => x.Id == projectId);

        public async Task<bool> ProjectExist(int projectId) => await this.db.Projects.AnyAsync(x => x.Id == projectId);
    }
}