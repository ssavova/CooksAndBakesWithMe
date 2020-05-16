namespace CooksAndBakes.Web.Controllers
{
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VoteResponseModel>> Reaction(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.VoteAsync(input.RecipeId, userId, input.IsUpVote);

            var votes = this.votesService.GetVotes(input.RecipeId);

            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
