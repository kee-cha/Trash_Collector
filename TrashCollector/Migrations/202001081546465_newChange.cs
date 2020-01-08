namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PickupDay", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PickupDay", c => c.Int(nullable: false));
        }
    }
}
