namespace CooksAndBakes.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CooksAndBakes.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
