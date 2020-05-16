namespace CooksAndBakes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(string recipeId)
        {
            var votes = this.votesRepository.All()
                .Where(r => r.RecipeId == recipeId)
                .Sum(x => (int)x.Type);

            return votes;
        }

        public async Task VoteAsync(string recipeId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(v => v.RecipeId == recipeId && v.UserId == userId);

            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
