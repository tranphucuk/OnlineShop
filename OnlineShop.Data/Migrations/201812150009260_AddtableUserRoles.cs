namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtableUserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUserRoles", "RoleName", c => c.String(maxLength: 250));
            AddColumn("dbo.ApplicationUserRoles", "UserName", c => c.String(maxLength: 250));
            AddColumn("dbo.ApplicationUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUserRoles", "Discriminator");
            DropColumn("dbo.ApplicationUserRoles", "UserName");
            DropColumn("dbo.ApplicationUserRoles", "RoleName");
        }
    }
}
