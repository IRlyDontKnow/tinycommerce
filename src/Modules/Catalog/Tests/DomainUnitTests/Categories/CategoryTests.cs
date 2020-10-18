using NSubstitute;
using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Domain.Categories;
using TinyCommerce.Modules.Catalog.Domain.Categories.Events;
using TinyCommerce.Modules.Catalog.Domain.Categories.Rules;
using TinyCommerce.Modules.Catalog.Tests.Unit.Domain.SeedWork;

namespace TinyCommerce.Modules.Catalog.Tests.Unit.Domain.Categories
{
    [TestFixture]
    public class CategoryTests : TestBase
    {
        [Test]
        public void CreateNew_Test()
        {
            var categoryCounter = Substitute.For<ICategoryCounter>();

            var category = Category.CreateNew(
                CategorySampleData.Id,
                CategorySampleData.Slug,
                CategorySampleData.Name,
                CategorySampleData.Description,
                CategorySampleData.ParentId,
                categoryCounter
            );

            var domainEvent = AssertPublishedDomainEvent<CategoryCreatedDomainEvent>(category);

            Assert.That(domainEvent.CategoryId, Is.EqualTo(CategorySampleData.Id));
            Assert.That(domainEvent.Slug, Is.EqualTo(CategorySampleData.Slug));
            Assert.That(domainEvent.Name, Is.EqualTo(CategorySampleData.Name));
            Assert.That(domainEvent.Description, Is.EqualTo(CategorySampleData.Description));
            Assert.That(domainEvent.ParentId, Is.EqualTo(CategorySampleData.ParentId));
        }

        [Test]
        public void TryCreateNew_WithAlreadyUsedSlug_ShouldFail()
        {
            var categoryCounter = Substitute.For<ICategoryCounter>();
            categoryCounter.CountBySlug(CategorySampleData.Slug, null).Returns(1);

            AssertBrokenRule<CategorySlugMustBeUniqueRule>(() =>
            {
                Category.CreateNew(
                    CategorySampleData.Id,
                    CategorySampleData.Slug,
                    CategorySampleData.Name,
                    CategorySampleData.Description,
                    null,
                    categoryCounter
                );
            });
        }

        [Test]
        public void TryCreateNew_WithAlreadyUsedName_ShouldFail()
        {
            var categoryCounter = Substitute.For<ICategoryCounter>();
            categoryCounter.CountByName(CategorySampleData.Name, null).Returns(1);

            AssertBrokenRule<CategoryNameMustBeUniqueRule>(() =>
            {
                Category.CreateNew(
                    CategorySampleData.Id,
                    CategorySampleData.Slug,
                    CategorySampleData.Name,
                    CategorySampleData.Description,
                    null,
                    categoryCounter
                );
            });
        }
    }
}