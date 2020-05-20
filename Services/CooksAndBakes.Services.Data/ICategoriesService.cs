namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;

    using CooksAndBakes.Web.ViewModels.Categories;
    using CooksAndBakes.Web.ViewModels.Recipes;

    public interface ICategoriesService
    {
        List<T> GetCategoriesName<T>();

        List<CategoryMenuViewModel> OrderCategories(List<CategoryMenuViewModel> collection, params string[] orderWay);

        List<CategoryDropDownViewModel> GetAll();

        List<CategoryDropDownViewModel> OrderDropDown(List<CategoryDropDownViewModel> collection, params string[] orderWay);

        string ReturnCategoryName(string categoryId);

        List<RecipesViewModel> ReturnAllRecipesFromCategory(string categroryId, int? take = null, int skip = 0);

        int CountRecipesPerCategory(string categoryId);
    }
}
