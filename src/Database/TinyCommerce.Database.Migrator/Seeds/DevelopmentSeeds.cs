using FluentMigrator;

namespace TinyCommerce.Database.Migrator.Seeds
{
    [Profile("Development")]
    public class DevelopmentSeeds : Migration
    {
        public override void Up()
        {
            Execute.Script("Scripts/0001_Administrators_Seed.sql");
            Execute.Script("Scripts/0002_Customers_Seed.sql");
            Execute.Script("Scripts/0003_Categories_Seed.sql");
        }

        public override void Down()
        {
        }
    }
}
