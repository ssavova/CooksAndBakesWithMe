namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using CooksAndBakes.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class EditRecipeViewModel : RecipeCreateInputModel
    {
        public string RecipeId { get; set; }

        public ICollection<string> ImageUrls { get; set; }
    }
}
