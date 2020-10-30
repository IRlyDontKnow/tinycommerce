using System;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategory
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        
        public string Slug { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public Guid? ParentId { get; set; }
        
        public DateTime CreatedAt { get; set; }
       
        public DateTime? UpdatedAt { get; set; }
    }
}