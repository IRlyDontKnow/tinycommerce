using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TinyCommerce.Modules.Catalog.Application.Categories.CreateCategory;
using TinyCommerce.Modules.Catalog.Application.Categories.EditCategory;
using TinyCommerce.Modules.Catalog.Application.Categories.GetCategories;
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

        [Test]
        public async Task EditCategory_WithValidData_ShouldSucceed()
        {
            await CatalogModule.ExecuteCommandAsync(new CreateCategoryCommand(
                CategorySampleData.Id,
                "clothes",
                "Clothes",
                "Clothes description",
                null
            ));

            await CatalogModule.ExecuteCommandAsync(new EditCategoryCommand(
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
        public async Task GetCategories_RootCategoriesOnly_Test()
        {
            await CreateSampleCategory("photo-and-camera", "Photo & Camera");
            var clothesCatId = await CreateSampleCategory("clothes", "Clothes");
            await CreateSampleCategory("t-shirts", "T-Shirts", parentId: clothesCatId);
            await CreateSampleCategory("jeans", "Jeans", parentId: clothesCatId);

            var categories = await CatalogModule.ExecuteQueryAsync(new GetCategoriesQuery());

            Assert.That(categories.FirstOrDefault(x => x.Slug == "clothes"), Is.Not.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "photo-and-camera"), Is.Not.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "t-shirts"), Is.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "jeans"), Is.Null);
        }

        [Test]
        public async Task GetCategories_ChildCategoriesOnly_Test()
        {
            await CreateSampleCategory("photo-and-camera", "Photo & Camera");
            var clothesCatId = await CreateSampleCategory("clothes", "Clothes");
            await CreateSampleCategory("t-shirts", "T-Shirts", parentId: clothesCatId);
            await CreateSampleCategory("jeans", "Jeans", parentId: clothesCatId);

            var categories = await CatalogModule.ExecuteQueryAsync(new GetCategoriesQuery(clothesCatId));

            Assert.That(categories.FirstOrDefault(x => x.Slug == "t-shirts"), Is.Not.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "jeans"), Is.Not.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "photo-and-camera"), Is.Null);
            Assert.That(categories.FirstOrDefault(x => x.Slug == "clothes"), Is.Null);
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