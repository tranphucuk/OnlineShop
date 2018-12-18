namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviseTableIdentityUserRole : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUserRoles", "RoleName");
            DropColumn("dbo.ApplicationUserRoles", "UserName");
            DropColumn("dbo.ApplicationUserRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ApplicationUserRoles", "UserName", c => c.String(maxLength: 250));
            AddColumn("dbo.ApplicationUserRoles", "RoleName", c => c.String(maxLength: 250));
        }
    }
}
