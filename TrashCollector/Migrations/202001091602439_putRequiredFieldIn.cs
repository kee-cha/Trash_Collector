namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class putRequiredFieldIn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "SuspendStart", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "SuspendStart", c => c.String());
        }
    }
}
