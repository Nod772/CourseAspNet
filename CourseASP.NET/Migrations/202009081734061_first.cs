namespace CourseASP.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                        Image = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Category_Id = c.Int(),
                        Client_Id = c.String(maxLength: 128),
                        SallerShop_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SallerShop_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.SallerShop_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleDate = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Product_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SallerShop_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Products", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.OrderProducts", new[] { "Product_Id" });
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            DropIndex("dbo.Products", new[] { "SallerShop_Id" });
            DropIndex("dbo.Products", new[] { "Client_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
