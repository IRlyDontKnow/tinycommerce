using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Domain.Brands;
using TinyCommerce.Modules.Catalog.Domain.Brands.Events;
using TinyCommerce.Modules.Catalog.Tests.Unit.Domain.SeedWork;

namespace TinyCommerce.Modules.Catalog.Tests.Unit.Domain.Brands
{
    [TestFixture]
    public class BrandTests : TestBase
    {
        [Test]
        public void CreateNew_WithValidData_ShouldSucceed()
        {
            var brand = Brand.CreateNew(
                BrandSampleData.Id,
                BrandSampleData.Name,
                BrandSampleData.Slug,
                BrandSampleData.Description
            );

            var domainEvent = AssertPublishedDomainEvent<BrandCreatedDomainEvent>(brand);

            Assert.That(domainEvent.BrandId, Is.EqualTo(BrandSampleData.Id));
            Assert.That(domainEvent.Name, Is.EqualTo(BrandSampleData.Name));
            Assert.That(domainEvent.Slug, Is.EqualTo(BrandSampleData.Slug));
            Assert.That(domainEvent.Description, Is.EqualTo(BrandSampleData.Description));
        }
    }
}