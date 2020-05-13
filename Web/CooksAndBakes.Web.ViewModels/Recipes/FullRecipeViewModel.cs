namespace CooksAndBakes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    public class FullRecipeViewModel
    {
        public string Title { get; set; }

        public string RecipeId { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryName { get; set; }

        public int Level { get; set; }

        public string Products { get; set; }

        public string Description { get; set; }

        public ICollection<string> ImageUrls { get; set; }


    }
}
