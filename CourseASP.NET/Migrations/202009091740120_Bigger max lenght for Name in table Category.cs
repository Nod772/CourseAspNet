namespace CourseASP.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BiggermaxlenghtforNameintableCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
