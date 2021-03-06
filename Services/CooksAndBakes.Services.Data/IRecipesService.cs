﻿namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        bool CheckWhetherThisUserHasRecipe(string recipeId, string userId);

        Task<Image> CreateImage(string recipeId, IFormFile file);

        Task<string> CreateRecipe(string title, string categoryId, int level, string products, string description, string userId);

        Task AddRecipeToUser(string recipeId, string userId);

        Task<string> UploadImagesForRecipe(IFormFile file);

        Recipe ReturnRecipe(string id);

        List<string> ReturnImageUrls(string recipeId);

        List<UserRecipesViewModel> ReturnAllUserRecipes(string userId, int? take = null, int skip = 0);

        List<RecipesViewModel> ReturnAllRecipes(int? take = null, int skip = 0);

        Task DeleteRecipe(string recipeId, string userId);

        int GetAllRecipesCount(string userdId = null);

        Task DeleteAllCurrentImagesOfRecipe(string recipeId);

        Task EditRecipe(string recipeId, string title, string categoryId, int level, string products, string description);
    }
}
