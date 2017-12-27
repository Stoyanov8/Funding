namespace Funding.Web.Controllers
{
    using Funding.Common.Constants;
    using Funding.Services.Interfaces;
    using Funding.Services.Models.MailViewModels;
    using Funding.Web.Models.InboxViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class InboxController : Controller
    {
        private readonly IInboxService service;

        public InboxController(IInboxService service)
        {
            this.service = service;
        }

        [Authorize]
        public async Task<IActionResult> All(int page = 1)
        {
            string userName = this.User.Identity.Name;

            var model = await service.GetAllMessages(userName, page);

            return this.View(model);
        }

        [Authorize]
        public IActionResult Send() => this.View();

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Send(SendMessageViewModel messageModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(messageModel);
            }

            string senderName = this.User.Identity.Name;

            bool succes = await this.service.SendMessage(senderName, messageModel.ReceiverName, messageModel.Title, messageModel.Content);

            if (!succes)
            {
                this.TempData[UserConst.TempDataError] = UserConst.UserDoesntExist;
                return this.View(messageModel);
            }
            else
            {
                return this.RedirectToAction(nameof(All));
            }
        }

        public async Task<IActionResult> Details(int messageId)
        {
            var currentUser = this.User.Identity.Name;

            var model = await service.GetSingleMessage(currentUser, messageId);

            return this.View(model);
        }

        public async Task<IActionResult> DeleteFromMine(int messageId)
        {
            var currentUser = this.User.Identity.Name;

            var success = await this.service.DeleteMessage(currentUser, messageId);

            if (!success)
            {
                return this.Unauthorized();
            }
            return this.RedirectToAction(nameof(All),ControllerConst.Inbox);
        }

        public IActionResult Reply(string userName, string title) =>

            this.View(new SendMessageViewModel
            {
                ReceiverName = userName,
                Title = title
            });
    }
}