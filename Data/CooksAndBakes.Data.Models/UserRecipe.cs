namespace CooksAndBakes.Data.Models
{
    public class UserRecipe
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
