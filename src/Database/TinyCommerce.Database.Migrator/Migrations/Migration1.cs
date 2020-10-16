using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Migrations
{
    [Migration(1)]
    public class Migration1 : Migration
    {
        public override void Up()
        {
            Create.Schema("customers");

            Create.Table("customer_registration")
                .InSchema("customers")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("status").AsString().NotNullable()
                .WithColumn("activation_code").AsString().Nullable()
                .WithColumn("registration_date").AsDateTime().NotNullable();

            Create.Table("customer")
                .InSchema("customers")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("registration_date").AsDateTime().NotNullable();

            Create.Table("outbox_messages")
                .InSchema("customers")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();

            Create.Table("internal_commands")
                .InSchema("customers")
                .WithColumn("id").AsGuid().PrimaryKey().Unique()
                .WithColumn("enqueue_date").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("error").AsString().Nullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }

        public override void Down()
        {
            Delete.Table("customer_registration").InSchema("customers");
            Delete.Table("customer").InSchema("customers");
            Delete.Table("outbox_messages").InSchema("customers");
            Delete.Table("internal_commands").InSchema("customers");
        }
    }
}