namespace CooksAndBakes.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {
        public IEnumerable<CategoryMenuViewModel> Categories { get; set; }
    }
}
