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
            var viewModel = new AllCategoriesViewModel()
            {
                Categories = this.categoryService.GetCategoriesName<CategoryMenuViewModel>()
            };

            return this.View(viewModel);
        }

}
}
