namespace CooksAndBakes.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        int GetVotes(string recipeId);

        Task VoteAsync(string recipeId, string userId, bool isUpVote);
    }
}
