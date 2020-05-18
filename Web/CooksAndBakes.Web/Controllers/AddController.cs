namespace CooksAndBakes.Web.Controllers
{
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AddController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public AddController(
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AddApiReturnViewModel>> AddRecipeToUser(AddApiInputModel input)
        {
            string returnText = string.Empty;

            var userId = this.userManager.GetUserId(this.User);

            if (this.recipesService.CheckWhetherThisUserHasRecipe(input.RecipeId, userId) == true)
            {
                returnText = "Hey! You already have this recipe in your RecipeBook!";
            }
            else
            {
                await this.recipesService.AddRecipeToUser(input.RecipeId, userId);
                returnText = "You successfully added this recipe to your RecipeBook!";
            }

            var result = new AddApiReturnViewModel { Result = returnText };
            return result;
        }
    }
}
