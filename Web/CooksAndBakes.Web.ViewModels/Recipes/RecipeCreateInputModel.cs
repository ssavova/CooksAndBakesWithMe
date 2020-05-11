using CooksAndBakes.Data.Models;
using CooksAndBakes.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CooksAndBakes.Web.ViewModels.Recipes
{
    public class RecipeCreateInputModel
    {
        [Display(Name ="Recipe Name")]
        [Required]
        public string Title { get; set; }

        [Display(Name ="Category")]
        [Required]
        public string CategoryId { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Products { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name ="Images")]
        public IEnumerable<IFormFile> RecipeImages { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
