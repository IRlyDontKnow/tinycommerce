using System;
using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Seeds
{
    [Profile("Development")]
    public class BackOfficeSeed : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("administrator").InSchema("backoffice")
                .Row(new
                {
                    id = Guid.Parse("142cb2f1-89b5-4457-a076-d7499e8f998a"),
                    email = "admin@squadore.com",
                    password = "AHh6GaJAoEbx8GmVPFNArpT7+unRc8/kYR+nm8e3FodEJ6730rVR9gtUdd7BV/Rm4w==",
                    first_name = "John",
                    last_name = "Doe",
                    role = "BusinessOwner",
                    created_at = DateTime.UtcNow
                });
        }

        public override void Down()
        {
        }
    }
}