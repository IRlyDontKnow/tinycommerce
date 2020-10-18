using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Categories
{
    public class Category : Entity, IAggregateRoot
    {
       public CategoryId Id { get; }
    }
}