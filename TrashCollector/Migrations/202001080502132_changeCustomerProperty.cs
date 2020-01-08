namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCustomerProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PickupDay", c => c.String());
            AlterColumn("dbo.Customers", "SuspendStart", c => c.String());
            AlterColumn("dbo.Customers", "SuspendEnd", c => c.String());
            AlterColumn("dbo.Customers", "ExtraPickupDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ExtraPickupDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "SuspendEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "SuspendStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "PickupDay", c => c.Int(nullable: false));
        }
    }
}
