namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;

    using CooksAndBakes.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        List<T> GetCategoriesName<T>();

        List<CategoryMenuViewModel> OrderCategories(List<CategoryMenuViewModel> collection, params string[] orderWay);

        List<CategoryDropDownViewModel> GetAll();

        List<CategoryDropDownViewModel> OrderDropDown(List<CategoryDropDownViewModel> collection, params string[] orderWay);
    }
}
