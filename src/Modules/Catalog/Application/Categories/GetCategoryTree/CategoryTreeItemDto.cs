using System;
using System.Collections.Generic;

namespace TinyCommerce.Modules.Catalog.Application.Categories.GetCategoryTree
{
    public class CategoryTreeItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Slug { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        public int Level { get; set; }
        
        public List<CategoryTreeItemDto> Children { get; set; }

        public Guid? ParentId { get; set; }
    }
}
