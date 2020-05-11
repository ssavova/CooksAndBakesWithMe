namespace CooksAndBakes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CooksAndBakes.Data.Common.Models;

    public class Recipe : BaseDeletableModel<string>
    {
        public Recipe()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RecipeImages = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
            this.UserRecipes = new HashSet<UserRecipe>();
        }

        public string Title { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Range(1, 5)]
        public int Level { get; set; }

        public string Products { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Image> RecipeImages { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
    }
}
