namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDisplayNameToMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ZipCode", c => c.Int(nullable: false));
        }
    }
}
