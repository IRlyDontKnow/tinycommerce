using System;
using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Application.Categories.CreateCategory;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategory;
using TinyCommerce.Modules.Catalog.Tests.Integration.SeedWork;

namespace TinyCommerce.Modules.Catalog.Tests.Integration.Categories
{
    [TestFixture]
    public class CategoryTests : TestBase
    {
        [Test]
        public async Task CreateCategory_AsRoot_WithValidData_ShouldSucceed()
        {
            await CatalogModule.ExecuteCommandAsync(new CreateCategoryCommand(
                CategorySampleData.Id,
                CategorySampleData.Slug,
                CategorySampleData.Name,
                CategorySampleData.Description,
                null
            ));

            var category = await CatalogModule.ExecuteQueryAsync(new GetCategoryQuery(CategorySampleData.Id));

            Assert.That(category, Is.Not.Null);
            Assert.That(category.Id, Is.EqualTo(CategorySampleData.Id));
            Assert.That(category.Slug, Is.EqualTo(CategorySampleData.Slug));
            Assert.That(category.Name, Is.EqualTo(CategorySampleData.Name));
            Assert.That(category.Description, Is.EqualTo(CategorySampleData.Description));
            Assert.That(category.ParentId, Is.Null);
        }

        [Test]
        public async Task CreateCategory_AsChild_WithValidData_ShouldSucceed()
        {
            var clothesCatId = await CreateSampleCategory("clothes", "Clothes");

            await CatalogModule.ExecuteCommandAsync(new CreateCategoryCommand(
                CategorySampleData.Id,
                CategorySampleData.Slug,
                CategorySampleData.Name,
                CategorySampleData.Description,
                clothesCatId
            ));

            var category = await CatalogModule.ExecuteQueryAsync(new GetCategoryQuery(CategorySampleData.Id));

            Assert.That(category, Is.Not.Null);
            Assert.That(category.Id, Is.EqualTo(CategorySampleData.Id));
            Assert.That(category.Slug, Is.EqualTo(CategorySampleData.Slug));
            Assert.That(category.Name, Is.EqualTo(CategorySampleData.Name));
            Assert.That(category.Description, Is.EqualTo(CategorySampleData.Description));
            Assert.That(category.ParentId, Is.EqualTo(clothesCatId));
        }

        private async Task<Guid> CreateSampleCategory(string slug, string name, Guid? id = null, Guid? parentId = null)
        {
            var categoryId = id ?? Guid.NewGuid();

            await CatalogModule.ExecuteCommandAsync(new CreateCategoryCommand(
                categoryId,
                slug,
                name,
                CategorySampleData.Description,
                parentId
            ));

            return categoryId;
        }
    }
}