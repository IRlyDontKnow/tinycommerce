using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Application.Brands.AddBrand;
using TinyCommerce.Modules.Catalog.Application.Brands.GetBrand;
using TinyCommerce.Modules.Catalog.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.Catalog.Tests.Integration.Brands
{
    [TestFixture]
    public class BrandTests : TestBase
    {
        [Test]
        public async Task AddBrand_WithValidData_ShouldSucceed()
        {
            await CatalogModule.ExecuteCommandAsync(new AddBrandCommand(
                BrandSampleData.Id,
                BrandSampleData.Name,
                BrandSampleData.Slug,
                BrandSampleData.Description
            ));

            var brand = await CatalogModule.ExecuteQueryAsync(new GetBrandQuery(brandId: BrandSampleData.Id));

            Assert.That(brand, Is.Not.Null);
            Assert.That(brand.Id, Is.EqualTo(BrandSampleData.Id));
            Assert.That(brand.Name, Is.EqualTo(BrandSampleData.Name));
            Assert.That(brand.Slug, Is.EqualTo(BrandSampleData.Slug));
            Assert.That(brand.Description, Is.EqualTo(BrandSampleData.Description));
        }
    }
}