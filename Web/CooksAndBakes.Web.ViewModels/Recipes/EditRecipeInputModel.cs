namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class EditRecipeInputModel : RecipeCreateInputModel
    {
        public string RecipeId { get; set; }

        public ICollection<string> ImageUrls { get; set; }

        public new IEnumerable<IFormFile> RecipeImages { get; set; }
    }
}
