namespace Funding.Web.Controllers
{
    using Funding.Common.Constants;
    using Funding.Services.Interfaces;
    using Funding.Services.Models.ProjectViewModels;
    using Funding.Web.Models.ProjectViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await this.service.GetAllProjects(page);
            return View(model);
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectViewModel project)
        {
            var user = this.User.Identity.Name;

            if (!this.ModelState.IsValid)
            {
                return this.View(project);
            }
            var successMessage = await this.service.AddProject
                (project.Name,
                project.Description,
                project.ImageUrl,
                (decimal)project.Goal,
               (DateTime)project.StartDate,
               (DateTime)project.EndDate,
                user,
                project.Tags);

            switch (successMessage)
            {
                case ProjectConst.StartDateMessage:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.StartDateMessage + DateTime.Now.ToShortDateString();
                    return this.View(project);

                case ProjectConst.EndDateMessage:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.EndDateMessage;
                    return this.View(project);

                case ProjectConst.Success:
                    this.TempData[ProjectConst.TempDataAddedProject] = ProjectConst.ProjectAdded;
                    return this.RedirectToAction(nameof(Index), ControllerConst.Project);

                case ProjectConst.Failed:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.Failed;
                    return this.View(project);

                default:
                    return this.View(project);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int projectId)
        {
            var model = await service.GetProjectById(projectId);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return this.View(model);
        }

        public async Task<IActionResult> Donate(int projectId, string returnUrl)
        {
            var user = this.User.Identity.Name;
            bool exist = await this.service.ProjectExist(projectId);
            bool alreadyDonated = await this.service.UserAlreadyDonated(projectId, user);
            if (exist)
            {
                if (!alreadyDonated)
                {
                    return this.View();
                }
            }
            this.TempData[ProjectConst.TempDataSorry] = ProjectConst.AlreadyDonated;
            return this.RedirectToAction(nameof(Details), new { projectId = projectId });
        }

        [HttpPost]
        public async Task<IActionResult> Donate(DonateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = this.User.Identity.Name;
            var result = await this.service.MakeDonation(model.ProjectId, user, model.Message, model.Amount);

            if (result)
            {
                this.TempData[ProjectConst.TempDataDonated] = string.Format(ProjectConst.SuccessfullyDonated, model.Amount);
                return this.RedirectToAction(nameof(Details), new { projectId = model.ProjectId });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> MyProjects(int page = 1)
        {
            string user = this.User.Identity.Name;
            var projects = await this.service.GetMyProjects(page, user);

            return this.View(projects);
        }

        public async Task<IActionResult> Delete(int projectId)
        {
            var user = this.User.Identity.Name;
            bool success = await this.service.DeleteProject(user, projectId);
            if (!success)
            {
                this.TempData[ProjectConst.TempDataUnauthorized] = ProjectConst.DoesntExist;
            }
            else
            {
                this.TempData[ProjectConst.TempDataDeleted] = ProjectConst.ProjectDeleted;
            }
            return this.RedirectToAction(nameof(MyProjects), new { page = 1 });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var user = this.User.Identity.Name;
            var project = await this.service.GetEditModel(user, Id);
            if (project == null)
            {
                this.TempData[ProjectConst.TempDataUnauthorized] = ProjectConst.DoesntExist;
                return this.RedirectToAction(nameof(MyProjects), new { page = 1 });
            }

            return this.View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectModel project)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(project);
            }

            string success = await this.service.
                EditProject(project.Id, project.Name,
               (DateTime)project.StartDate,
               (DateTime)project.EndDate,
                project.Description, (decimal)project.Goal,
                project.ImageUrl);

            switch (success)
            {
                case ProjectConst.SuccesfullyEdited:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.SuccesfullyEdited;
                    return this.RedirectToAction(nameof(MyProjects));

                case ProjectConst.StartDateMessage:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.StartDateMessage + DateTime.Now.ToShortDateString();
                    return this.View(project);

                case ProjectConst.EndDateMessage:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.EndDateMessage;
                    return this.View(project);

                case ProjectConst.Failed:
                    this.TempData[ProjectConst.TempDataMessage] = ProjectConst.Failed;
                    return this.View(project);

                default:
                    return this.View(project);
            }
        }

        public async Task<IActionResult> FundedProjects(int page = 1)
        {
            var email = this.User.Identity.Name;
            var model = await this.service.GetFundedProjects(page, email);

            if (model == null)
            {
                return this.RedirectToAction(nameof(Index), new { page = 1 });
            }
            return this.View(model);
        }

        public async Task<IActionResult> AddComment(string NewComment, int Id)
        {
            if (string.IsNullOrEmpty(NewComment))
            {
                return this.RedirectToAction(nameof(Details), new { projectId = Id });
            }
            var user = this.User.Identity.Name;
            var result = await this.service.AddComment(NewComment, Id, user);

            return this.RedirectToAction(nameof(Details), new { projectId = Id });

        }

        public async Task<IActionResult> DeleteComment(int Id, int projectId)
        {
            var userName = this.User.Identity.Name;

            var result = await this.service.DeleteComment(Id, userName);

            if (result)
            {
                this.TempData[ProjectConst.TempDataDeletedComment] = ProjectConst.DeletedCommentSuccess;
            }
            else
            {
                this.TempData[ProjectConst.TempDataDeletedComment] = ProjectConst.DeletedCommentFailed;

            }
            return this.RedirectToAction(nameof(Details), new { projectId = projectId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchTerm, bool name, bool tag, int page = 1)
        {
            ProjectsSearchViewModel result = null;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                this.TempData[ProjectConst.TempDataNoResults] = ProjectConst.NoResults;
                return this.RedirectToAction(nameof(Index), new { projectId = 1 });
            }
            else
            {
                result = await this.service.GetSearchResults(searchTerm, tag, page);
            }

            if(result.Projects.Count == 0)
            {
                this.TempData[ProjectConst.TempDataNoResults] = ProjectConst.NoResults;
                return this.RedirectToAction(nameof(Index), new { projectId = 1 });
            }
            return this.View(result);
        }
    }
}