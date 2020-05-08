namespace CooksAndBakes.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CooksAndBakes.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Recipes = new HashSet<Recipe>();
        }

        public string Title { get; set; }

        public string DefaultImageUrl { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
