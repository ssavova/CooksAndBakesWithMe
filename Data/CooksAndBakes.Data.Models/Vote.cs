namespace CooksAndBakes.Data.Models
{
    using System;

    using CooksAndBakes.Data.Common.Models;

    public class Vote : BaseDeletableModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public VoteType Type { get; set; }
    }
}
