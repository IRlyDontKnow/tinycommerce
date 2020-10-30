using System;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategoriesForSelect
{
    public class CategoryItemDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Path { get; set; }
       
        public Guid? ParentId { get; set; }
    }
}