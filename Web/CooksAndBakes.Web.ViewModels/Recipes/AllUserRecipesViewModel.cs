namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllUserRecipesViewModel
    {
        public ICollection<UserRecipesViewModel> UserRecipes { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
