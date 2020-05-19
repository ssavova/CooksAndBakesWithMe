namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Web.ViewModels.Comments;
    using Ganss.XSS;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public static string SanitizeCommentsContent(string content)
        {
            return new HtmlSanitizer().Sanitize(content);
        }

        public async Task CreateCommentAsync(string recipeId, string userId, ApplicationUser user, string content)
        {
            var newComment = new Comment
            {
                RecipeId = recipeId,
                UserId = userId,
                User = user,
                Content = content,
            };

            await this.commentsRepository.AddAsync(newComment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public List<RecipeCommentsViewModel> ReturnCommentsToRecipe(string recipeId)
        {
            return this.commentsRepository.All().Where(c => c.RecipeId == recipeId).Select(c => new RecipeCommentsViewModel
            {
                AuthorComments = c.User.UserName,
                Content = SanitizeCommentsContent(c.Content),
                CreatedOn = c.CreatedOn,
            }).ToList();
        }
    }
}
