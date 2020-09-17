namespace CourseASP.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withid_category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ID_Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ID_Category");
        }
    }
}
