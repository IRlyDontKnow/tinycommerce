using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Migrations
{
    [Migration(3)]
    public class Migration3 : Migration
    {
        public override void Up()
        {
            Create.Schema("catalog");

            Create.Table("category")
                .InSchema("catalog")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("slug").AsString().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("parent_id").AsGuid().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().Nullable();

            Create.Table("outbox_messages")
                .InSchema("catalog")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }

        public override void Down()
        {
            Delete.Schema("catalog");
        }
    }
}