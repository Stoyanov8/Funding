namespace Funding.Web.Areas.Admin.Controllers
{
    using Funding.Common.Constants;
    using Funding.Data.Models;
    using Funding.Services.Interfaces.Admin;
    using Funding.Services.Models.AdminViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = Roles.Admin)]
    public class UsersController : BaseController
    {
        private readonly IUserService service;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService service, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var model = await service.ListAll(page);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var userToEdit = await service.GetUserById(userId);

            return this.View(userToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { userId = userModel.userId });
            }

            var result = await this.service
                .EditUser(userModel.userId, userModel.FirstName,
                userModel.LastName,
                userModel.Age, userModel.Email,
                userModel.UserName);

            this.TempData[UserConst.TempDataResult] = result;

            if (!result.Contains(UserConst.Success))
            {
                return RedirectToAction(nameof(Edit), new { userId = userModel.userId });
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddRole(string userId, string role)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(role);
            var user = await this.userManager.FindByIdAsync(userId);
            var userExists = user != null;
            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, UserConst.InvalidIdentity);
            }

            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(All));
            }

            await this.userManager.AddToRoleAsync(user, role);

            this.TempData[UserConst.TempDataResult] = string.Format(UserConst.AddedInRole,user.UserName,role);
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(string userId)
        {
            bool result = await this.service.DeleteUser(userId);

            if (!result)
            {
                this.TempData[UserConst.TempDataResult] = UserConst.UserDoesntExist;
            }
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Activate(string userId)
        {
            bool result = await this.service.ActivateUser(userId);

            if (!result)
            {
                this.TempData[UserConst.TempDataResult] = UserConst.UserDoesntExist;
            }

            return this.RedirectToAction(nameof(All));
        }
    }
}