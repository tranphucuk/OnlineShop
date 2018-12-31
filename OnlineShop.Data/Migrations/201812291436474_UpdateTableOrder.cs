namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ProductQuantity");
        }
    }
}
