using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Migrations
{
    [Migration(4)]
    public class Migration4 : Migration
    {
        public override void Up()
        {
            Create.Table("brand")
                .InSchema("catalog")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("slug").AsString().NotNullable()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("created_at").AsDateTime2().NotNullable();

            Create.Table("product")
                .InSchema("catalog")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("slug").AsString().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("seo_keywords").AsString().Nullable()
                .WithColumn("seo_title").AsString().Nullable()
                .WithColumn("seo_description").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("brand").InSchema("catalog");
        }
    }
}