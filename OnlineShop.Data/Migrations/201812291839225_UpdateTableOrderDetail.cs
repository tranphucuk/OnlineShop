namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductUrl");
        }
    }
}
