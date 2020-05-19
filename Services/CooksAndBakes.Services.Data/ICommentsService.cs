namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CooksAndBakes.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateCommentAsync(string recipeId, string userId, string content);

        List<RecipeCommentsViewModel> ReturnCommentsToRecipe(string recipeId);

    }
}
