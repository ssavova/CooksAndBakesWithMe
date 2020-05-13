namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Mapping;
    using CooksAndBakes.Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public List<CategoryDropDownViewModel> GetAll()
        {
            return this.categoriesRepository.All().Select(c => new CategoryDropDownViewModel
            {
                Id = c.Id,
                Title = c.Title,
                DefaultImageUrl = c.DefaultImageUrl,
            }).ToList();
        }

        public List<T> GetCategoriesName<T>()
        {
            return this.categoriesRepository.All()
                .To<T>().ToList();
        }

        public List<CategoryMenuViewModel> OrderCategories(List<CategoryMenuViewModel> collection, params string[] orderWay)
        {
            var orderedCollection = new List<CategoryMenuViewModel>();

            foreach (var word in orderWay)
            {
                var searched = collection.Where(o => o.Title == word).FirstOrDefault();
                orderedCollection.Add(searched);
            }

            return orderedCollection;
        }

        public List<CategoryDropDownViewModel> OrderDropDown(List<CategoryDropDownViewModel> collection, params string[] orderWay)
        {
            var orderedCollection = new List<CategoryDropDownViewModel>();

            foreach (var word in orderWay)
            {
                var searched = collection.Where(o => o.Title == word).FirstOrDefault();
                orderedCollection.Add(searched);
            }

            return orderedCollection;
        }

        public string ReturnCategoryName(string categoryId)
        {
            return this.categoriesRepository.All().Where(c => c.Id == categoryId).Select(c => c.Title).FirstOrDefault();
        }
    }
}
