using System;
using System.Collections.Generic;
using System.Text;

namespace CooksAndBakes.Web.ViewModels.Categories
{
    public class AllCategoriesViewModel
    {
        public IEnumerable<CategoryMenuViewModel> Categories { get; set; }
    }
}
