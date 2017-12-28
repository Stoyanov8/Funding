namespace Funding.Services.Services.Admin
{
    using AutoMapper.QueryableExtensions;
    using Funding.Common.Constants;
    using Funding.Data;
    using Funding.Services.Interfaces.Admin;
    using Funding.Services.Models.AdminViewModels.Projects;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectService : IProjectService
    {
        private readonly FundingDbContext db;

        public ProjectService(FundingDbContext db)
        {
            this.db = db;
        }

        public async Task<AdminProjectsServiceModel> ListAll(int page = 1)
        {
            List<ProjectsListingViewModel> projects = null;

            if (page < 1)
            {
                page = 1;
            }

            int totalUsers = await this
                .db
                .Projects
                .CountAsync();

            int numberOfPages = (int)Math.Ceiling((double)totalUsers / Page.ProjectSize);

            projects = await this.db.Projects
                .ProjectTo<ProjectsListingViewModel>()
                .ToListAsync();

            return new AdminProjectsServiceModel
            {
                Projects = projects.OrderBy(x => x.isApproved ? 1 : 0).Skip((page - 1) * Page.ProjectSize).Take(Page.ProjectSize).ToList(),
                Page = page,
                NumberOfPages = numberOfPages
            };
        }

        public async Task<bool> Approve(int projectId)
        {
            var project = await
                this
                .db.Projects
             .SingleOrDefaultAsync(x => x.Id == projectId);

            if (project == null)
            {
                return false;
            }

            if (project.isApproved == true)
            {
                return false;
            }

            project.isApproved = true;

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<string> DoNotApprove(int projectId)
        {
            var project = await this.db.Projects
            .SingleOrDefaultAsync(x => x.Id == projectId);
            string creator = await GetCreatorById(projectId);

            if (project == null)
            {
                return creator = Account.AdminName;
            }

            this.db.Projects.Remove(project);
            await this.db.SaveChangesAsync();
            return creator;
        }

        public async Task<string> GetCreatorById(int projectId)
        {
            var project = await this.db.Projects
              .SingleOrDefaultAsync(x => x.Id == projectId);

            var result = await this.db.Users.SingleOrDefaultAsync(x => x.Id == project.CreatorId);

            return result.UserName;
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            var project = await this.db.Projects.SingleOrDefaultAsync(x => x.Id == projectId);

            if (project == null)
            {
                return false;
            }
            this.db.Remove(project);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}