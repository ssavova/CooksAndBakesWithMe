namespace CooksAndBakes.Web.Controllers
{
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public RecipesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var categoriesDropDown = this.categoriesService.GetAll();

            var orderDropDown = this.categoriesService.OrderDropDown(categoriesDropDown, "Starters", "Dressings", "Soups", "Salads", "Main Courses", "Pizza", "Pasta", "Pastry", "Desserts", "Cocktails");

            var viewModel = new RecipeCreateInputModel()
            {
                Categories = orderDropDown
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(RecipeCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            //Create Recipe and Save it to db

            return this.Redirect("/");
        }
    }
}
