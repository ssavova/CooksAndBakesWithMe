namespace CooksAndBakes.Web.ViewModels.Categories
{
    using CooksAndBakes.Data.Models;
    using CooksAndBakes.Services.Mapping;

    public class CategoryMenuViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
