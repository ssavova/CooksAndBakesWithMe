namespace CooksAndBakes.Data.Models
{
    using System;

    using CooksAndBakes.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string ImageUrl { get; set; }
    }
}
