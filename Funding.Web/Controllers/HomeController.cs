using Funding.Common.Constants;
using Funding.Services.Interfaces;
using Funding.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Funding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var email = this.User.Identity.Name;
            string model = UserConst.Guest;

            if (email != null)
            {
                model = await service.GetUserFullName(email);
            }
            return this.View(nameof(Index),model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}