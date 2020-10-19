using System;
using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Seeds
{
    [Profile("Development")]
    public class CatalogSeeds : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("category").InSchema("catalog")
                .Row(new
                {
                    id = Guid.Parse("9b7e7305-c16a-448b-bbaf-5ac0f0f09b0f"),
                    slug = "electronics",
                    name = "Electronics",
                    created_at = DateTime.UtcNow
                });

            Insert.IntoTable("category").InSchema("catalog")
                .Row(new
                {
                    id = Guid.Parse("1dd97678-fce3-4674-9bc5-85848224ebc0"),
                    slug = "camera-and-photo",
                    name = "Camera & Photo",
                    parent_id = Guid.Parse("9b7e7305-c16a-448b-bbaf-5ac0f0f09b0f"),
                    created_at = DateTime.UtcNow
                });

            Insert.IntoTable("category").InSchema("catalog")
                .Row(new
                {
                    id = Guid.Parse("3207ee48-5182-4f6f-96f9-2539d4cb3516"),
                    slug = "digital-cameras",
                    name = "Digital Cameras",
                    parent_id = Guid.Parse("1dd97678-fce3-4674-9bc5-85848224ebc0"),
                    created_at = DateTime.UtcNow
                });

            Insert.IntoTable("category").InSchema("catalog")
                .Row(new
                {
                    id = Guid.Parse("ce5777e7-cf18-4b14-9a89-23ab77b6596b"),
                    slug = "camera-drones",
                    name = "Camera drones",
                    parent_id = Guid.Parse("1dd97678-fce3-4674-9bc5-85848224ebc0"),
                    created_at = DateTime.UtcNow
                });
        }

        public override void Down()
        {
        }
    }
}