namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Emails", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emails", "Status");
            DropColumn("dbo.Emails", "CreatedDate");
        }
    }
}
