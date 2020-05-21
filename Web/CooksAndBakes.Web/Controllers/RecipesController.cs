namespace CooksAndBakes.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private const int RecipesPerPage = 9;

        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVotesService votesService;
        private readonly ICommentsService commentsService;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager,
            IVotesService votesService,
            ICommentsService commentsService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.votesService = votesService;
            this.commentsService = commentsService;
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

            return this.RedirectToAction(nameof(this.ById), new { recipeId = recipeId });
        }

        [Authorize]
        public async Task<IActionResult> ById(string recipeId)
        {
            var recipe = this.recipesService.ReturnRecipe(recipeId);
            var user = await this.userManager.FindByIdAsync(recipe.UserId);
            var currentlyLoggedUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = new FullRecipeViewModel
            {
                RecipeId = recipe.Id,
                UserId = recipe.UserId,
                Username = user.UserName,
                CurrentlyLoggeduser = currentlyLoggedUser.UserName,
                Title = recipe.Title,
                Level = recipe.Level,
                CreatedOn = recipe.CreatedOn,
                CategoryName = this.categoriesService.ReturnCategoryName(recipe.CategoryId),
                Products = new HtmlSanitizer().Sanitize(recipe.Products),
                Description = new HtmlSanitizer().Sanitize(recipe.Description),
                ImageUrls = this.recipesService.ReturnImageUrls(recipe.Id),
                VotesCount = this.votesService.GetVotes(recipe.Id),
                Comments = this.commentsService.ReturnCommentsToRecipe(recipe.Id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> UserRecipes(int page = 1)
        {
            var currentlyLoggedUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllUserRecipesViewModel
            {
                UserRecipes = this.recipesService.ReturnAllUserRecipes(currentlyLoggedUser.Id, RecipesPerPage, (page - 1) * RecipesPerPage),
            };

            var count = this.recipesService.GetAllRecipesCount(currentlyLoggedUser.Id);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / RecipesPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);

        }

        public IActionResult Edit(string recipeId)
        {
            var categoriesDropDown = this.categoriesService.GetAll();

            var orderDropDown = this.categoriesService.OrderDropDown(categoriesDropDown, "Starters", "Dressings", "Soups", "Salads", "Main Courses", "Pizza", "Pasta", "Pastry", "Desserts", "Cocktails");

            var searchedRecipe = this.recipesService.ReturnRecipe(recipeId);

            var recipeViewModel = new EditRecipeViewModel
            {
                RecipeId = searchedRecipe.Id,
                Title = searchedRecipe.Title,
                CategoryId = searchedRecipe.CategoryId,
                Categories = orderDropDown,
                Level = searchedRecipe.Level,
                Products = new HtmlSanitizer().Sanitize(searchedRecipe.Products),
                Description = new HtmlSanitizer().Sanitize(searchedRecipe.Description),
                ImageUrls = this.recipesService.ReturnImageUrls(searchedRecipe.Id),
            };

            return this.View(recipeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var updatedRecipe = this.recipesService.ReturnRecipe(input.RecipeId);

            await this.recipesService.EditRecipe(input.RecipeId, input.Title, input.CategoryId, input.Level, input.Products, input.Description);

            if (input.RecipeImages.Count() != 0)
            {
                await this.recipesService.DeleteAllCurrentImagesOfRecipe(input.RecipeId);

                foreach (var image in input.RecipeImages)
                {
                    var newImage = await this.recipesService.CreateImage(input.RecipeId, image);
                    updatedRecipe.RecipeImages.Add(newImage);
                }
            }

            return this.RedirectToAction(nameof(this.ById), new { id = input.RecipeId });
        }

        public async Task<IActionResult> Delete(string recipeId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.recipesService.DeleteRecipe(recipeId, userId);

            return this.RedirectToAction(nameof(this.UserRecipes));
        }

        public IActionResult AllRecipes(int page = 1)
        {
            var viewModel = new AllRecipesViewModel
            {
                Recipes = this.recipesService.ReturnAllRecipes(RecipesPerPage, (page - 1) * RecipesPerPage),
            };

            var count = this.recipesService.GetAllRecipesCount();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / RecipesPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

    }
}
