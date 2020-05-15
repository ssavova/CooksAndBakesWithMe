namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllRecipesViewModel
    {
        public ICollection<RecipesViewModel> Recipes { get; set; }
    }
}
