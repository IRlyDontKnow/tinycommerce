using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Migrations
{
    [Migration(2)]
    public class Migration2 : Migration
    {
        public override void Up()
        {
            Create.Schema("backoffice");

            Create.Table("administrator")
                .InSchema("backoffice")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("role").AsString().NotNullable()
                .WithColumn("created_at").AsDateTime2().NotNullable();

            Create.Table("outbox_messages")
                .InSchema("backoffice")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }

        public override void Down()
        {
            Delete.Schema("backoffice");
        }
    }
}