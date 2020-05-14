using System;
using System.Collections.Generic;
using System.Text;

namespace CooksAndBakes.Web.ViewModels.Recipes
{
    public class AllUserRecipesViewModel
    {
        public ICollection<UserRecipesViewModel> UserRecipes { get; set; }
    }
}
