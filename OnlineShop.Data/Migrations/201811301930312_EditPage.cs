namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pages", "MetaKeyword", c => c.String(maxLength: 256));
            AlterColumn("dbo.Pages", "MetaDescription", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pages", "MetaDescription", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Pages", "MetaKeyword", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
