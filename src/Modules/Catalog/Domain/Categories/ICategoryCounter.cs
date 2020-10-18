namespace TinyCommerce.Modules.Catalog.Domain.Categories
{
    public interface ICategoryCounter
    {
        int CountByName(string name, CategoryId parentId);

        int CountBySlug(string slug, CategoryId parentId);
    }
}