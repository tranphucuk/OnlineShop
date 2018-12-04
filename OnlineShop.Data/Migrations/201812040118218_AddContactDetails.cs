namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false, maxLength: 250),
                        Other = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactDetails");
        }
    }
}
