using CooksAndBakes.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CooksAndBakes.Web.ViewModels.Recipes
{
    public class RecipeCreateInputModel
    {
        public string Title { get; set; }

        [Display(Name ="Category")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
