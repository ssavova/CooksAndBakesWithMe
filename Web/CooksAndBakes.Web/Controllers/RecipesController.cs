namespace CooksAndBakes.Web.Controllers
{
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add()
        {
            var categoriesDropDown = this.categoriesService.GetAll();

            var orderDropDown = this.categoriesService.OrderDropDown(categoriesDropDown, "Starters", "Dressings", "Soups", "Salads", "Main Courses", "Pizza", "Pasta", "Pastry", "Desserts", "Cocktails");

            var viewModel = new RecipeCreateInputModel()
            {
                Categories = orderDropDown,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(RecipeCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string recipeId = await this.recipesService.CreateRecipe(input.Title, input.CategoryId, input.Level, input.Products, input.Description, user.Id);

            var currentRecipe = this.recipesService.ReturnRecipe(recipeId);

            foreach (var image in input.RecipeImages)
            {
                var newImage = await this.recipesService.CreateImage(recipeId, image);
                currentRecipe.RecipeImages.Add(newImage);
            }

            await this.recipesService.AddRecipeToUser(recipeId, user.Id);

            return this.RedirectToAction(nameof(this.ById), new { id = recipeId });
        }

        public IActionResult ById(string id)
        {
            // viewModel
            return this.View();
        }
    }
}
