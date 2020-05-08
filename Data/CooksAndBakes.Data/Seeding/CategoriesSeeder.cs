namespace CooksAndBakes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CooksAndBakes.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Title, string DefaultCategoryImage)>
            {
                ("Starters", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964500/Starters_k66o6v.jpg"),
                ("Dressings", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964551/Dressings_ojklhb.jpg"),
                ("Soups", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964594/Soups_exh4wk.jpg"),
                ("Salads", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964629/Salads_ngsrxj.jpg"),
                ("Main Courses", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964658/Main_Courses_kjx3ij.jpg"),
                ("Pastry", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964703/Pastry_lzaycn.jpg"),
                ("Desserts", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964738/Desserts_kt4n42.jpg"),
                ("Cocktails", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964786/Cocktails_ke4gqo.jpg"),
                ("Pizza", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964817/Pizza_a9gtcy.jpg"),
                ("Pasta", "https://res.cloudinary.com/dyk67xps4/image/upload/v1588964842/Pasta_psfqlg.jpg"),
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Title = category.Title,
                    DefaultImageUrl = category.DefaultCategoryImage,
                });
            }
        }
    }
}
