namespace CooksAndBakes.Web.Areas.Administration.Controllers
{
    using CooksAndBakes.Common;
    using CooksAndBakes.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
