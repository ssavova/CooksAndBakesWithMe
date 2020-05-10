﻿namespace CooksAndBakes.Services.Data
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
    }
}
