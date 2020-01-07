namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMigrationWithChangeInCustomerClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PickupDay", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "PickupDay", c => c.String());
        }
    }
}
