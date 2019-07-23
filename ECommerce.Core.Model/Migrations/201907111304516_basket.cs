namespace ECommerce.Core.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basket : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Baskets", new[] { "Products_ID" });
            RenameColumn(table: "dbo.Baskets", name: "Products_ID", newName: "ProductID");
            AlterColumn("dbo.Baskets", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Baskets", "ProductID");
            DropColumn("dbo.Baskets", "Product");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Baskets", "Product", c => c.Int(nullable: false));
            DropIndex("dbo.Baskets", new[] { "ProductID" });
            AlterColumn("dbo.Baskets", "ProductID", c => c.Int());
            RenameColumn(table: "dbo.Baskets", name: "ProductID", newName: "Products_ID");
            CreateIndex("dbo.Baskets", "Products_ID");
        }
    }
}
