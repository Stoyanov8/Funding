using Funding.Common.Constants;
using Funding.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funding.Web.Areas.ProjectAdmin.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService service;

        public ProjectsController(IProjectService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var model = await service.ListAll(page);
            return this.View(model);
        }

        public async Task<IActionResult> ApproveOrDissaprove(int projectId, bool approved)
        {
            var admin = this.User.Identity.Name;           

            if (approved)
            {
                var result = await this.service.Approve(projectId);
                if (result)
                {
                    string userName = await this.service.GetCreatorById(projectId);
                    return this.RedirectToAction(ControllerConst.Reply, ControllerConst.Inbox, new { userName = userName, title = ProjectConst.ProjectApproved, area = ControllerConst.NoArea});
                }
            }
            else
            {
                var result = await this.service.DoNotApprove(projectId);
                if (result == Account.AdminName)
                {
                    return this.RedirectToAction(ControllerConst.Reply, ControllerConst.Inbox, new { userName = result, title = ProjectConst.ProjectNotApproved, area = ControllerConst.NoArea});
                }
            }
            return this.RedirectToAction(nameof(ProjectsController.All), ControllerConst.Projects);
        }

        public async Task<IActionResult> Delete(int projectId)
        {
            bool success = await this.service.DeleteProject(projectId);

            if (!success)
            {
                this.TempData[ProjectConst.TempDataMessage] = ProjectConst.DoesntExist;
            }

            return this.RedirectToAction(nameof(All), new { page = 1 });
        }
    }
}