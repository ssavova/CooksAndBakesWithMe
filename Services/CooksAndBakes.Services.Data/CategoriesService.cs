namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Mapping;
    using CooksAndBakes.Web.ViewModels.Categories;
    using CooksAndBakes.Web.ViewModels.Recipes;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IVotesService votesService;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IVotesService votesService)
        {
            this.categoriesRepository = categoriesRepository;
            this.recipesRepository = recipesRepository;
            this.votesService = votesService;
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

        public List<RecipesViewModel> ReturnAllRecipesFromCategory(string categroryId, int? take = null, int skip = 0)
        {
            var query = this.recipesRepository
                .All()
                .Where(r => r.CategoryId == categroryId)
                .OrderByDescending(r => r.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.Select(r => new RecipesViewModel
            {
                Title = r.Title,
                Level = r.Level,
                CategoryName = r.Category.Title,
                RecipeId = r.Id,
                ImageUrl = r.RecipeImages.Select(u => u.ImageUrl).FirstOrDefault(),
                VoteCount = this.votesService.GetVotes(r.Id),
            }).ToList();
        }

        public int CountRecipesPerCategory(string categoryId)
        {
            return this.recipesRepository.All()
                .Where(r => r.CategoryId == categoryId)
                .Count();
        }
    }
}
