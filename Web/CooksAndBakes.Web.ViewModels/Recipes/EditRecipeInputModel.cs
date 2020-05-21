namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CooksAndBakes.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class EditRecipeInputModel : RecipeCreateInputModel
    {
        public string RecipeId { get; set; }
    }
}
