using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Catalog.Domain.Brands
{
    public class BrandId : TypedIdValueBase
    {
        public BrandId(Guid value) : base(value)
        {
        }
    }
}