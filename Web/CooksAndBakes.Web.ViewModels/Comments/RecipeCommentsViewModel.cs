using System;

namespace CooksAndBakes.Web.ViewModels.Comments
{
    public class RecipeCommentsViewModel
    {
        public string AuthorComments { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }
    }
}
