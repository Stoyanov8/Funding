using Funding.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Funding.Web.Areas.ProjectAdmin.Controllers
{
    [Area(Area.ProjectAdmin)]
    [Authorize(Roles = Roles.Admin + "," + Roles.ProjectAdmin )]
    public class BaseController:Controller
    {

    }
}
