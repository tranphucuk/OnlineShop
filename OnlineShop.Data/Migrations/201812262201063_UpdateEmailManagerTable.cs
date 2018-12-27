namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailManagerTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmailManagers", "MailTitle", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.EmailManagers", "MailContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmailManagers", "MailContent", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.EmailManagers", "MailTitle", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
