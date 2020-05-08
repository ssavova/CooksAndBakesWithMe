namespace CooksAndBakes.Web.Controllers
{
    using CooksAndBakes.Services.Data;
    using CooksAndBakes.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService service)
        {
            this.categoriesService = service;
        }
    }
}
