﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CooksAndBakes.Web.ViewModels.Recipes
{
    public class RecipesViewModel
    {
        public string Title { get; set; }

        public string CategoryName { get; set; }

        public int Level { get; set; }

        public string RecipeId { get; set; }

        public string ImageUrl { get; set; }

        public int VoteCount { get; set; }
    }
}
