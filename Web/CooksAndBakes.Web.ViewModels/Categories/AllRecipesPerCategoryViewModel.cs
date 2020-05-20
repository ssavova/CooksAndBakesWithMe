using CooksAndBakes.Web.ViewModels.Recipes;
using System.Collections.Generic;

namespace CooksAndBakes.Web.ViewModels.Categories
{
    public class AllRecipesPerCategoryViewModel
    {
        public ICollection<RecipesViewModel> Recipes { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int NumberOfRecipes { get; set; }
    }
}
