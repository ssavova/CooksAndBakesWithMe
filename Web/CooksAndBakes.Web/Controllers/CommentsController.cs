namespace CooksAndBakes.Web.Controllers
{
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.CreateCommentAsync(input.RecipeId, user.Id, user, input.Content);

            return this.RedirectToAction("ById", "Recipes", new { recipeId = input.RecipeId });
        }
    }
}
