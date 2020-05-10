namespace CooksAndBakes.Web.Infrastructure
{
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "MenuNavBar")]
    public class MenuNavBarViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoryService;

        public MenuNavBarViewComponent(ICategoriesService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var unorderedCollection = this.categoryService.GetCategoriesName<CategoryMenuViewModel>();

            var orderedCollection = this.categoryService.OrderCategories(unorderedCollection, "Starters", "Dressings", "Soups", "Salads", "Main Courses", "Pizza", "Pasta", "Pastry", "Desserts", "Cocktails");

            var viewModel = new AllCategoriesViewModel() { Categories = orderedCollection };

            return this.View(viewModel);
        }

    }
}
