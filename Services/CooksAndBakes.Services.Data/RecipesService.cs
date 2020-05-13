namespace CooksAndBakes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CooksAndBakes.Data.Common.Repositories;
    using CooksAndBakes.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class RecipesService : IRecipesService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRepository<UserRecipe> userRecipesRepository;

        public RecipesService(
            Cloudinary cloudinary,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IRepository<UserRecipe> userRecipes)
        {
            this.cloudinary = cloudinary;
            this.imagesRepository = imagesRepository;
            this.recipesRepository = recipesRepository;
            this.userRecipesRepository = userRecipes;
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
    }
}
