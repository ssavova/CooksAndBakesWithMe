﻿namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Http;

    public class RecipesService : IRecipesService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRepository<UserRecipe> userRecipesRepository;
        private readonly IDeletableEntityRepository<Vote> votesRepository;
        private readonly IVotesService votesService;

        public RecipesService(
            Cloudinary cloudinary,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IRepository<UserRecipe> userRecipes,
            IDeletableEntityRepository<Vote> votesRepository,
            IVotesService votesService)
        {
            this.cloudinary = cloudinary;
            this.imagesRepository = imagesRepository;
            this.recipesRepository = recipesRepository;
            this.userRecipesRepository = userRecipes;
            this.votesRepository = votesRepository;
            this.votesService = votesService;
        }

        public async Task<Image> CreateImage(string recipeId, IFormFile file)
        {
            var url = await this.UploadImagesForRecipe(file);

            var image = new Image
            {
                RecipeId = recipeId,
                ImageUrl = url,
            };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();

            return image;
        }

        public async Task<string> CreateRecipe(string title, string categoryId, int level, string products, string description, string userId)
        {
            var recipe = new Recipe()
            {
                Title = title,
                CategoryId = categoryId,
                Level = level,
                Products = products,
                Description = description,
                UserId = userId,
            };

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
        }

        public async Task AddRecipeToUser(string recipeId, string userId)
        {
            var userRecipe = new UserRecipe
            {
                RecipeId = recipeId,
                UserId = userId,
            };

            await this.userRecipesRepository.AddAsync(userRecipe);
            await this.userRecipesRepository.SaveChangesAsync();
        }

        public async Task<string> UploadImagesForRecipe(IFormFile file)
        {
            string imageUrl = string.Empty;

            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                    Transformation = new Transformation()
                    .Width(600)
                    .Crop("scale")
                    .Chain()
                    .Quality("auto")
                    .FetchFormat("auto"),
                };

                var result = await this.cloudinary.UploadAsync(uploadParams);

                imageUrl = result.Uri.AbsoluteUri;
            }

            return imageUrl;
        }

        public Recipe ReturnRecipe(string id)
        {
            return this.recipesRepository.All().Where(r => r.Id == id).FirstOrDefault();
        }

        public List<string> ReturnImageUrls(string recipeId)
        {
            var result = new List<string>();
            var images = this.recipesRepository.All().Where(r => r.Id == recipeId).Select(r => r.RecipeImages).FirstOrDefault();

            foreach (var url in images)
            {
                result.Add(url.ImageUrl);
            }

            return result;
        }

        public List<UserRecipesViewModel> ReturnAllUserRecipes(string userId, int? take = null, int skip = 0)
        {
            var allUserRecipesId = this.userRecipesRepository.All()
                .Where(r => r.UserId == userId)
                .Select(r => r.RecipeId).Skip(skip).ToList();

            var allUserRecipes = new List<UserRecipesViewModel>();

            foreach (var rId in allUserRecipesId)
            {
                var recipe = this.recipesRepository.All().Where(r => r.Id == rId).Select(nr => new UserRecipesViewModel
                {
                    Title = nr.Title,
                    RecipeId = nr.Id,
                    CategoryName = nr.Category.Title,
                    Level = nr.Level,
                    ImageUrl = nr.RecipeImages.FirstOrDefault().ImageUrl,
                    CreatedOn = nr.CreatedOn,
                }).FirstOrDefault();

                allUserRecipes.Add(recipe);
            }

            allUserRecipes = allUserRecipes.OrderByDescending(t => t.CreatedOn).ToList();

            if (take.HasValue)
            {
                allUserRecipes = allUserRecipes.Take(take.Value).ToList();
            }

            return allUserRecipes;
        }

        public bool CheckWhetherThisUserHasRecipe(string recipeId, string userId)
        {
            return this.userRecipesRepository.All().Any(ur => ur.RecipeId == recipeId && ur.UserId == userId);
        }

        public List<RecipesViewModel> ReturnAllRecipes(int? take = null, int skip = 0)
        {
            var query = this.recipesRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.Select(r => new RecipesViewModel
            {
                Title = r.Title,
                Level = r.Level,
                CategoryName = r.Category.Title,
                RecipeId = r.Id,
                ImageUrl = r.RecipeImages.Select(u => u.ImageUrl).FirstOrDefault(),
                VoteCount = this.votesService.GetVotes(r.Id),
            }).ToList();
        }

        public async Task DeleteRecipe(string recipeId, string userId)
        {
            var searchedPair = this.userRecipesRepository
                .All()
                .Where(r => r.RecipeId == recipeId && r.UserId == userId)
                .FirstOrDefault();

            this.userRecipesRepository.Delete(searchedPair);
            await this.userRecipesRepository.SaveChangesAsync();
        }

        public int GetAllRecipesCount(string userId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.recipesRepository.All().Count();
            }
            else
            {
                return this.recipesRepository.All().Where(r => r.UserId == userId).Count();
            }
        }

        public async Task EditRecipe(string recipeId, string title, string categoryId, int level, string products, string description)
        {
            var editrecipe = this.ReturnRecipe(recipeId);
            editrecipe.Title = title;
            editrecipe.CategoryId = categoryId;
            editrecipe.Level = level;
            editrecipe.Products = products;
            editrecipe.Description = description;

            this.recipesRepository.Update(editrecipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAllCurrentImagesOfRecipe(string recipeId)
        {
            var collectionRecipesImages = this.imagesRepository.All().Where(i => i.RecipeId == recipeId).ToList();

            foreach (var img in collectionRecipesImages)
            {
                img.IsDeleted = true;
                img.RecipeId = null;
                this.imagesRepository.Update(img);
            }

            await this.imagesRepository.SaveChangesAsync();
        }

    }
}
