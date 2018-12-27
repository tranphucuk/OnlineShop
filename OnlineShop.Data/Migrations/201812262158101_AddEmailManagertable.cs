namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailManagertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailManagers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailUser = c.String(nullable: false, maxLength: 50, unicode: false),
                        MailTitle = c.String(nullable: false, maxLength: 50),
                        MailContent = c.String(nullable: false, maxLength: 50),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailManagers");
        }
    }
}
