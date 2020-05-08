namespace CooksAndBakes.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetCategoriesName<T>();
    }
}
