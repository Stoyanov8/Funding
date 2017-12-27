namespace Funding.Web.Areas.Admin.Controllers
{
    using Funding.Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(Area.Admin)]
    [Authorize(Roles = Roles.Admin)]
    public class BaseController : Controller
    {
    }
}