namespace CooksAndBakes.Web.Controllers
{
    using System;

    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private const int RecipesPerPage = 9;

        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService service)
        {
            this.categoriesService = service;
        }

        public IActionResult GetAll(string id, int page = 1)
        {
            var viewModel = new AllRecipesPerCategoryViewModel()
            {
                Recipes = this.categoriesService.ReturnAllRecipesFromCategory(id, RecipesPerPage, (page - 1) * RecipesPerPage),
            };

            var count = this.categoriesService.CountRecipesPerCategory(id);
            viewModel.NumberOfRecipes = count;

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
