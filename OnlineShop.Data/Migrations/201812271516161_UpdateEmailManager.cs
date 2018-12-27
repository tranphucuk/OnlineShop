namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailManager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailManagers", "RecipientCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailManagers", "RecipientCount");
        }
    }
}
