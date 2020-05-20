using System.ComponentModel.DataAnnotations;

namespace CooksAndBakes.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public string RecipeId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
